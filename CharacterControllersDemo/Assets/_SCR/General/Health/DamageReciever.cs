using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DamageReciever : MonoBehaviour, IDamageable
{
    [SerializeField] private IDamageable parent = null;

    [DisableInPlayMode] [SerializeField] private GameObject parentObject = null;

    [Header("Damage")]
    [SerializeField] private float damageMultiplier = 1.0f;

    private void Start() 
    {
        if (parentObject == null)
        {
            parent = transform.parent.GetComponentInParent<IDamageable>();
            Debug.LogError("[DamageReciever] Couldn't find IDamagable through GetComponentInParent, destroying self", gameObject);
            Destroy(this);
        }
        else
        {
            parent = parentObject.GetComponent<IDamageable>();

            if (parent == null)
            {
                Debug.LogError("[DamageReciever] Couldn't find IDamagable on parentObject, destroying self", gameObject);
                Destroy(this);
            }
        }
    }

    public void Damage(int pValue, GameObject pAttacker, Vector3 pPoint, Vector3 pDirection, Color pColor)
    {
        pValue = DamageMultipler(pValue);
        parent.Damage(pValue, pAttacker, pPoint, pDirection, pColor);
    }

    public void Damage(int pValue, GameObject pAttacker, Vector3 pPoint, Vector3 pDirection)
    {
        pValue = DamageMultipler(pValue);
        parent.Damage(pValue, pAttacker, pPoint, pDirection);
    }

    private int DamageMultipler(int pValue)
    {
        if (damageMultiplier != 1)
            return Mathf.RoundToInt((float)pValue * damageMultiplier);
        else
            return pValue;
    }

    // public void SetStatus(StatusEffects.status pStatus, float pSeconds)
    // {
    //     if (parent != null)
    //     {
    //         parent.SetStatus(pStatus, pSeconds);
    //     }
    // }

    public GameObject GetGameObject()
    {
        if (parent != null)
        {
            return parent.GetGameObject();
        }
        else
        {
            return gameObject;
        }
    }

    public IDamageable GetParentDamageable()
    {
        if (parent != null)
        {
            return parent.GetParentDamageable();
        }
        else
        {
            return this;
        }
    }
}
