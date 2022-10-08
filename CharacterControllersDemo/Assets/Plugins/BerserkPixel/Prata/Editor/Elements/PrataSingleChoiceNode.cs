using BerserkPixel.Prata.Utilities;
using UnityEngine;

namespace BerserkPixel.Prata.Elements
{
    public class PrataSingleChoiceNode : PrataNode
    {
        public override void Init(PrataGraphView graphView, Vector2 position, NodeTypes type)
        {
            if (allCharacters.Count <= 0 || allEmotions.Count <= 0)
            {
                Debug.LogError("Please be sure that you created at least 1 Character");
                return;
            }

            base.Init(graphView, position, NodeTypes.SingleChoice);

            DialogType = NodeTypes.SingleChoice;
            Choices.Add("Next Dialog");
        }

        public override void Draw()
        {
            if (allCharacters.Count <= 0 || allEmotions.Count <= 0)
            {
                Debug.LogError("Please be sure that you created at least 1 Character");
                return;
            }

            base.Draw();
            foreach (var choice in Choices)
            {
                var portChoice = this.CreatePort(choice);

                outputContainer.Add(portChoice);
            }

            RefreshExpandedState();
            RefreshPorts();
        }
    }
}