using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
    public static class MathUtil
    {
		public static Vector3 Horizontalize(Vector3 pVector, in bool pNormalize = false)
		{
			pVector.y = 0;
			if (pNormalize)
				pVector.Normalize();
			return pVector;
		}

		public static Vector3 Inverse(in Vector3 pVector) => new Vector3(1.0f / pVector.x, 1.0f / pVector.y, 1.0f / pVector.z);
	}
}