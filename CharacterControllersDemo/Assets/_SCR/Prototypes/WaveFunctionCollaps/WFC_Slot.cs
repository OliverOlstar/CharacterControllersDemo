using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.WaveFunctionCollapse
{
	[System.Serializable]
	public class WFC_Slot
	{
		private Vector3 m_Position = Vector3.zero;
		private Transform container = null;
		public List<WFC_Module> States = new List<WFC_Module>();
		public bool isSet = false;

		public Dictionary<Vector3Int, int[]> edges = new Dictionary<Vector3Int, int[]>();
		
		public WFC_Slot()
		{
			CreateEdgesDictionary();
		}

		private void CreateEdgesDictionary()
		{
			edges.Clear();
			edges.Add(Vector3Int.up, new int[3]);
			edges.Add(Vector3Int.down, new int[3]);
			edges.Add(Vector3Int.right, new int[3]);
			edges.Add(Vector3Int.left, new int[3]);
		}

		public WFC_Slot Initialize(List<WFC_Module> states, Vector3 position, Transform transform)
		{
			States = new List<WFC_Module>(states);
			foreach (WFC_Module module in States)
			{
				AddEdges(module);
			}
			m_Position = position;
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
			GameObject.Instantiate(module.gameObject, m_Position, Quaternion.identity, container);
			isSet = true;

			CreateEdgesDictionary();
			AddEdges(module);
		}

		private void AddEdges(WFC_Module module)
		{
			edges[Vector3Int.up][module.edges[0]]++;
			edges[Vector3Int.down][module.edges[1]]++;
			edges[Vector3Int.right][module.edges[2]]++;
			edges[Vector3Int.left][module.edges[3]]++;
		}

		public List<int> GetValidEdges(Vector3Int dir)
		{
			List<int> result = new List<int>();
			for (int i = 0; i < 3; i++)
			{
				if (edges[dir][i] > 0)
				{
					result.Add(i);
				}
			}
			return result;
		}

		public bool Constrain(List<int> validEdges, Vector3Int dir)
		{
			if (isSet)
			{
				return false;
			}
			Log($"{nameof(Constrain)}() - {dir}");
			bool changed = false;
			for (int i = 0; i < States.Count; i++)
			{
				if (!validEdges.Contains(States[i].GetEdge(dir)))
				{
					RemoveModule(States[i]);
					i--;
					changed = true;
				}
			}
			return changed;
		}

		public void RemoveModule(WFC_Module module)
		{
			edges[Vector3Int.up][module.edges[0]]--;
			edges[Vector3Int.down][module.edges[1]]--;
			edges[Vector3Int.right][module.edges[2]]--;
			edges[Vector3Int.left][module.edges[3]]--;
			States.Remove(module);
		}

#region Helpers
		private void Log(string message)
		{
			Debug.Log($"[Slot ({m_Position.x}, {m_Position.y}, {m_Position.z})] {message}");
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
		public string Name => $"({m_Position.x}, {m_Position.y}, {m_Position.z})";
#endregion
	}
}