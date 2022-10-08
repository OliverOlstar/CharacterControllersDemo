using OliverLoescher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[InfoBox("1. IsGround could be done better\n2. Moving object will pass through instead of push\n3. Doesn't move with moving objects\n4. No good solution to make getting over little ledges")]
public class KinematicController : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField, Min(0.0f)]
	private float gravity = 9.81f;
	[SerializeField]
	private float groundedDistance = 0.05f;
	[SerializeField, Range(-1.0f, 1.0f)]
	private float groundAllowDot = 0.5f;
	[SerializeField]
	protected Transform upTransform = null;
	[SerializeField]
	private int collisionIterations = 3;

	[Header("Capsule")]
	[SerializeField]
	private float radius = 1.0f;
	[SerializeField]
	private float height = 1.0f;
	[SerializeField]
	private Vector3 center = Vector3.zero;
	[SerializeField]
	private LayerMask layerMask = new LayerMask();

	protected bool isGrounded = false;
	protected float verticalVelocity = 0.0f;
	protected Vector3 groundNormal = Vector3.up;

	public bool IsGrounded => isGrounded;
	public Vector3 Up => upTransform == null ? transform.up : upTransform.up;
	private bool IsValidGround(in Vector3 normal) => Vector3.Dot(Up, normal) > groundAllowDot;

	private void FixedUpdate()
	{
		UpdateGrounded();
	}

	public void Move(Vector3 pMove)
	{
		if (isGrounded)
		{
			float y = pMove.y;
			pMove.y = 0;
			pMove = Util.Horizontalize(pMove, groundNormal, pMove.magnitude);
			pMove.y += y;
		}
		CheckCollisions(transform.position, pMove, collisionIterations, out Vector3 resultPos, out _);
		transform.position = resultPos;
	}

	private void UpdateGrounded()
	{
		// TODO do less casts for grounded
		if (verticalVelocity <= Util.NEARZERO && CheckCollisions(transform.position, Up * -groundedDistance, 0, out Vector3 resultPos, out Vector3 collisionNormal) && IsValidGround(collisionNormal))
		{
			verticalVelocity = 0.0f;
			groundNormal = collisionNormal;
			isGrounded = true;
		}
		else
		{
			verticalVelocity += -gravity * Time.fixedDeltaTime;
			isGrounded = CheckCollisions(transform.position, Up * verticalVelocity, 1, out resultPos, out collisionNormal) && IsValidGround(collisionNormal);
			groundNormal = isGrounded ? collisionNormal : Up;
		}
		transform.position = resultPos;
	}

	private bool CheckCollisions(Vector3 pCurrPos, Vector3 pMovement, int iterations, out Vector3 resultPos, out Vector3 collisionNormal) // Returns resulting position
	{
		resultPos = pCurrPos + pMovement;
		collisionNormal = Up;

		// Raycast
		RaycastHit? nearestHit = null;
		Vector3 up = Up * ((height * 0.5f) - radius);
		foreach (RaycastHit hit in Physics.CapsuleCastAll(pCurrPos + center + up, pCurrPos + center - up, radius, pMovement, pMovement.magnitude, layerMask))
		{
			if (!nearestHit.HasValue || nearestHit.Value.distance > hit.distance)
			{
				nearestHit = hit;
			}
		}

		// Result
		if (nearestHit.HasValue)
		{
			Vector3 movementToTarget = pMovement.normalized * (nearestHit.Value.distance - Util.NEARZERO);
			resultPos = pCurrPos + movementToTarget;
			collisionNormal = /*IsValidGround(nearestHit.Value.normal) ?*/ nearestHit.Value.normal /*: Util.Horizontalize(nearestHit.Value.normal, Up, true)*/; // Slope you can't walk up or down, consider them as just flat walls
			pMovement = Vector3.ProjectOnPlane(pMovement - movementToTarget, collisionNormal);

			if (iterations > 0)
			{
				return CheckCollisions(resultPos, pMovement, --iterations, out resultPos, out collisionNormal);
			}
		}
		return nearestHit.HasValue;
	}

	private void OnDrawGizmos()
	{
		Vector3 pos = transform.position;
		Vector3 up = Up * ((height * 0.5f) - radius);
		Util.GizmoCapsule(pos + center + up, pos + center - up, radius);
		Gizmos.DrawLine(transform.position, transform.position - (Up * groundedDistance));
	}
}