using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher 
{
	public static class RandUtil
	{
		public static float Range(Vector2 pRange) => Random.Range(pRange.x, pRange.y);
		public static int Range(Vector2Int pRange) => Random.Range(pRange.x, pRange.y);
		public static float Range(float pRange) => Random.Range(-pRange, pRange);
		public static int Range(int pRange) => Random.Range(-pRange, pRange);
	}
}