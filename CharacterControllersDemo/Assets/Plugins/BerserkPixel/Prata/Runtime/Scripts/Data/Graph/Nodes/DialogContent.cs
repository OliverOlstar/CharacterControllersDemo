using System;
using BerserkPixel.Prata.Elements;

namespace BerserkPixel.Prata.Data
{
    [Serializable]
    public class DialogContent
    {
        public string characterID;
        public ActorsEmotions emotion;
        public string DialogText = "New Dialog";

        public void Fill(PrataNode prataNode)
        {
            characterID = prataNode.Content.characterID;
            emotion = prataNode.Content.emotion;
            DialogText = prataNode.Content.DialogText;
        }
    }
}