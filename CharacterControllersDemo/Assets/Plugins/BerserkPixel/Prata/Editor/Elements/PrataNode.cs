using System;
using System.Collections.Generic;
using BerserkPixel.Prata.Data;
using BerserkPixel.Prata.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BerserkPixel.Prata.Elements
{
    public class PrataNode : Node
    {
        public string GUID { get; set; } = Guid.NewGuid().ToString();
        public List<string> Choices { get; set; }

        public NodeTypes DialogType { get; set; }

        public DialogContent Content { get; set; }

        protected PrataGraphView _graphView;

        protected List<Character> allCharacters => DataUtilities.GetAllCharacters();
        protected List<ActorsEmotions> allEmotions => DataUtilities.GetAllEmotions();

        public void Init(PrataGraphView graphView, DialogNodeData nodeData, NodeTypes type)
        {
            _graphView = graphView;
            Choices = nodeData.Choices;
            Content = nodeData.Content;
            GUID = nodeData.Guid;
            DialogType = type;

            SetPosition(new Rect(nodeData.Position, Vector2.zero));

            mainContainer.AddToClassList("prata-node_main-container");
            extensionContainer.AddToClassList("prata-node_extension-container");
        }

        public virtual void Init(PrataGraphView graphView, Vector2 position, NodeTypes type)
        {
            _graphView = graphView;
            Choices = new List<string>();
            Content = new DialogContent
            {
                emotion = allEmotions[0],
                characterID = allCharacters[0].id
            };
            DialogType = type;

            SetPosition(new Rect(position, Vector2.zero));

            mainContainer.AddToClassList("prata-node_main-container");
            extensionContainer.AddToClassList("prata-node_extension-container");
        }

        public virtual void Draw()
        {
            title = Content.DialogText;

            // create the character who is talking
            var characterSelector = PrataElementsUtilities.CreateDropDownMenu("Characters");

            characterSelector.RegisterValueChangedCallback(evt =>
            {
                var index = allCharacters.FindIndex(character => character.characterName == evt.newValue);
                Content.characterID = allCharacters[index].id;
            });

            characterSelector.AppendCharacterAction(allCharacters, Content.characterID,
                action => { characterSelector.text = ((Character)action.userData).characterName; });
            mainContainer.Insert(1, characterSelector);

            var emotionSelector = PrataElementsUtilities.CreateDropDownMenu("Emotions");

            emotionSelector.RegisterValueChangedCallback((evt) =>
            {
                var index = allEmotions.FindIndex(emotion => emotion.ToString().Equals(evt.newValue));
                Content.emotion = allEmotions[index];
            });

            emotionSelector.AppendEmotionsAction(allEmotions, Content.emotion,
                action => { emotionSelector.text = ((ActorsEmotions)action.userData).ToString(); });
            mainContainer.Insert(2, emotionSelector);

            // Input

            var inputPort = this.CreatePort("Dialog Connection", direction: Direction.Input,
                capacity: Port.Capacity.Multi);

            inputContainer.Add(inputPort);

            // Foldout
            var customDataContainer = new VisualElement();
            customDataContainer.AddToClassList("prata-node_custom-data-container");

            var textFoldout = PrataElementsUtilities.CreateFoldout("Dialog text");

            var textTextField = PrataElementsUtilities.CreateTextField(Content.DialogText, (evt) =>
            {
                Content.DialogText = evt.newValue;
                title = evt.newValue;
            });

            textTextField.AddClasses(
                "prata-node_textfield",
                "prata-node_quote-textfield"
            );

            textFoldout.Add(textTextField);

            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);
        }

        public void RemoveFromChoices(string choice)
        {
            Choices.Remove(choice);
        }
    }
}