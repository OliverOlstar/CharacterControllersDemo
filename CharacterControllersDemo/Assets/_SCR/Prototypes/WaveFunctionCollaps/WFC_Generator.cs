using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace OliverLoescher.WaveFunctionCollapse
{
	public class WFC_Generator : MonoBehaviour
	{
		[SerializeField]
		private int GRIDSIZE = 10;
		[SerializeField]
		private float PICKDELAY = 0.02f;

		[SerializeField]
		private List<WFC_Module> modules = new List<WFC_Module>();
		private WFC_Slot[,] slots = null;

		[SerializeField]
		private Stack<Vector3Int> m_Stack = new Stack<Vector3Int>();

		public static List<WFC_Slot> slotsToUpdate = new List<WFC_Slot>();

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

			slotsToUpdate.Clear();
			slots = new WFC_Slot[GRIDSIZE, GRIDSIZE];
			for (int x = 0; x < GRIDSIZE; x++)
				for (int y = 0; y < GRIDSIZE; y++)
				{
					slots[x, y] = new WFC_Slot();
					slotsToUpdate.Add(slots[x, y]);
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
					slots[x, y].Initialize(neighbours, modules, new Vector3(x, 0.0f, y), transform);
				}

			WFC_Slot slot = GetNext();
			while (slot != null)
			{
				slot.PickModule();
				slot = GetNext();
				yield return new WaitForSeconds(PICKDELAY);
			}
		}

		private WFC_Slot GetNext()
		{
			if (slotsToUpdate.Count == 0)
			{
				return null;
			}

			WFC_Slot smallest = null;
			int smallestSize = int.MaxValue;
			foreach (WFC_Slot slot in slotsToUpdate)
			{
				if (slot.isSet)
				{
					Debug.LogError($"slot {slot.Name} is set but is also in slotsToUpdate, this shouldn't be here");
					continue;
				}
				int moduleSize = slot.States.Count;
				if (moduleSize < smallestSize || (moduleSize == smallestSize && Random.value > 0.25f))
				{
					smallest = slot;
					smallestSize = moduleSize;
				}
			}
			return smallest;
		}

		private void Propagate(Vector3Int index)
		{
			m_Stack.Append(index);
			while (m_Stack.Count > 0)
			{
				Vector3Int currIndex = m_Stack.Pop();
				foreach (Vector3Int neighIndex in ValidNeighbours(currIndex))
				{
					
				}
			}
		}

		private WFC_Slot GetSlot(Vector3Int index) => slots[index.x, index.y];

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
	}
}