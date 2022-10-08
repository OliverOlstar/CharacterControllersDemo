using System;
using System.Collections.Generic;
using System.Linq;
using BerserkPixel.Prata.Data;
using BerserkPixel.Prata.Elements;
using BerserkPixel.Prata.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BerserkPixel.Prata
{
    public class PrataGraphView : GraphView
    {
        public readonly Vector2 DefaultNodeSize = new Vector2(200, 150);
        private readonly PrataEditorWindow editorWindow;
        private PrataSearchWindow searchWindow;

        public PrataGraphView(PrataEditorWindow prataEditorWindow)
        {
            editorWindow = prataEditorWindow;
            AddManipulators();
            AddSearchWindow();
            AddGridBackground();
            AddStyles();

            // Creating start node
            AddElement(GenerateEntryPointNode());
        }

        private PrataNode GenerateEntryPointNode()
        {
            PrataNode node = new PrataNode
            {
                title = "Start",
                DialogType = NodeTypes.Start
            };

            node.SetPosition(new Rect(100, 200, 100, 150));

            var outputPort = node.CreatePort("Next");
            node.outputContainer.Add(outputPort);

            // we can't remove or move the first node
            node.capabilities &= ~Capabilities.Movable;
            node.capabilities &= ~Capabilities.Deletable;

            node.RefreshExpandedState();
            node.RefreshPorts();

            return node;
        }

        #region Override Methods

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort == port) return;

                if (startPort.node == port.node) return;

                if (startPort.direction == port.direction) return;

                compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        #endregion

        #region Element Creation

        public PrataNode RestoreNode(DialogNodeData nodeData)
        {
            var nodeType = Type.GetType($"{PrataConstants.NODE_TYPE_BASE}{nodeData.Type}Node");

            if (nodeType == null) return null;

            var node = (PrataNode)Activator.CreateInstance(nodeType);

            node.Init(this, nodeData, nodeData.Type);
            node.Draw();
            node.RefreshExpandedState();
            node.RefreshPorts();

            return node;
        }

        public PrataNode CreateNode(NodeTypes type, Vector2 mousePosition)
        {
            var nodeType = Type.GetType($"{PrataConstants.NODE_TYPE_BASE}{type}Node");

            if (nodeType == null) return null;

            var node = (PrataNode)Activator.CreateInstance(nodeType);

            node.Init(this, mousePosition, type);
            node.Draw();
            node.RefreshExpandedState();
            node.RefreshPorts();

            return node;
        }

        public Group CreateGroup(string title, Vector2 mousePosition)
        {
            var group = new Group
            {
                title = title
            };

            group.SetPosition(new Rect(mousePosition, Vector2.zero));

            return group;
        }

        #endregion

        #region Manipulators

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContentDragger());

            this.AddManipulator(CreateNodeContextualMenu("Add Single Choice Node", NodeTypes.SingleChoice));
            this.AddManipulator(CreateNodeContextualMenu("Add Multiple Choice Node", NodeTypes.MultipleChoice));
            this.AddManipulator(CreateGroupContextualMenu());
        }

        private IManipulator CreateNodeContextualMenu(string actionTitle, NodeTypes type)
        {
            var contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(
                    actionTitle,
                    actionEvent => AddElement(
                        CreateNode(type, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))
                    )
                )
            );

            return contextualMenuManipulator;
        }

        private IManipulator CreateGroupContextualMenu()
        {
            var contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(
                    "Create Group",
                    actionEvent => AddElement(
                        CreateGroup("Dialog Group", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))
                    )
                )
            );

            return contextualMenuManipulator;
        }

        #endregion

        #region Element Addition

        private void AddSearchWindow()
        {
            if (searchWindow == null)
            {
                searchWindow = ScriptableObject.CreateInstance<PrataSearchWindow>();
                searchWindow.Init(this);
            }

            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        private void AddStyles()
        {
            this.AddStyleSheets(
                PrataConstants.STYLESHEET_GRAPH,
                PrataConstants.STYLESHEET_NODE
            );
        }

        private void AddGridBackground()
        {
            var gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        #endregion

        #region Utilities

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            var worldMousePosition = mousePosition;

            if (isSearchWindow) worldMousePosition -= editorWindow.position.position;

            return contentViewContainer.WorldToLocal(worldMousePosition);
        }

        public void RemovePort(PrataNode node, Port socket)
        {
            if (node.Choices.Count > 1)
            {
                node.RemoveFromChoices(socket.portName);
                var targetEdge = edges.ToList()
                    .Where(x => x.output.portName == socket.portName && x.output.node == socket.node);
                if (targetEdge.Any())
                {
                    var edge = targetEdge.First();
                    edge.input.Disconnect(edge);
                    RemoveElement(targetEdge.First());
                }

                node.outputContainer.Remove(socket);
                node.RefreshPorts();
                node.RefreshExpandedState();
            }
        }

        #endregion
    }
}