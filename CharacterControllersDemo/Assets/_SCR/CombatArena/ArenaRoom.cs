using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRoom : MonoBehaviour
{
    [System.Serializable]
    public class Edge
    {
        public ArenaRoom room = null;
        public GameObject edgeObject = null;

        public void Toggle(bool pClosed)
        {
            edgeObject.SetActive(pClosed);
        }
    }

    public List<Edge> edges = new List<Edge>();
}
