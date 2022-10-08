using System;
using System.Collections.Generic;
using UnityEngine;

namespace BerserkPixel.Prata
{
    [Serializable]
    public class Dialog
    {
        public string guid;
        public Character character;
        public ActorsEmotions emotion;
        public string text;
        public bool isFacingRight = true;
        public List<string> choices;

        public Sprite GetImage() => character.GetFaceForEmotion(emotion);
    }
}