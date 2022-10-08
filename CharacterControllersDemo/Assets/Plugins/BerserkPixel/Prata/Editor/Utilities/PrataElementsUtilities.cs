using System;
using System.Collections.Generic;
using BerserkPixel.Prata.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace BerserkPixel.Prata.Utilities
{
    public static class PrataElementsUtilities
    {
        public static Button CreateButton(string title, Action onClick = null)
        {
            var button = new Button(onClick)
            {
                text = title
            };
            return button;
        }

        public static Foldout CreateFoldout(string title, bool collapsed = false)
        {
            var foldout = new Foldout
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        public static Port CreatePort(
            this PrataNode node,
            string portName = "",
            Orientation orientation = Orientation.Horizontal,
            Direction direction = Direction.Output,
            Port.Capacity capacity = Port.Capacity.Single)
        {
            var port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));
            port.portName = portName;

            return port;
        }

        public static TextElement CreateTextElement(string value = null)
        {
            var textElement = new TextElement
            {
                text = value
            };

            return textElement;
        }

        public static TextField CreateTextField(
            string value = null,
            EventCallback<ChangeEvent<string>> onValueChanged = null
        )
        {
            var textTextField = new TextField
            {
                value = value,
            };

            if (onValueChanged != null) textTextField.RegisterValueChangedCallback(onValueChanged);

            return textTextField;
        }

        public static TextField CreateTextField(
            string value = null,
            string label = null,
            EventCallback<ChangeEvent<string>> onValueChanged = null
        )
        {
            var textTextField = new TextField
            {
                label = label,
                value = value,
            };

            if (onValueChanged != null) textTextField.RegisterValueChangedCallback(onValueChanged);

            return textTextField;
        }

        public static TextField CreateTextArea(
            string value = null,
            EventCallback<ChangeEvent<string>> onValueChanged = null
        )
        {
            var textArea = CreateTextField(value, onValueChanged);
            textArea.multiline = true;
            return textArea;
        }

        public static ToolbarMenu CreateDropDownMenu(string title = "")
        {
            return new ToolbarMenu { text = title };
        }

        public static void AppendCharacterAction(
            this ToolbarMenu toolbarMenu,
            List<Character> characters,
            string savedCharacterId = null,
            Action<DropdownMenuAction> action = null
        )
        {
            if (string.IsNullOrEmpty(savedCharacterId))
            {
                toolbarMenu.text = characters[0].characterName;
            }
            else
            {
                var savedCharacter = characters.Find(c => c.id == savedCharacterId);
                toolbarMenu.text = savedCharacter.characterName;
            }

            foreach (var character in characters)
            {
                toolbarMenu.menu.AppendAction(
                    actionName: character.characterName,
                    action: action,
                    actionStatusCallback: a => DropdownMenuAction.Status.Normal,
                    userData: character);
            }
        }

        public static void AppendEmotionsAction(
            this ToolbarMenu toolbarMenu,
            List<ActorsEmotions> emotions,
            ActorsEmotions savedEmotion,
            Action<DropdownMenuAction> action = null
        )
        {
            var savedCharacter = emotions.Find(c => c.Equals(savedEmotion));
            toolbarMenu.text = savedCharacter.ToString();

            foreach (var emotion in emotions)
            {
                toolbarMenu.menu.AppendAction(
                    actionName: emotion.ToString(),
                    action: action,
                    actionStatusCallback: a => DropdownMenuAction.Status.Normal,
                    userData: emotion);
            }
        }

        public static ObjectField CreateObjectField<T>(
            string title = "",
            EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null
        )
        {
            var objectField = new ObjectField
            {
                objectType = typeof(T),
                label = title
            };

            if (onValueChanged != null)
                objectField.RegisterCallback(onValueChanged);

            return objectField;
        }
    }
}