using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher 
{
	public static class FuncUtil
	{
		public static float SmoothStep(float pMin, float pMax, float pIn)
		{
			return Mathf.Clamp01((pIn - pMin) / (pMax - pMin));
		}
		public static float SmoothStep(Vector2 pMinMax, float pIn) => SmoothStep(pMinMax.x, pMinMax.y, pIn);

		public static float SafeAngle(float pAngle)
		{
			if (pAngle > 180)
			{
				pAngle -= 360;
			}
			return pAngle;
		}
	}
}