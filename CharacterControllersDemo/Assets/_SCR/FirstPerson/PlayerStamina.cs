using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float stamina { get; private set; } = 100.0f;
    [Min(1.0f)] public float maxStamina = 100.0f;

    [SerializeField] private BarValue UIBar = null;

    public void ModifyStamina(float pValue)
    {
        
    }
}
