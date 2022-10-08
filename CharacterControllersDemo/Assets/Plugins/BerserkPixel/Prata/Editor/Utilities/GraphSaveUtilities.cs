using System.Collections.Generic;
using System.Linq;
using BerserkPixel.Prata.Elements;
using BerserkPixel.Prata.Data;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BerserkPixel.Prata.Utilities
{
    public class GraphSaveUtilities
    {
        private PrataGraphView _targetGraphView;
        private DialogContainer _containerCached;

        private List<Edge> Edges => _targetGraphView.edges.ToList();

        private Dictionary<string, PrataNode> Nodes => _targetGraphView.nodes.ToList().Cast<PrataNode>()
            .ToDictionary(node => node.GUID, node => node);

        public static GraphSaveUtilities GetInstance(PrataGraphView graphView)
        {
            return new GraphSaveUtilities
            {
                _targetGraphView = graphView
            };
        }

        public void SaveGraph(string filename)
        {
            var path = $"{PrataConstants.FOLDER_GRAPH}/{filename}.asset";
            var prevAsset = Resources.Load<DialogContainer>(path);
            if (prevAsset == null)
            {
                SaveNewGraph(path);
            }
            else
            {
                OverwriteGraph(prevAsset);
            }
        }

        private void SaveNewGraph(string path)
        {
            Debug.Log($"New graph {path}");
            var dialogContainer = ScriptableObject.CreateInstance<DialogContainer>();

            SavePorts(dialogContainer);

            SaveNodes(dialogContainer);

            // creates an Resources folder if there's none
            GenerateFolders();

            AssetDatabase.CreateAsset(dialogContainer, path);
            EditorUtility.SetDirty(dialogContainer);
            AssetDatabase.SaveAssets();

            Selection.activeObject = dialogContainer;
            SceneView.FrameLastActiveSceneView();
        }

        public void OverwriteGraph(DialogContainer dialogContainer)
        {
            Debug.Log($"Overwriting {AssetDatabase.GetAssetPath(dialogContainer)}");
            SavePorts(dialogContainer);

            SaveNodes(dialogContainer);

            EditorUtility.SetDirty(dialogContainer);
            AssetDatabase.SaveAssets();

            Selection.activeObject = dialogContainer;
            SceneView.FrameLastActiveSceneView();
        }

        private void SaveNodes(DialogContainer dialogContainer)
        {
            foreach (var prataNode in Nodes.Values.Where(node => node.DialogType != NodeTypes.Start))
            {
                var dialogNode = new DialogNodeData(prataNode);
                var index = dialogContainer.DialogNodes.FindIndex(p => p.Guid == prataNode.GUID);
                if (index >= 0)
                {
                    // update
                    dialogContainer.DialogNodes[index] = dialogNode;
                }
                else
                {
                    // new one
                    dialogContainer.DialogNodes.Add(dialogNode);
                }
            }
        }

        private void SavePorts(DialogContainer dialogContainer)
        {
            var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
            foreach (var connectedPort in connectedPorts)
            {
                var outputNode = connectedPort.output.node as PrataNode;
                var inputNode = connectedPort.input.node as PrataNode;

                if (outputNode != null && inputNode != null)
                {
                    NodeLinkData linkData = new NodeLinkData
                    {
                        BaseNodeGuid = outputNode.GUID,
                        PortName = connectedPort.output.portName,
                        TargetNodeGuid = inputNode.GUID
                    };

                    var index = dialogContainer.NodeLinks
                        .FindIndex(l => l.BaseNodeGuid == outputNode.GUID &&
                                        l.TargetNodeGuid == inputNode.GUID);

                    if (index >= 0)
                    {
                        // update
                        dialogContainer.NodeLinks[index] = linkData;
                    }
                    else
                    {
                        // new one
                        dialogContainer.NodeLinks.Add(linkData);
                    }
                }
            }
        }

        public void LoadGraph(DialogContainer dialogContainer)
        {
            _containerCached = dialogContainer;
            if (_containerCached == null)
            {
                EditorUtility.DisplayDialog("File not found", "Target dialog graph does not exist", "Ok");
                return;
            }

            ClearGraph();

            RestoreNodes();

            ConnectNodes();
        }

        public void ClearAll()
        {
            _targetGraphView.DeleteElements(Nodes.Values.Where(t => t.DialogType != NodeTypes.Start));
            // remove edges connected to node
            _targetGraphView.DeleteElements(Edges);
        }

        private void ClearGraph()
        {
            // set the entry point
            Nodes.Values.First(x => x.DialogType == NodeTypes.Start).GUID =
                _containerCached.NodeLinks[0].BaseNodeGuid;

            ClearAll();
        }

        private void RestoreNodes()
        {
            foreach (var nodeData in _containerCached.DialogNodes)
            {
                var tempNode = _targetGraphView.RestoreNode(nodeData);
                _targetGraphView.AddElement(tempNode);
            }
        }

        private void ConnectNodes()
        {
            foreach (var node in Nodes.Values)
            {
                var connections = _containerCached.NodeLinks.Where(
                    x => x.BaseNodeGuid == node.GUID).ToList();

                foreach (var nodeLinkData in connections)
                {
                    var targetNodeGuid = nodeLinkData.TargetNodeGuid;
                    var targetNode = Nodes.Values.FirstOrDefault(x => x.GUID == targetNodeGuid);

                    if (targetNode == null) continue;

                    // we search for the corresponding port name
                    Port outputPort = node.outputContainer.Children()
                        .FirstOrDefault(child => child.Q<Port>().portName.Equals(nodeLinkData.PortName))
                        .Q<Port>();

                    if (outputPort != null)
                    {
                        LinkNodesTogether(outputPort, (Port)targetNode.inputContainer[0]);

                        targetNode.SetPosition(new Rect(
                            _containerCached.DialogNodes.First(x => x.Guid == targetNodeGuid).Position,
                            _targetGraphView.DefaultNodeSize));
                    }
                }
            }
        }

        private void LinkNodesTogether(Port outputSocket, Port inputSocket)
        {
            var tempEdge = new Edge
            {
                output = outputSocket,
                input = inputSocket
            };
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            _targetGraphView.Add(tempEdge);
        }
        
        public static void GenerateFolders()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            if (!AssetDatabase.IsValidFolder(PrataConstants.FOLDER_GRAPH))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Graphs");
            }
            
            if (!AssetDatabase.IsValidFolder(PrataConstants.FOLDER_INTERACTIONS_COMPLETE))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Interactions");
            }

            if (!AssetDatabase.IsValidFolder(PrataConstants.FOLDER_CHARACTERS_COMPLETE))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Characters");
            }
        }
        
        public static void CreateFirstCharacter(string newName = "")
        {
            // only if this is the first character we create a dummy one
            if (!DataUtilities.HasAnyCharacter()) return;

            if (!AssetDatabase.IsValidFolder(PrataConstants.FOLDER_CHARACTERS_COMPLETE))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Characters");
            }
            
            var character = ScriptableObject.CreateInstance<Character>();
                
            character.characterName = newName;
            
            AssetDatabase.CreateAsset(character, $"{PrataConstants.FOLDER_CHARACTERS_COMPLETE}/New Character.asset");
            EditorUtility.SetDirty(character);
            AssetDatabase.SaveAssets();
        }
    }
}