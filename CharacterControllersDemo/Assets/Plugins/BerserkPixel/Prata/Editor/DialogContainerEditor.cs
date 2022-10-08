using System.Collections.Generic;
using System.Linq;
using BerserkPixel.Prata.Data;
using BerserkPixel.Prata.Utilities;
using UnityEditor;
using UnityEngine;

namespace BerserkPixel.Prata
{
    [CustomEditor(typeof(DialogContainer))]
    public class DialogContainerEditor : Editor
    {
        private static string SPACE = "    ";

        private DialogContainer _dialogContainer;
        private List<Character> _allCharacters => DataUtilities.GetAllCharacters();

        private Dictionary<string, List<NodeLinkData>> allLinks;
        Dictionary<string, bool> folded = new Dictionary<string, bool>();

        private void OnEnable()
        {
            _dialogContainer = (DialogContainer)target;
            allLinks = GetNodeLinksBasedOnId(_dialogContainer.NodeLinks);

            foreach (KeyValuePair<string, List<NodeLinkData>> nodeLink in allLinks)
                folded[nodeLink.Key] = true;
        }

        public override void OnInspectorGUI()
        {
            DrawNodeLinks(_dialogContainer.NodeLinks, _dialogContainer.DialogNodes);
        }

        private void DrawNodeLinks(List<NodeLinkData> nodeLinks, List<DialogNodeData> dialogNodes)
        {
            // we need to flatten the list because a link can have multiple choices       

            int nodeIndex = 1;

            GUIStyle nodeTitleStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontStyle = FontStyle.Bold
            };

            GUIStyle italicStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(),
                padding = new RectOffset(),
                fontStyle = FontStyle.Italic
            };

            GUIStyle characterSayingStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(),
                padding = new RectOffset(32, 32, 0, 0)
            };

            foreach (KeyValuePair<string, List<NodeLinkData>> nodeLink in allLinks)
            {
                DialogContent parent = GetDialogNodeFromLinkData(nodeLink.Key, dialogNodes);

                GUIContent parentTitle;
                if (parent == null)
                {
                    parentTitle = new GUIContent($"{nodeIndex} Start");
                }
                else
                {
                    string characterName = GetCharacterName(parent.characterID);
                    parentTitle = new GUIContent(
                        text: $"{nodeIndex} [{characterName}]: " + parent.DialogText
                    );
                }

                folded[nodeLink.Key] = EditorGUILayout.BeginFoldoutHeaderGroup(folded[nodeLink.Key], parentTitle, nodeTitleStyle);

                if (folded[nodeLink.Key])
                {
                    Rect rect = EditorGUILayout.BeginVertical();

                    int option = 1;

                    foreach (NodeLinkData linkData in nodeLink.Value)
                    {
                        EditorGUILayout.LabelField($"{SPACE}-> [Option {option}] " + linkData.PortName, italicStyle);

                        // next one
                        DialogContent dialogContent = GetDialogNodeFromLinkData(linkData.TargetNodeGuid, dialogNodes);

                        EditorGUILayout.LabelField(GetDialogHeader(dialogContent), characterSayingStyle);
                        EditorGUILayout.Separator();
                        //GUILayout.FlexibleSpace();

                        option++;
                    }

                    EditorGUILayout.EndVertical();
                    GUI.Box(rect, GUIContent.none);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                nodeIndex++;
            }
        }

        private GUIContent GetDialogHeader(DialogContent dialogContent)
        {
            string characterName = GetCharacterName(dialogContent.characterID);

            Sprite sprite = _allCharacters.Find(character => character.id.Equals(dialogContent.characterID))
                .GetFaceForEmotion(dialogContent.emotion);

            Texture2D texture = AssetPreview.GetAssetPreview(sprite);

            return new GUIContent(
                text: $" [{characterName}]: " + dialogContent.DialogText,
                image: texture,
                tooltip: $"({dialogContent.emotion.ToString()}) {characterName}: {dialogContent.DialogText}"
            );
        }

        private Dictionary<string, List<NodeLinkData>> GetNodeLinksBasedOnId(List<NodeLinkData> nodeLinks)
        {
            return nodeLinks
                .GroupBy(nodeLink => nodeLink.BaseNodeGuid)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToList());
        }

        private DialogContent GetDialogNodeFromLinkData(string baseGUID, List<DialogNodeData> dialogNodes)
        {
            return dialogNodes.Find(nodeData => nodeData.Guid.Equals(baseGUID))?.Content;
        }

        private string GetCharacterName(string characterGuid)
        {
            string content = _allCharacters.Find(character => character.id.Equals(characterGuid)).characterName;

            if (string.IsNullOrEmpty(content))
            {
                return "Name Not Set";
            }
            else
            {
                return content;
            }
        }
    }
}