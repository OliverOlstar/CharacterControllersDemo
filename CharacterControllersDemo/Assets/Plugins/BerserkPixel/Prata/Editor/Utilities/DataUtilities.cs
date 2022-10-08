using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BerserkPixel.Prata.Utilities
{
    public static class DataUtilities
    {
        public static bool HasAnyCharacter() => GetAllCharacters().Any(); 
        
        public static List<Character> GetAllCharacters()
        {
            var characters = Resources.LoadAll<Character>(PrataConstants.FOLDER_CHARACTERS);
            return characters.ToList();
        }

        public static List<ActorsEmotions> GetAllEmotions()
        {
            var emotions = Enum.GetValues(typeof(ActorsEmotions)).Cast<ActorsEmotions>().ToList();
            return emotions;
        }
    }
}