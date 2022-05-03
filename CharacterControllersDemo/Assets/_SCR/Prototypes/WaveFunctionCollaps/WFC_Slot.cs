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

        public WFC_Slot northNeighbour = null;
        public WFC_Slot westNeighbor = null;
        public WFC_Slot southNeighbor = null;
        public WFC_Slot eastNeighbor = null;
        public Dictionary<string, int[]> edgeCounts = new Dictionary<string, int[]>();
        
        public WFC_Slot Initialize(WFC_Slot north, WFC_Slot south, WFC_Slot west, WFC_Slot east, List<WFC_Module> states, Vector3 positions, Transform transform)
        {
            States = new List<WFC_Module>(states);
            edgeCounts.Clear();
            edgeCounts.Add("North", new int[3]);
            edgeCounts.Add("South", new int[3]);
            edgeCounts.Add("East", new int[3]);
            edgeCounts.Add("West", new int[3]);
            foreach (WFC_Module module in States)
            {
                edgeCounts["North"][module.northEdge]++;
                edgeCounts["South"][module.southEdge]++;
                edgeCounts["East"][module.eastEdge]++;
                edgeCounts["West"][module.westEdge]++;
            }
            position = positions;
            northNeighbour = north;
            southNeighbor = south;
            eastNeighbor = east;
            westNeighbor = west;
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
            List<int> northEdgesLost = new List<int>();
            List<int> southEdgesLost = new List<int>();
            List<int> eastEdgesLost = new List<int>();
            List<int> westEdgesLost = new List<int>();

            // Remove
            foreach (WFC_Module state in toRemove)
            {
                if (!States.Remove(state))
                {
                    continue;
                }
                edgeCounts["North"][state.northEdge]--;
                if (edgeCounts["North"][state.northEdge] <= 0)
                {
                    northEdgesLost.Add(state.northEdge);
                }
                edgeCounts["South"][state.southEdge]--;
                if (edgeCounts["South"][state.southEdge] <= 0)
                {
                    southEdgesLost.Add(state.southEdge);
                }
                edgeCounts["East"][state.eastEdge]--;
                if (edgeCounts["East"][state.eastEdge] <= 0)
                {
                    eastEdgesLost.Add(state.eastEdge);
                }
                edgeCounts["West"][state.westEdge]--;
                if (edgeCounts["West"][state.westEdge] <= 0)
                {
                    westEdgesLost.Add(state.westEdge);
                }
            }
            toRemove.Clear();

            Log(nameof(RemoveState) + " - " + DictionaryString(edgeCounts));

            // North
            if (northNeighbour != null && !northNeighbour.isSet)
            {
                northNeighbour.SouthEdgeLost(northEdgesLost);
                northNeighbour.RemoveState();
            }
            // South
            if (southNeighbor != null && !southNeighbor.isSet)
            {
                southNeighbor.NorthEdgeLost(southEdgesLost);
                southNeighbor.RemoveState();
            }
            // West
            if (westNeighbor != null && !westNeighbor.isSet)
            {
                westNeighbor.EastEdgeLost(westEdgesLost);
                westNeighbor.RemoveState();
            }
            // East
            if (eastNeighbor != null && !eastNeighbor.isSet)
            {
                eastNeighbor.WestEdgeLost(eastEdgesLost);
                eastNeighbor.RemoveState();
            }
        }

        public void SouthEdgeLost(in List<int> lostEdges)
        {
            Log(nameof(SouthEdgeLost) + ": " + ListString(lostEdges));
            for (int i = 0; i < States.Count; i++)
            {
                if (lostEdges.Contains(States[i].southEdge))
                {
                    AddToRemove(States[i]);
                }
            }
        }
        public void NorthEdgeLost(in List<int> lostEdges)
        {
            Log(nameof(NorthEdgeLost) + ": " + ListString(lostEdges));
            for (int i = 0; i < States.Count; i++)
            {
                if (lostEdges.Contains(States[i].northEdge))
                {
                    AddToRemove(States[i]);
                }
            }
        }
        public void WestEdgeLost(in List<int> lostEdges)
        {
            Log(nameof(WestEdgeLost) + ": " + ListString(lostEdges));
            for (int i = 0; i < States.Count; i++)
            {
                if (lostEdges.Contains(States[i].westEdge))
                {
                    AddToRemove(States[i]);
                }
            }
        }
        public void EastEdgeLost(in List<int> lostEdges)
        {
            Log(nameof(EastEdgeLost) + ": " + ListString(lostEdges));
            for (int i = 0; i < States.Count; i++)
            {
                if (lostEdges.Contains(States[i].eastEdge))
                {
                    AddToRemove(States[i]);
                }
            }
        }

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
        public string Name => $"({position.x}, {position.y}, {position.z})";
    }
}