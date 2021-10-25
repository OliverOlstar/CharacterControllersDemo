using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OliverLoescher;

[RequireComponent(typeof(Health))]
public class HealthDamageNumbers : MonoBehaviour
{
    private Health health;

    private void Awake() 
    {
        health = GetComponent<Health>();

        health.onValueLowered.AddListener(OnDamaged);
        health.onValueRaised.AddListener(OnHealed);
        health.onValueOut.AddListener(OnDeath);
    }

    public void OnDamaged(float pValue)
    {
        // Color c = pBool ? CritDamagedColor : DamagedColor;
        DisplayNumber(Mathf.CeilToInt(pValue), health.damageColor);
    }

    public void OnHealed(float pValue)
    {
        DisplayNumber(Mathf.CeilToInt(pValue), health.healColor);
    }

    public void OnDeath()
    {
        DisplayNumber("DEATH", health.deathColor);
    }

    public void DisplayNumber(int pValue, Color pColor)
    {
        DisplayNumber((Mathf.Abs(pValue) * 15).ToString(), pColor);
    }

    public void DisplayNumber(string pText, Color pColor)
    {
        DamageNumbers.Instance.DisplayNumber(pText, transform.position + Vector3.up * 2, pColor);
    }
}
