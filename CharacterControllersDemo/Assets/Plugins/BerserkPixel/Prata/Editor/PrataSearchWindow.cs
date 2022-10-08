using System.Collections.Generic;
using BerserkPixel.Prata.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BerserkPixel.Prata
{
    public class PrataSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private PrataGraphView graphView;
        private Texture2D indentationIcon;

        public void Init(PrataGraphView prataGraphView)
        {
            graphView = prataGraphView;
            indentationIcon = new Texture2D(1, 1);
            indentationIcon.SetPixel(0, 0, Color.clear);
            indentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            return new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Element")),
                new SearchTreeGroupEntry(new GUIContent("Dialog Node"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", indentationIcon))
                {
                    level = 2,
                    userData = NodeTypes.SingleChoice
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", indentationIcon))
                {
                    level = 2,
                    userData = NodeTypes.MultipleChoice
                },
                new SearchTreeGroupEntry(new GUIContent("Dialog Group"), 1),
                new SearchTreeEntry(new GUIContent("Single Group", indentationIcon))
                {
                    level = 2,
                    userData = new Group()
                }
            };
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var localMousePosition = graphView.GetLocalMousePosition(context.screenMousePosition, true);

            switch (searchTreeEntry.userData)
            {
                case NodeTypes.SingleChoice:
                    var singleNode =
                        (PrataSingleChoiceNode)graphView.CreateNode(NodeTypes.SingleChoice, localMousePosition);
                    graphView.AddElement(singleNode);
                    return true;
                case NodeTypes.MultipleChoice:
                    var multipleNode =
                        (PrataMultipleChoiceNode)graphView.CreateNode(NodeTypes.MultipleChoice, localMousePosition);
                    graphView.AddElement(multipleNode);
                    return true;
                case Group _:
                    var group = graphView.CreateGroup("Dialog Group", localMousePosition);
                    graphView.AddElement(group);
                    return true;
                default:
                    return false;
            }
        }
    }
}