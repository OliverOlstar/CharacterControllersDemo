using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace OliverLoescher.WaveFunctionCollapse
{
    public class WFC_Generator : MonoBehaviour
    {
        [SerializeField]
        private int GRIDSIZE = 10;
        [SerializeField]
        private float PICKDELAY = 0.02f;

        [SerializeField]
        private List<WFC_Module> modules = new List<WFC_Module>();
        private WFC_Slot[,] slots = null;

        public static List<WFC_Slot> slotsToUpdate = new List<WFC_Slot>();

        [Button]
        public void Generate()
        {
            StopAllCoroutines();
            StartCoroutine(GenerateRoutine());
        }

        private IEnumerator GenerateRoutine()
        {
            while (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                child.SetParent(null);
                DestroyImmediate(child.gameObject);
            }

            slots = new WFC_Slot[GRIDSIZE, GRIDSIZE];
            for (int x = 0; x < GRIDSIZE; x++)
                for (int y = 0; y < GRIDSIZE; y++)
                {
                    slots[x, y] = new WFC_Slot();
                    slotsToUpdate.Add(slots[x, y]);
                }
            for (int x = 0; x < GRIDSIZE; x++)
                for (int y = 0; y < GRIDSIZE; y++)
                {
                    slots[x, y].Initialize(
                        y == GRIDSIZE - 1 ? null : slots[x, y + 1],
                        y == 0 ? null : slots[x, y - 1],
                        x == 0 ? null : slots[x - 1, y],
                        x == GRIDSIZE - 1 ? null : slots[x + 1, y],
                        modules, new Vector3(x, 0.0f, y), transform);
                }

            WFC_Slot slot = GetNext();
            while (slot != null)
            {
                slot.PickModule();
                slot = GetNext();
                yield return new WaitForSeconds(PICKDELAY);
            }
        }

        private WFC_Slot GetNext()
        {
            if (slotsToUpdate.Count == 0)
            {
                return null;
            }

            WFC_Slot smallest = null;
            int smallestSize = int.MaxValue;
            foreach (WFC_Slot slot in slotsToUpdate)
            {
                if (slot.isSet)
                {
                    Debug.LogError($"slot {slot.Name} is set but is also in slotsToUpdate, this should be here");
                    continue;
                }
                int moduleSize = slot.States.Count;
                if (moduleSize < smallestSize || (moduleSize == smallestSize && Random.value > 0.25f))
                {
                    smallest = slot;
                    smallestSize = moduleSize;
                }
            }
            return smallest;
        }
    }
}