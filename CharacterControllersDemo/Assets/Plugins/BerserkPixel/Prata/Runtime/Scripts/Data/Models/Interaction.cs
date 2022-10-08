using System.Collections.Generic;
using System.Linq;
using BerserkPixel.Prata.Utilities;
using UnityEngine;

namespace BerserkPixel.Prata
{
    [CreateAssetMenu(fileName = "Interaction", menuName = "Prata/New Interaction", order = 1)]
    public class Interaction : ScriptableObject
    {
        public DialogContainer dialogContainer;

        private List<Character> allCharacters => DataUtilities.GetAllCharacters();

        private readonly List<Dialog> conversation = new List<Dialog>();

        private Dialog currentDialog;
        private bool hasStarted;

        private void OnEnable()
        {
            if (dialogContainer == null || allCharacters == null || allCharacters.Count <= 0) return;

            var lastCharacter = dialogContainer.DialogNodes[0].Content.characterID;

            // by default we start facing right
            var isFacingRight = true;
            foreach (var nodeData in dialogContainer.DialogNodes)
            {
                // this means we have a connection (it's a valid Node)
                var dialog = new Dialog
                {
                    guid = nodeData.Guid
                };

                var character =
                    allCharacters.Find(character => character.id == nodeData.Content.characterID);
                var emotion = nodeData.Content.emotion;

                dialog.character = character;
                dialog.emotion = emotion;
                dialog.text = nodeData.Content.DialogText;

                // If the character that's speaking changes, then we toggle it so it faces left
                if (lastCharacter != character.id)
                {
                    isFacingRight = !isFacingRight;
                    lastCharacter = character.id;
                }

                dialog.isFacingRight = isFacingRight;

                // we need to only show the choices that actually have a connection on the port
                var usedChoices = dialogContainer.NodeLinks
                    .Where(node => node.BaseNodeGuid == nodeData.Guid)
                    .Select(node => node.PortName)
                    .ToList();

                dialog.choices = usedChoices;

                conversation.Add(dialog);
            }

            currentDialog = null;
            hasStarted = false;
        }

        public Dialog GetCurrentDialog()
        {
            if (currentDialog == null)
            {
                currentDialog = conversation[0];
                hasStarted = true;
                return currentDialog;
            }

            currentDialog = GetNextDialog(currentDialog.guid);
            return currentDialog;
        }

        public Dialog GetCurrentDialogFromChoice(string dialogGuid, string choice)
        {
            currentDialog = GetNextDialog(dialogGuid, choice);
            return currentDialog;
        }

        private Dialog GetNextDialog(string guid)
        {
            var linkData = dialogContainer.NodeLinks
                .FirstOrDefault(link => link.BaseNodeGuid == guid);
            var nodeData = dialogContainer.DialogNodes.FirstOrDefault(node => node.Guid == linkData?.TargetNodeGuid);
            var next = conversation.FirstOrDefault(dialog => dialog.guid == nodeData?.Guid);

            return next;
        }

        private Dialog GetNextDialog(string guid, string choice)
        {
            var linkData = dialogContainer.NodeLinks
                .FirstOrDefault(link => link.BaseNodeGuid == guid && link.PortName == choice);
            var nodeData = dialogContainer.DialogNodes.FirstOrDefault(node => node.Guid == linkData?.TargetNodeGuid);
            var next = conversation.FirstOrDefault(dialog => dialog.guid == nodeData?.Guid);

            return next;
        }

        public bool HasAnyDialogLeft()
        {
            if (!hasStarted)
            {
                return true;
            }

            return currentDialog != null &&
                   dialogContainer.NodeLinks.Any(link => link.BaseNodeGuid == currentDialog.guid);
        }

        public void Reset()
        {
            if (!hasStarted) return;

            currentDialog = null;
            hasStarted = false;
        }
    }
}