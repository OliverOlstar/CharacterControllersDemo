﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using OliverLoescher;

[RequireComponent(typeof(Health))]
public class DamageFlash : MonoBehaviour
{
    private Health health = null;

    [SerializeField] [DisableInPlayMode] private Renderer[] renderers;
    private Color[] initalColors;

    [Header("Time")]
    [SerializeField] [Range(0.001f, 1.0f)] private float flashSeconds = 0.1f;
    [SerializeField] [Range(0.001f, 5.0f)] private float flashDeathSeconds = 0.1f;

    private void Start()
    {
        health = GetComponent<Health>();
        health.onValueLowered.AddListener(OnDamaged);
        // health.onDamagedShield += OnDamagedShield;
        health.onValueRaised.AddListener(OnHealed);
        health.onValueOut.AddListener(OnDeath);

        initalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                initalColors[i] = renderers[i].material.color;
            }
        }
    }
    
    // public void OnDamagedHealth(GameObject pSender, int pHealth, int pChange) { OnDamagedHealth(); }
    public void OnDamaged(float pValue) { SetColor(health.damageColor, flashSeconds); }
    // public void OnDamagedShield(GameObject pSender, int pHealth, int pChange) { OnDamagedShield(); }
    // public void OnDamagedShield() { SetColor(health.shieldDamageColor, flashSeconds); }
    // public void OnHealed(GameObject pSender, int pHealth, int pChange) { OnHealed(); }
    public void OnHealed(float pValue) { SetColor(health.healColor, flashSeconds); }
    public void OnDeath(GameObject pSender, int pHealth, int pChange) { OnDeath(); }
    public void OnDeath() { SetColor(health.deathColor, flashDeathSeconds); }

    public void SetColor(Color pColor, float pSeconds)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                renderers[i].material.color = pColor;
            }
        }

        CancelInvoke();
        Invoke(nameof(ResetColor), pSeconds);
    }

    private void ResetColor()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = initalColors[i];
        }
    }
}
