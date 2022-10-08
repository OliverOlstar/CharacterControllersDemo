using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class IKBody : MonoBehaviour
    {
		[SerializeField]
		private Rigidbody rigid = null;

		public float Speed => rigid.velocity.magnitude;
	}
}
