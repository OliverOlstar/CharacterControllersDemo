using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRoom : MonoBehaviour
{
	public List<ArenaRoomGenerator.Edge> edges = new List<ArenaRoomGenerator.Edge>();
	public ArenaRoomGenerator.Edge[] GetEdgesRandomizedOrder()
	{
		if (edges.Count < 2)
			return edges.ToArray();

		List<ArenaRoomGenerator.Edge> list = new List<ArenaRoomGenerator.Edge>(edges);
		ArenaRoomGenerator.Edge[] rand = new ArenaRoomGenerator.Edge[edges.Count];

		while (list.Count > 0)
		{
			int i = Random.Range(0, list.Count);
			rand[list.Count - 1] = list[i];
			list.RemoveAt(i);
		}

		return rand;
	}
}
