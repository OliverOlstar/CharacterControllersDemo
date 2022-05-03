using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.GunGenerator
{
    public class PartPickingManager : MonoBehaviour
    {
        [System.Serializable]
        public class PartGroup
        {
            [Required] public string key = "";
            [AssetsOnly] public GameObject[] prefabs = new GameObject[0];

            public GameObject GetPrefab(float pLength, float pWidth)
            {
                int index = Random.Range(0, prefabs.Length);
                PartPickerPart ppp = prefabs[index].GetComponent<PartPickerPart>();

                if (pLength > 0 && pWidth > 0)
                {
                    // Check if fits surface area
                    int firstIndex = index;
                    while (ppp != null && (ppp.surfaceAreaLength > pLength || ppp.surfaceAreaWidth > pWidth))
                    {
                        // Check next
                        index++;
                        if (index == prefabs.Length)
                        {
                            index = 0;
                        }
                        
                        // If looped through all parts
                        if (index == firstIndex)
                        {
                            Debug.LogError("[PartPickingManager.PartGroup] No " + key + " fits in the bounds of " + pLength + " & " + pWidth);
                            return null;
                        }
                        
                        // Check next
                        ppp = prefabs[index].GetComponent<PartPickerPart>();
                    }
                }
                
                return prefabs[index];
            }
        }

        public static Dictionary<string, PartGroup> parts = new Dictionary<string, PartGroup>();
        [SerializeField, DisableInPlayMode] private PartGroup[] partGroups = new PartGroup[0];

        private void Awake() 
        {
            foreach (PartGroup part in partGroups)
            {
                part.key = part.key.ToLower();
                parts.Add(part.key, part);
            }
        }
    }
}