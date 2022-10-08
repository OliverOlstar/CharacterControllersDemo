using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.GunGenerator
{
	public class PartPickerPart : MonoBehaviour
	{
		[Header("Surface Area")]
		[Range(Util.NEARZERO, 0.2f)] public float surfaceAreaWidth = 1.0f;
		[Range(Util.NEARZERO, 0.2f)] public float surfaceAreaLength = 1.0f;

#if UNITY_EDITOR
		[Header("Gizmos")]
		[SerializeField] private Vector3 surfaceAreaAxis = Vector3.forward;
		
		private void OnDrawGizmosSelected() 
		{
			Gizmos.matrix = transform.localToWorldMatrix;

			Vector3 offset = new Vector3(0.0f, 0.0f, surfaceAreaLength * 0.5f);
			Vector3 scale = new Vector3(surfaceAreaWidth, Util.NEARZERO, surfaceAreaLength);

			if (surfaceAreaAxis.sqrMagnitude == 1)
			{
				Quaternion dir = Quaternion.LookRotation(surfaceAreaAxis);
				offset = dir * offset;
				scale = dir * scale;
			}

			Gizmos.color = Color.cyan;
			Gizmos.DrawCube(offset, scale);
		}
#endif
	}
}