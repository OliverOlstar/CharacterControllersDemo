using System;
using System.Collections.Generic;
using BerserkPixel.Prata.Data;
using UnityEngine;

namespace BerserkPixel.Prata
{
    [Serializable]
    public class DialogContainer : ScriptableObject
    {
        public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
        public List<DialogNodeData> DialogNodes = new List<DialogNodeData>();
    }
}