using System;
using System.Collections.Generic;
using BerserkPixel.Prata.Elements;
using UnityEngine;

namespace BerserkPixel.Prata.Data
{
    [Serializable]
    public class DialogNodeData
    {
        public string Guid;
        public DialogContent Content;
        public Vector2 Position;
        public NodeTypes Type;
        public List<string> Choices;

        public DialogNodeData(PrataNode prataNode)
        {
            var content = new DialogContent();
            content.Fill(prataNode);

            Guid = prataNode.GUID;
            Content = content;
            Position = prataNode.GetPosition().position;
            Type = prataNode.DialogType;
            Choices = prataNode.Choices;
        }
    }
}