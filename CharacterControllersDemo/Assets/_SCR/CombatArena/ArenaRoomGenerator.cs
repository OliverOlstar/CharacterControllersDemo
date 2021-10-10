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
    }

    [SerializeField, Required()] private ArenaRoom[] mustUseRooms = new ArenaRoom[1];
    private List<ArenaRoom> checkedRooms = new List<ArenaRoom>();

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
            ArenaRoom.Edge n = new ArenaRoom.Edge();
            n.edgeObject = e.edgeObject;
            
            // Set A
            n.room = e.roomB;
            e.roomA.edges.Add(n);
            
            // Set B
            n.room = e.roomA;
            e.roomB.edges.Add(n);
        }
    }

    [Button()]
    public void Generate()
    {
        checkedRooms.Clear();
        Connect(mustUseRooms[0]);
    }

    private void Connect(ArenaRoom pRoom)
    {
        foreach (ArenaRoom.Edge edge in pRoom.edges)
        {
            if (!checkedRooms.Contains(edge.room))
            {
                edge.Toggle(Random.value > 0.5f);
                checkedRooms.Add(edge.room);
                Connect(edge.room);
            }
        }
    }
}
