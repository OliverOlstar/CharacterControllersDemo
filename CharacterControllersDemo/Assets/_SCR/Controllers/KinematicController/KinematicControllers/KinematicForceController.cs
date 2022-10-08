using OliverLoescher;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicForceController : KinematicController
{
	[Header("Force")]
	[SerializeField]
	private InputBridge_KinematicController input = null;
	[SerializeField]
	private Transform forwardTransform = null;
	[SerializeField, DisableInPlayMode]
	private MonoUtil.UpdateType updateType = default;

	[Header("Move")]
	[SerializeField, Min(0.0f)]
	private float forwardAccel = 5.0f;
	[SerializeField, Min(0.0f)]
	private float sideAccel = 5.0f;
	[SerializeField, Min(0.0f)]
	private float drag = 5.0f;

	[Header("Sprint")]
	[SerializeField, Min(0.0f)]
	private float sprintForwardAccel = 5.0f;
	[SerializeField, Min(0.0f)]
	private float sprintSideAccel = 5.0f;

	[Header("Jump")]
	[SerializeField, Min(0.0f)]
	private float jumpForce = 5.0f;

	private Vector3 velocity = Vector3.zero;

	public Vector3 Velocity => velocity;
	public Vector3 Forward()
	{
		if (forwardTransform == null)
		{
			if (upTransform == null)
			{
				return Vector3.forward;
			}
			return Vector3.ProjectOnPlane(Vector3.forward, Up).normalized;
		}
		return Vector3.ProjectOnPlane(forwardTransform.forward, Up).normalized;
	}
	public Vector3 Right()
	{
		if (forwardTransform == null)
		{
			if (upTransform == null)
			{
				return Vector3.right;
			}
			return Vector3.ProjectOnPlane(Vector3.right, Up).normalized;
		}
		return Vector3.ProjectOnPlane(forwardTransform.right, Up).normalized;
	}

	public bool IsSprinting => input.Sprint.Input;
	private float ForwardAccel => IsSprinting ? sprintForwardAccel : forwardAccel;
	private float SideAccel => IsSprinting ? sprintSideAccel : sideAccel;

	private void Start()
	{
		MonoUtil.RegisterUpdate(this, Tick, updateType, MonoUtil.Priorities.CharacterControllers);
		input.Jump.onPerformed.AddListener(DoJump);
	}

	private void OnDestroy()
	{
		MonoUtil.DeregisterUpdate(this, updateType);
		input.Jump.onPerformed.RemoveListener(DoJump);
	}

	private void Tick(float pDeltaTime)
	{
		AddMove(pDeltaTime);

		velocity -= velocity * drag * pDeltaTime;
		velocity.y = 0.0f;
		Move(velocity * pDeltaTime);
	}

	private void AddMove(in float pDeltaTime)
	{
		if (input.Move.Input == Vector2.zero)
		{
			return;
		}

		velocity += Forward() * input.Move.Input.y * pDeltaTime * ForwardAccel;
		velocity += Right() * input.Move.Input.x * pDeltaTime * SideAccel;
	}

	private void DoJump()
	{
		if (isGrounded)
		{
			verticalVelocity = jumpForce;
		}
	}
}
