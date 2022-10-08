using BerserkPixel.Prata.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BerserkPixel.Prata.Elements
{
    public class PrataMultipleChoiceNode : PrataNode
    {
        public override void Init(PrataGraphView graphView, Vector2 position, NodeTypes type)
        {
            if (allCharacters.Count <= 0 || allEmotions.Count <= 0)
            {
                Debug.LogError("Please be sure that you created at least 1 Character");
                return;
            }

            base.Init(graphView, position, NodeTypes.MultipleChoice);
            DialogType = NodeTypes.MultipleChoice;
            Choices.Add("New Choice 0");
        }

        public override void Draw()
        {
            if (allCharacters.Count <= 0 || allEmotions.Count <= 0)
            {
                Debug.LogError("Please be sure that you created at least 1 Character");
                return;
            }

            base.Draw();

            var addChoiceButton = PrataElementsUtilities.CreateButton("Add Choice", () =>
            {
                var outputPortCount = outputContainer.Query("connector").ToList().Count;
                CreateChoicePort($"New Choice {outputPortCount}");
                Choices.Add($"New Choice {outputPortCount}");
            });

            addChoiceButton.AddToClassList("prata-node_button");

            mainContainer.Insert(1, addChoiceButton);

            foreach (var choice in Choices) CreateChoicePort(choice);

            RefreshExpandedState();
            RefreshPorts();
        }

        private void CreateChoicePort(string textTitle)
        {
            var portChoice = this.CreatePort(textTitle);

            var deleteChoiceButton = PrataElementsUtilities.CreateButton("X", () => RemovePort(portChoice));

            deleteChoiceButton.AddToClassList("prata-node_button");

            var choiceTextField = PrataElementsUtilities.CreateTextField(textTitle, (evt) =>
            {
                var prev = evt.previousValue;
                var index = Choices.FindIndex(choice => choice == prev);
                Choices[index] = evt.newValue;
                portChoice.portName = evt.newValue;
            });

            choiceTextField.AddClasses(
                "prata-node_textfield",
                "prata-node_choice-textfield",
                "prata-node_textfield_hidden"
            );

            portChoice.Add(choiceTextField);
            portChoice.Add(deleteChoiceButton);

            outputContainer.Add(portChoice);
        }

        private void RemovePort(Port port)
        {
            _graphView.RemovePort(this, port);
        }
    }
}