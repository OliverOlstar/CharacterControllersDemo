using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageFlash : MonoBehaviour
{
    [SerializeField] private Image image = null;
    private Color initalColor;
    [SerializeField] private OliverLoescher.Health health;

    [Header("Time")]
    [SerializeField] [Range(0.001f, 1.0f)] private float flashSeconds = 0.1f;

    private void Start()
    {
        initalColor = image.color;
        image.gameObject.SetActive(false);
    }
    
    public void OnDamaged() { SetColor(Color.Lerp(health.damageColor, health.deathColor, 1 - ((float)health.Get() / (float)health.GetMax())), flashSeconds); }
    public void OnHealed() { SetColor(health.healColor, flashSeconds); }

    public void SetColor(Color pColor, float pSeconds)
    {
        image.color = pColor;
        image.gameObject.SetActive(true);

        CancelInvoke();
        Invoke(nameof(ResetColor), pSeconds);
    }

    private void ResetColor()
    {
        image.color = initalColor;
        image.gameObject.SetActive(false);
    }
}
