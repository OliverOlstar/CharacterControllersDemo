using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.UI
{
    public class Menu : MonoBehaviour
    {
        public string menuName = "Untitled";

        public void Toggle(bool pOpen)
        {
            gameObject.SetActive(pOpen);
        }
    }
}