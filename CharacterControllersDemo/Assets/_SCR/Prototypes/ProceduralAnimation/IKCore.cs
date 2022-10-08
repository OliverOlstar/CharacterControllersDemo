using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OliverLoescher;
using UnityEngine.SocialPlatforms.Impl;

namespace Prototype
{
    public class IKCore : MonoBehaviour
	{
		[SerializeField]
		private IKBody body = null;
		[SerializeField]
		private IKLimb[] limbs = new IKLimb[0];

		[Header("Stats")]
		public Vector3 DeltaPosition = Vector3.zero;
		public Quaternion DeltaRotation = Quaternion.identity;

		private Vector3 prevCenter = Vector3.zero;
		private Quaternion prevRotation = Quaternion.identity;

		public Vector3 Up => Body.up;
		public Vector3 Forward => Body.forward;
		public Vector3 Center => Body.position;
		public Transform Body => body.transform;
		public float Speed => body.Speed;
		public float SpeedPercent01 => Mathf.Clamp01(Mathf.Pow(body.Speed, 3) * 0.01f);

		private void Reset()
		{
			body = GetComponentInChildren<IKBody>();
			limbs = GetComponentsInChildren<IKLimb>();
			Start();
		}

		private void Start()
		{
			foreach (IKLimb limb in limbs)
			{
				limb.Initalize(this);
			}
		}

		void Update()
        {
			DeltaPosition = Center - prevCenter;
			prevCenter = Center;

			DeltaRotation = Util.Difference(Body.rotation, prevRotation);
			prevRotation = Body.rotation;

			foreach (IKLimb limb in limbs)
			{
				limb.Tick();
			}
        }
	}
}
