using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace OliverLoescher.WaveFunctionCollapse
{
	public class WFC_Generator : MonoBehaviour
	{
		private static readonly Vector3Int NULLVector3Int = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);

		[SerializeField]
		private int GRIDSIZE = 10;
		[SerializeField]
		private float PICKDELAY = 0.02f;

		[SerializeField]
		private List<WFC_Module> modules = new List<WFC_Module>();
		private WFC_Slot[,] slots = null;

		[SerializeField]
		private Stack<Vector3Int> m_Stack = new Stack<Vector3Int>();

		[Button]
		public void Generate()
		{
			StopAllCoroutines();
			StartCoroutine(GenerateRoutine());
		}

		private IEnumerator GenerateRoutine()
		{
			while (transform.childCount > 0)
			{
				Transform child = transform.GetChild(0);
				child.SetParent(null);
				DestroyImmediate(child.gameObject);
			}

			slots = new WFC_Slot[GRIDSIZE, GRIDSIZE];
			for (int x = 0; x < GRIDSIZE; x++)
				for (int y = 0; y < GRIDSIZE; y++)
				{
					slots[x, y] = new WFC_Slot();
				}
			WFC_Slot[] neighbours;
			for (int x = 0; x < GRIDSIZE; x++)
				for (int y = 0; y < GRIDSIZE; y++)
				{
					neighbours = new WFC_Slot[4];
					neighbours[0] = y == GRIDSIZE - 1 ? null : slots[x, y + 1];
					neighbours[1] = y == 0 ? null : slots[x, y - 1];
					neighbours[2] = x == GRIDSIZE - 1 ? null : slots[x + 1, y];
					neighbours[3] = x == 0 ? null : slots[x - 1, y];
					slots[x, y].Initialize(modules, new Vector3(x, 0, y), transform);
				}

			Vector3Int index = GetNext();
			while (index != NULLVector3Int)
			{
				GetSlot(index).PickModule();
				Propagate(index);
				index = GetNext();
				yield return new WaitForSeconds(PICKDELAY);
			}
		}

		private Vector3Int GetNext()
		{
			Vector3Int smallest = NULLVector3Int;
			int smallestSize = int.MaxValue;
			for (int x = 0; x < GRIDSIZE; x++)
				for (int y = 0; y < GRIDSIZE; y++)
				{
					Vector3Int index = new Vector3Int(x, y, 0);
					if (GetSlot(index).isSet)
					{
						continue;
					}
					int moduleSize = slots[x, y].States.Count;
					if (moduleSize < smallestSize || (moduleSize == smallestSize && Random.value > 0.25f))
					{
						smallest = index;
						smallestSize = moduleSize;
					}
				}
			return smallest;
		}

		private void Propagate(Vector3Int index)
		{
			m_Stack.Push(index);
			while (m_Stack.Count > 0)
			{
				Vector3Int currIndex = m_Stack.Pop();
				Debug.Log($"{nameof(Propagate)}() - {currIndex}");
				foreach (Vector3Int neighIndex in ValidNeighbours(currIndex))
				{
					if (Constrain(currIndex, neighIndex))
					{
						m_Stack.Push(neighIndex);
					}
				}
			}
		}

		private bool Constrain(Vector3Int indexFrom, Vector3Int indexTo)
		{
			Vector3Int dir = indexTo - indexFrom;
			if (GetSlot(indexTo).isSet)
			{
				return false;
			}
			return GetSlot(indexTo).Constrain(GetSlot(indexFrom).GetValidEdges(dir), dir);
		}

		private IEnumerable<Vector3Int> ValidNeighbours(Vector3Int currIndex)
		{
			if (currIndex.y + 1 < GRIDSIZE)
			{
				yield return currIndex + new Vector3Int(0, 1, 0); // North
			}
			if (currIndex.y > 0)
			{
				yield return currIndex + new Vector3Int(0, -1, 0); // South
			}
			if (currIndex.x + 1 < GRIDSIZE)
			{
				yield return currIndex + new Vector3Int(1, 0, 0); // East
			}
			if (currIndex.x > 0)
			{
				yield return currIndex + new Vector3Int(-1, 0, 0); // West
			}
		}

		private WFC_Slot GetSlot(Vector3Int index) => slots[index.x, index.y];
	}
}