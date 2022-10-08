using System;
using UnityEngine;

namespace BerserkPixel.Prata
{
    [Serializable]
    public class Faces : ISerializationCallbackReceiver
    {
        [HideInInspector]
        public string name;

        public ActorsEmotions emotion = ActorsEmotions.Normal;

        public Sprite face;

        public void OnAfterDeserialize()
        {
            name = SplitCamelCase(emotion.ToString());
        }

        public void OnBeforeSerialize()
        {
            name = SplitCamelCase(emotion.ToString());
        }

        private string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
    }
}