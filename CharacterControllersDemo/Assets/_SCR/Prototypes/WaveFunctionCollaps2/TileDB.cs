using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.WFC
{
	[CreateAssetMenu(fileName = "TileDB", menuName = "DBs/WFC Tile DB", order = 1)]
    public class TileDB : ScriptableObject
    {
		[SerializeField]
		private TileData[] values = new TileData[0];

		public TileData[] Values => values;
    }
}
