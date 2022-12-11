using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
	[System.Serializable]
	public struct Capsule : IPrimitive
	{
		public Vector3 center;
		public float radius;
		public float height;
		public Transform upTransform;

		public Vector3 Up => upTransform != null ? upTransform.up : Vector3.up;

		public Capsule(Transform pUpTransform)
		{
			center = default;
			radius = 0.5f;
			height = 2.0f;
			upTransform = pUpTransform;
#if DEBUG
			debugMovements = new List<Movement2>();
#endif
		}
		public Capsule(Vector3 pCenter, float pRadius, float pHeight, Transform pUpTransform = null)
		{
			center = pCenter;
			radius = pRadius;
			height = pHeight;
			upTransform = pUpTransform;
#if DEBUG
			debugMovements = new List<Movement2>();
#endif
		}

		public bool PointIntersects(Vector3 pPoint, Vector3 pPosition)
		{
			Vector3 worldSpaceCenter = pPosition + center;
			float sphereHeight = height - (radius * 2.0f);
			if (sphereHeight <= 0.0f) // Is a sphere
			{
				return Util.DistanceGreaterThan(pPoint, worldSpaceCenter, radius);
			}
			else if (pPoint.y > worldSpaceCenter.y + sphereHeight * 0.5f) // Is above
			{
				return Util.DistanceGreaterThan(pPoint, worldSpaceCenter + (Up * sphereHeight * 0.5f), radius);
			}
			else if (pPoint.y < worldSpaceCenter.y - sphereHeight * 0.5f) // If below
			{
				return Util.DistanceGreaterThan(pPoint, worldSpaceCenter + (-Up * sphereHeight * 0.5f), radius);
			}
			else // Is at same height
			{
				return Util.DistanceOnPlaneEqualGreaterThan(pPoint, worldSpaceCenter, radius, Up);
			}
		}

		public bool CheckCollisions(Vector3 pMovement, Vector3 pPosition, int pIterations, LayerMask pLayerMask, out Vector3 resultPosition, out Vector3 collisionNormal) // Returns resulting position
		{
#if DEBUG
			if (debugMovements == null)
			{
				debugMovements = new List<Movement2>();
			}
#endif

			resultPosition = pPosition + pMovement;
			collisionNormal = Up; // Default result

			// Raycast
			RaycastHit? nearestHit = null;
			Vector3 up = Up * ((height * 0.5f) - radius);
			foreach (RaycastHit hit in Physics.CapsuleCastAll(pPosition + center + up, pPosition + center - up, radius, pMovement, pMovement.magnitude, pLayerMask))
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
				resultPosition = pPosition + movementToTarget;
				collisionNormal = /*IsValidGround(nearestHit.Value.normal) ?*/ nearestHit.Value.normal /*: Util.Horizontalize(nearestHit.Value.normal, Up, true)*/; // Slope you can't walk up or down, consider them as just flat walls
				pMovement = Vector3.ProjectOnPlane(pMovement - movementToTarget, collisionNormal);

				if (pIterations > 0)
				{
#if DEBUG
					debugMovements.Add(new Movement2(pPosition, resultPosition, 0.1f));
#endif
					return CheckCollisions(resultPosition, pMovement, --pIterations, pLayerMask, out resultPosition, out collisionNormal);
				}
			}
#if DEBUG
			debugMovements.Add(new Movement2(pPosition, resultPosition, 0.1f));
#endif
			return nearestHit.HasValue;
		}

#if DEBUG
		private struct Movement2
		{
			public Vector3 fromPosition;
			public Vector3 toPosition;
			public float lifeEndTime;
			public Color color;

			public Movement2(Vector3 pFrom, Vector3 pTo, float pLifeTime)
			{
				fromPosition = pFrom;
				toPosition = pTo;
				lifeEndTime = Time.time + pLifeTime;
				color = new Color(Mathf.Pow(Random.value, 2), Mathf.Pow(Random.value, 2), Mathf.Pow(Random.value, 2));
			}
		}

		private List<Movement2> debugMovements;
#endif

		public void DrawGizmos(Vector3 pPosition)
		{
			pPosition += center;
			Vector3 up = Up * ((height * 0.5f) - radius);
			Util.GizmoCapsule(pPosition + up, pPosition - up, radius);

#if DEBUG
			for (int i = 0; debugMovements != null && i < debugMovements.Count; i++)
			{
				if (debugMovements[i].lifeEndTime <= Time.time)
				{
					debugMovements.RemoveAt(i);
					i--;
					continue;
				}

				Gizmos.color = debugMovements[i].color;
				Gizmos.DrawLine(debugMovements[i].fromPosition, debugMovements[i].toPosition);
				Util.GizmoCapsule(debugMovements[i].toPosition + up + center, debugMovements[i].toPosition - up + center, radius);
				Gizmos.color = Color.white;
#endif
			}
		}
	}

	[System.Serializable]
	public struct CharacterControllerCapsule : IPrimitive
	{
		public CharacterController controller;
		[DisableInPlayMode]
		public Transform upTransform; // Copy so we can set in inspector but not have to show our capsule member
		[HideInInspector]
		public Capsule capsule;

		public CharacterControllerCapsule(CharacterController pController, Transform pUpTransform = null)
		{
			controller = pController;
			upTransform = pUpTransform;
			capsule = new Capsule(controller.center, controller.radius, controller.height, upTransform);
		}

		public Vector3 Up => capsule.Up;

		private void UpdateCapsuleValues()
		{
			if (controller == null)
			{
				Debug.LogError("CharacterControllerCapsule.UpdateCapsuleValues() But has no characterController, this should never happen.");
				return;
			}
			capsule.center = controller.center;
			capsule.radius = controller.radius;
			capsule.height = controller.height;
		}

		public bool PointIntersects(Vector3 pPoint, Vector3 pPosition)
		{
			UpdateCapsuleValues();
			return capsule.PointIntersects(pPoint, pPosition);
		}

		public bool CheckCollisions(Vector3 pMovement, Vector3 pPosition, int pIterations, LayerMask pLayerMask, out Vector3 resultPosition, out Vector3 collisionNormal) // Returns resulting position
		{
			UpdateCapsuleValues();
			return capsule.CheckCollisions(pMovement, pPosition, pIterations, pLayerMask, out resultPosition, out collisionNormal);
		}

		public void DrawGizmos(Vector3 pPosition)
		{
			UpdateCapsuleValues();
			capsule.DrawGizmos(pPosition);
		}
	}
}
