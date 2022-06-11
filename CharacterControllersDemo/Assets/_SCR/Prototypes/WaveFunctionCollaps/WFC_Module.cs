using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.WaveFunctionCollapse
{
	public class WFC_Module : MonoBehaviour
	{
		public int[] edges = new int[4];

#if UNITY_EDITOR
		private void OnDrawGizmosSelected() 
		{
			UnityEditor.Handles.Label(transform.position + Vector3.forward * 0.65f, edges[0].ToString());
			UnityEditor.Handles.Label(transform.position + Vector3.back * 0.65f, edges[1].ToString());
			UnityEditor.Handles.Label(transform.position + Vector3.right * 0.65f, edges[2].ToString());
			UnityEditor.Handles.Label(transform.position + Vector3.left * 0.65f, edges[3].ToString());
		}
#endif

		public static int GetOpposingEdge(int edgeIndex)
		{
			switch (edgeIndex)
			{
				case 0: return 1;
				case 1: return 0;
				case 2: return 3;
				case 3: return 2;
			}
			return -1;
		}

		public int GetEdge(Vector3Int dir)
		{
			if (dir == Vector3Int.up)
			{
				return edges[0];
			}
			if (dir == Vector3Int.down)
			{
				return edges[1];
			}
			if (dir == Vector3Int.right)
			{
				return edges[2];
			}
			return edges[3]; // Left
		}
	}
}