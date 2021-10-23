using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField, Min(0.0f)] private float stamina = 100.0f;
    private float maxStamina = 100.0f;

    [Header("Recharge")]
    [SerializeField, Min(0.0f)] private float rechargeDelay = 1.0f;
    [SerializeField, Min(0.0f)] private float rechargeRate = 20.0f;

    [Space]
    [SerializeField] private BarValue UIBar = null;

    [FoldoutGroup("Events")] public UnityEvent OnStaminaOut;
    [FoldoutGroup("Events")] public UnityEvent OnStaminaIn;

    public bool isOut { get; private set; } = false;

    private void Start() 
    {
        maxStamina = stamina;
        UIBar.SetValue(1.0f);
    }

    public float GetStamina() { return stamina; }
    public float GetMaxStamina() { return maxStamina; }

    public void ModifyStamina(float pValue)
    {
        stamina += pValue;
        stamina = Mathf.Clamp(stamina, 0.0f, maxStamina);

        StopAllCoroutines();
        StartCoroutine(StaminaRoutine());

        if (isOut == false)
        {
            if (stamina == 0.0f)
                StaminaOut();
        }
        else
        {
            if (stamina == maxStamina)
                StaminaIn();
        }
        
        UIBar.SetValue(stamina / maxStamina);
    }

    private IEnumerator StaminaRoutine()
    {
        yield return new WaitForSeconds(rechargeDelay);

        while (stamina < maxStamina)
        {
            stamina += Time.deltaTime * rechargeRate;
            stamina = Mathf.Min(stamina, maxStamina);

            UIBar.SetValue(stamina / maxStamina);

            yield return null;
        }

        if (isOut)
            StaminaIn();
    }

    private void StaminaOut()
    {
        OnStaminaOut?.Invoke();
        isOut = true;

        UIBar.SetToggled(true);
    }

    private void StaminaIn()
    {
        OnStaminaIn?.Invoke();
        isOut = false;

        UIBar.SetToggled(false);
    }
}
