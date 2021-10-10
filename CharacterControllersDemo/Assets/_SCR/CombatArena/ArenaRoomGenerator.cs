using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ArenaRoomGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Edge
    {
        public ArenaRoom roomA = null;
        public ArenaRoom roomB = null;
        public GameObject edgeObject = null;

        public bool closed { get; private set; } = true;

        public void Toggle(bool pClosed)
        {
            edgeObject.SetActive(pClosed);
            closed = pClosed;
        }

        public ArenaRoom OtherRoom(ArenaRoom pRoom)
        {
            return (roomA == pRoom ? roomB : roomA);
        }
    }

    [SerializeField, Required()] private ArenaRoom[] mustUseRooms = new ArenaRoom[1];
    [SerializeField] private int targetRoomCount = 4;

    [Space, SerializeField] private Edge[] edges = new Edge[0];

    [Button()]
    public void SetEdges()
    {
        foreach (ArenaRoom room in FindObjectsOfType<ArenaRoom>())
        {
            room.edges.Clear();
        }

        foreach(Edge e in edges)
        {
            // Set A
            e.roomA.edges.Add(e);

            // Set B
            e.roomB.edges.Add(e);
        }
    }

    [Button()]
    public void ResetEdges()
    {
        foreach (Edge e in edges)
        {
            e.Toggle(true);
        }
    }

    [Button()]
    public void Generate()
    {
        ResetEdges();

        // Calculate short path
        List<ArenaRoom> path = GeneratePath(mustUseRooms[0], new List<ArenaRoom>());

        // Open path
        if (path != null)
        {
            Debug.Log("Found Path: " + path.Count);
            for (int i = 0; i < path.Count - 1; i++)
            {
                foreach (Edge edge in path[i].edges)
                {
                    if (edge.OtherRoom(path[i]) == path[i + 1])
                    {
                        edge.Toggle(false);
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Path == null");
        }

        // Add random rooms in
        while (path.Count < targetRoomCount)
        {
            ArenaRoom room = path[Random.Range(0, path.Count)];
            foreach (Edge edge in room.GetEdgesRandomizedOrder())
            {
                if (edge.closed)
                {
                    path.Add(edge.OtherRoom(room));
                    edge.Toggle(false);
                    break;
                }
            }
        }

        // Randomize creating room loops
        foreach (ArenaRoom room in path)
        {
            foreach (Edge edge in room.edges)
            {
                if (edge.closed && Random.value > 0.5f && path.Contains(edge.OtherRoom(room)))
                {
                    edge.Toggle(false);
                }
            }
        }
    }

    private List<ArenaRoom> GeneratePath(ArenaRoom pActiveRoom, List<ArenaRoom> pList)
    {
        pList.Add(pActiveRoom);
        //Debug.Log("Now checking " + pActiveRoom.name + " | count: " + pList.Count);

        // Found valid path, return my path
        if (IsValidPath(pList))
        {
            //Debug.Log(pActiveRoom.name + " | Found Path, returning it: " + pList.Count);
            return pList;
        }

        List<ArenaRoom> shortest = null;
        int shortestCount = int.MaxValue;
        foreach (Edge edge in pActiveRoom.edges)
        {
            //Debug.Log(pActiveRoom.name + " | Check Edge: " + pList.Count);
            ArenaRoom otherRoom = edge.OtherRoom(pActiveRoom);
            if (!pList.Contains(otherRoom))
            {
                List<ArenaRoom> path = GeneratePath(otherRoom, new List<ArenaRoom>(pList));
                if (path != null)
                {
                    // If under target then 40% chance else if shortest is greater than target and curr is less curr shortest
                    if ((shortestCount > targetRoomCount && path.Count < shortestCount) || (path.Count <= targetRoomCount && Random.value < 0.4f))
                    {
                        shortest = path;
                        shortestCount = shortest.Count;
                    }
                }
            }
        }

        // Return edge's path
        //Debug.Log(pActiveRoom.name + " | Length: " + (shortest == null ? "NULL" : shortest.Count.ToString()));
        return shortest;
    }

    private bool IsValidPath(List<ArenaRoom> pList)
    {
        // TODO Change to be O(n)
        foreach (ArenaRoom room in mustUseRooms)
        {
            if (!pList.Contains(room))
            {
                return false;
            }
        }
        return true;
    }
}
