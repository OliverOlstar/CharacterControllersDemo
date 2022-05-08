using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.WaveFunctionCollapse
{
	[System.Serializable]
	public class WFC_Slot
	{
		public Vector3 position = Vector3.zero;
		private Transform container = null;
		public List<WFC_Module> States = new List<WFC_Module>();
		public bool isSet = false;

		public WFC_Slot[] neighbours = new WFC_Slot[4];
		public int[,] edgeCounts = new int[4, 3];
		
		public WFC_Slot Initialize(WFC_Slot[] neighs, List<WFC_Module> states, Vector3 positions, Transform transform)
		{
			States = new List<WFC_Module>(states);
			edgeCounts = new int[4, 3];
			foreach (WFC_Module module in States)
			{
				for (int i = 0; i < 4; i++)
				{
					edgeCounts[i, module.edges[i]]++;
				}
			}
			neighbours = neighs;
			position = positions;
			container = transform;
			isSet = false;
			return this;
		}

		public void PickModule()
		{
			if (isSet)
			{
				return;
			}
			if (States.Count == 0)
			{
				isSet = true;
				Debug.LogError("No states left when there should be");
				return;
			}
			Log("PickModule()");

			WFC_Module module = States[Random.Range(0, States.Count)];
			GameObject.Instantiate(module.gameObject, position, Quaternion.identity, container);
			isSet = true;

			AddToRemove(States);
			RemoveFromToRemove(module);
			RemoveState();
			
			WFC_Generator.slotsToUpdate.Remove(this);
		}

		private List<WFC_Module> toRemove = new List<WFC_Module>();
		public void AddToRemove(WFC_Module state) 
		{
			if (toRemove.Contains(state))
			{
				return;
			}
			toRemove.Add(state);
		}
		public void AddToRemove(List<WFC_Module> states) => toRemove.AddRange(states);
		public void RemoveFromToRemove(WFC_Module state) => toRemove.Remove(state);

		public void RemoveState()
		{
			if (toRemove.Count == 0)
			{
				return;
			}
			Log(nameof(RemoveState) + " - " + States[0].name + " - " + States.Count + " _ " + ListString(toRemove));
			List<int>[] edgesLost = new List<int>[4];
			for (int i = 0; i < 4; i++)
			{
				edgesLost[i] = new List<int>();
			}

			// Remove
			foreach (WFC_Module state in toRemove)
			{
				if (!States.Remove(state))
				{
					continue;
				}
				for (int i = 0; i < 4; i++)
				{
					edgeCounts[i, state.edges[i]]--;
					if (edgeCounts[i, state.edges[i]] <= 0)
					{
						edgesLost[i].Add(state.edges[i]);
					}
				}
			}
			toRemove.Clear();

			Log(nameof(RemoveState) + " - " + Array2DString(edgeCounts));
			
			for (int i = 0; i < 4; i++)
			{
				if (neighbours[i] != null && !neighbours[i].isSet)
				{
					neighbours[i].EdgesLost(WFC_Module.GetOpposingEdge(i), edgesLost[i]);
					neighbours[i].RemoveState();
				}
			}
		}

		public void EdgesLost(int edgeIndex, in List<int> lostEdges)
		{
			Log(nameof(EdgesLost) + ": " + ListString(lostEdges));
			for (int i = 0; i < States.Count; i++)
			{
				if (lostEdges.Contains(States[i].edges[edgeIndex]))
				{
					AddToRemove(States[i]);
				}
			}
		}

#region Helpers
		private void Log(string message)
		{
			Debug.Log($"[Slot ({position.x}, {position.y}, {position.z})] {message}");
		}
		private string ListString(List<WFC_Module> modules)
		{
			string output = "{";
			for (int i = 0; i < modules.Count; i++)
			{
				output += modules[i].name;
				if (i < modules.Count - 1)
				{
					output += ", ";
				}
			}
			return output + "}";
		}
		private string ListString(List<int> values)
		{
			string output = "{";
			for (int i = 0; i < values.Count; i++)
			{
				output += values[i];
				if (i < values.Count - 1)
				{
					output += ", ";
				}
			}
			return output + "}";
		}
		private string DictionaryString(Dictionary<string, int[]> dictionary)
		{
			string output = "\n{";
			foreach (KeyValuePair<string, int[]> valuePair in dictionary)
			{
				output += $"\n{valuePair.Key}: [";
				for (int i = 0; i < valuePair.Value.Length; i++)
				{
					output += valuePair.Value[i] + (i < valuePair.Value.Length - 1 ? ", " : "]");
				}
			}
			return output + "\n}";
		}
		private string Array2DString(int[,] array)
		{
			string output = "\n{";
			for (int x = 0; x < array.GetLength(0); x++)
			{
				output += $"\n{x}: [";
				for (int y = 0; y < array.GetLength(1); y++)
				{
					output += array[x, y] + (y < array.GetLength(1) - 1 ? ", " : "]");
				}
			}
			return output + "\n}";
		}
		public string Name => $"({position.x}, {position.y}, {position.z})";
#endregion
	}
}