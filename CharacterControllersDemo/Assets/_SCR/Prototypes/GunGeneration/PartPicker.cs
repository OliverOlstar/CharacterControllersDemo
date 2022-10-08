using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.GunGenerator
{
	public class PartPicker : MonoBehaviour
	{
		[SerializeField, Required] private string key = "";
		[Range(0, 1)] public float chance01 = 1.0f;
		
		[Header("Surface Area")]
		[SerializeField, Range(0.0f, 0.2f)] private float surfaceAreaWidth = 0.0f;
		[SerializeField, Range(0.0f, 0.2f)] private float surfaceAreaLength = 0.0f;

		void Start()
		{
			if (chance01 < 1 && Random.value > chance01)
				return;

			key = key.ToLower();
			
			GameObject p = PartPickingManager.parts[key].GetPrefab(surfaceAreaLength, surfaceAreaWidth);
			if (p == null)
				return;

			GameObject part = Instantiate(p, transform, false);
			part.transform.localPosition = Vector3.zero;
			part.transform.localRotation = Quaternion.identity;

			Destroy(this);
		}

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
