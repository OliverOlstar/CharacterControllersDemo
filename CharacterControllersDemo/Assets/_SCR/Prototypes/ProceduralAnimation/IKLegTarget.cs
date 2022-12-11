using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OliverLoescher;

namespace Prototype
{
    public class IKLegTarget : IKLimb
	{
		private enum State
		{
			Idle,
			Stepping,
			Blocked
		}

		[SerializeField]
		private LayerMask layerMask = new LayerMask();
		[SerializeField]
		private IKLimb[] opposingLimbs = new IKLimb[0];

		[Header("Idle")]
		[SerializeField]
		private float maxDistance = 1.0f;
		[SerializeField]
		private float maxHeight = 2.0f;
		[SerializeField]
		private float stepSpeed = 1.0f;
		[SerializeField]
		private Vector2 stepHeight = new Vector2(2.0f, -2.0f);

		[Header("Min")]
		[SerializeField]
		private float maxDistanceMax = 1.0f;
		[SerializeField]
		private float maxHeightMax = 2.0f;
		[SerializeField]
		private float stepSpeedMax = 1.0f;
		[SerializeField]
		private Vector2 stepHeightMax = new Vector2(2.0f, -2.0f);

		private State state = State.Stepping;
		private Vector3 initalOffset;
		private Vector3 targetPosition;
		private Vector3 previousOffset;
		private Capsule capsule;

		private float PercentToMax01 => core == null ? 0.0f : core.SpeedPercent01;
		private float MaxDistance => Mathf.Lerp(maxDistance, maxDistanceMax, PercentToMax01);
		private float MaxHeight => Mathf.Lerp(maxHeight, maxHeightMax, PercentToMax01);
		private float StepSpeed => Mathf.Lerp(stepSpeed, stepSpeedMax, PercentToMax01);
		private Vector2 StepHeight => Vector2.Lerp(stepHeight, stepHeightMax, PercentToMax01);

		public override bool IsBlocking => state == State.Stepping;
		private Vector3 localPosition => core.Body.InverseTransformPoint(transform.position);

		public override void OnInitalize()
		{
			initalOffset = localPosition;
			targetPosition = core.Body.TransformPoint(initalOffset);
			capsule = new Capsule(core.Body);
		}

		public override void Tick()
		{
			targetPosition = core.Body.TransformPoint(initalOffset);

			switch (state)
			{
				case State.Idle:
					IdleTick();
					break;

				case State.Stepping:
					SteppingTick();
					break;

				case State.Blocked:
					BlockedTick();
					break;
			}
		}

		private void SwitchState(State pState)
		{
			if (state == pState)
			{
				return;
			}

			state = pState;
			switch (state)
			{
				case State.Idle:
					break;

				case State.Stepping:
					previousOffset = localPosition;
					break;

				case State.Blocked:
					previousOffset = localPosition;
					break;
			}
		}

		private void IdleTick()
		{
			if (PointIntersectsCapsule(transform.position, targetPosition))
			{
				SwitchState(IsBlocked() ? State.Blocked : State.Stepping);
			}
		}

		private void SteppingTick()
		{
			transform.position = core.Body.TransformPoint(previousOffset);

			if (Physics.Linecast(targetPosition + (core.Up * StepHeight.x), targetPosition + (core.Up * StepHeight.y), out RaycastHit info, layerMask))
			{
				transform.position = Vector3.MoveTowards(transform.position, info.point + (core.Up * Mathf.Clamp(Util.DistanceOnPlane(targetPosition, transform.position, core.Up), 0.0f, 5.0f)), Time.deltaTime * StepSpeed);

				if (Util.DistanceEqualLessThan(transform.position, info.point, Util.NEARZERO))
				{
					transform.position = info.point;
					SwitchState(State.Idle);
				}
			}

			previousOffset = localPosition;
		}

		private void BlockedTick()
		{
			transform.position = Vector3.Lerp(transform.position, core.Body.TransformPoint(previousOffset), 0.25f);
			previousOffset = localPosition;

			if (!IsBlocked() || PointIntersectsCapsule(transform.position, targetPosition))
			{
				SwitchState(State.Stepping);
			}
		}

		private bool IsBlocked()
		{
			foreach (IKLimb limb in opposingLimbs)
			{
				if (limb.IsBlocking)
				{
					return true;
				}
			}
			return false;
		}

		private bool PointIntersectsCapsule(Vector3 pPosition, Vector3 pTarget)
		{
			capsule.height = MaxHeight;
			capsule.radius = MaxDistance;
			return capsule.PointIntersects(transform.position, targetPosition);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = state == State.Stepping ? Color.blue : Color.yellow;
			Gizmos.DrawSphere(transform.position, 0.3f);
			Gizmos.color = Color.white;

			Transform t = transform;
			Vector3 pos = transform.position;
			if (core != null)
			{
				if (!Application.isPlaying)
				{
					OnInitalize();
				}

				t = core.Body;
				pos = targetPosition;
			}

			Gizmos.DrawLine(transform.position, pos);
			Gizmos.DrawLine(pos + (t.up * StepHeight.x), pos + (t.up * StepHeight.y));
			Util.GizmoCapsule(initalOffset, maxDistance, maxHeight, t.localToWorldMatrix);
			Util.GizmoCapsule(initalOffset, maxDistanceMax, maxHeightMax, t.localToWorldMatrix);
			Gizmos.color = PointIntersectsCapsule(transform.position, pos) ? Color.red : Color.cyan;
			Util.GizmoCapsule(initalOffset, MaxDistance, MaxHeight, t.localToWorldMatrix);
			Gizmos.color = Color.white;
		}
	}
}
