using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.WFC
{
	[System.Serializable]
    public class TileData
    {
		[SerializeField]
		private char id = 'A';
		[SerializeField]
		private int[] edges = new int[4];

		public int GetEdgeType(int index)
		{
			return edges[index];
		}
    }
}
