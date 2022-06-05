using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
    public static class MathUtil
    {
		public static Vector3 Horizontalize(Vector3 pVector, bool pNormalize = false)
		{
			pVector.y = 0;
			if (pNormalize)
				pVector.Normalize();
			return pVector;
		}
	}
}