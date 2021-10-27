using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
    public class DamageReciever : MonoBehaviour, IDamageable
    {
        [SerializeField] private IDamageable parent = null;

        [DisableInPlayMode] [SerializeField] private GameObject parentObject = null;
        [SerializeField] private float damageMultiplier = 1.0f;

        private void Start() 
        {
            if (parentObject == null)
            {
                parent = transform.parent.GetComponentInParent<IDamageable>();

                if (parent == null)
                {
                    Debug.LogError("[DamageReciever] Couldn't find IDamagable through GetComponentInParent, destroying self", gameObject);
                    Destroy(this);
                }
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
            Debug.Log("[DamageReciever.cs] Damage()1 - Damage: " + pValue);
            parent.Damage(pValue, pAttacker, pPoint, pDirection, pColor);
        }

        public void Damage(int pValue, GameObject pAttacker, Vector3 pPoint, Vector3 pDirection)
        {
            pValue = DamageMultipler(pValue);
            Debug.Log("[DamageReciever.cs] Damage()2 - Damage: " + pValue);
            parent.Damage(pValue, pAttacker, pPoint, pDirection);
        }

        private int DamageMultipler(int pValue)
        {
            if (damageMultiplier != 1)
                return Mathf.RoundToInt((float)pValue * damageMultiplier);
            else
                return pValue;
        }

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
}