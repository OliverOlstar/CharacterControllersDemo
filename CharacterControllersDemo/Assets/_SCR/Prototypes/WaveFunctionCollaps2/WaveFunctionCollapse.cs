using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.WFC
{
	[System.Serializable]
	public struct LostEdge
	{
		public int index;
		public int type;

		public LostEdge(int pIndex, int pType)
		{
			index = pIndex;
			type = pType;
		}
	}

	[System.Serializable]
	public struct Slot
	{
		private List<TileData> tiles;
		private int[,] edgeTypeCounts;
		public bool Collapsed => collapsedTile != null;
		public TileData collapsedTile { get; private set; }

		public Slot(TileData[] pTiles)
		{
			Debug.Log("[WFC][Slot] Constructor");
			tiles = new List<TileData>(pTiles);
			edgeTypeCounts = new int[4, 3]; // Hard coded 4 sides, 3 edges types
			foreach (TileData tile in pTiles)
			{
				for (int i = 0; i < edgeTypeCounts.GetLength(0); i++)
				{
					edgeTypeCounts[i, tile.GetEdgeType(i)]++;
				}
			}
			Util.Log("[WFC][Slot] Constructor | EdgeTypeCounts:", edgeTypeCounts);
			collapsedTile = null;
		}

		public void Collapse()
		{
			if (Collapsed)
			{
				return;
			}
			collapsedTile = tiles[Random.Range(0, tiles.Count)];
			throw new System.NotImplementedException(); // TODO pass back what edges are enforced and propigate
		}
	}

	[System.Serializable]
	public struct Grid
	{
		public Slot[,] slots;
		public int xLength { get; private set; }
		public int yLength { get; private set; }

		public Grid(int pXLength, int pYLength, TileData[] pTiles)
		{
			xLength = pXLength;
			yLength = pYLength;
			slots = new Slot[pXLength, pYLength];

			Slot defaultSlot = new Slot(pTiles);
			for (int x = 0; x < xLength; x++)
				for (int y = 0; y < yLength; y++)
				{
					slots[x, y] = defaultSlot;
				}
		}

		// private IEnumerable<Slot> GetAllSlots()
		// {
		// 	for (int x = 0; x < xLength; x++)
		// 		for (int y = 0; y < yLength; y++)
		// 		{
		// 			yield return slots[x, y];
		// 		}
		// }
	}

    public class WaveFunctionCollapse : MonoBehaviour
    {
		[SerializeField]
		private TileDB tileDB = null;

		[Space, SerializeField, Min(1)]
		private Vector2Int gridSize = new Vector2Int(4, 4);

		private Grid grid;

		[Button()]
		public void Setup()
		{
			grid = new Grid(gridSize.x, gridSize.y, tileDB.Values);
		}

		[Button()]
		public void Collapse(int pX, int pY)
		{
			grid.slots[pX, pY].Collapse();
		}
    }
}
