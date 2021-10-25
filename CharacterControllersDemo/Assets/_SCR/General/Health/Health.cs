using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
    public class Health : CharacterValue, IDamageable
    {
        public delegate void HealthChangeEvent(GameObject pSender, int pHealth, int pChange);

        [Header("Death")]
        [SerializeField] private bool disableCollidersOnDeath = true;
        private Collider[] colliders = new Collider[0];

        [Header("Damage Numbers")]
        [SerializeField] private bool showDamageNumbers = true;
        [SerializeField] private bool showDeathText = true;

        [Space]
        [ColorPalette("UI")] public Color deathColor = Color.grey;
        [ColorPalette("UI")] public Color damageColor = Color.red;
        [ColorPalette("UI")] public Color healColor = Color.green;

        protected override void Start() 
        {
            base.Start();

            if (disableCollidersOnDeath)
            {
                colliders = GetComponentsInChildren<Collider>();
            }

            onValueOut.AddListener(Death);

            Init();
        }

        protected virtual void Init() { }

        public virtual void Damage(int pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection, Color pColor)
        {
            Modify(-pValue);
            if (value > 0.0f)
                ShowDamageNumber(pValue.ToString(), pPoint, pColor);
        }

        [Button()]
        public virtual void Damage(int pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection)
        {
            Modify(-pValue);
            if (value > 0.0f)
                ShowDamageNumber(pValue.ToString(), pPoint, damageColor);
        }

        private void ShowDamageNumber(string pString, Vector3 pPoint, Color pColor)
        {
            if (showDamageNumbers)
                DamageNumbers.Instance?.DisplayNumber(pString, pPoint + (Vector3.up * 0.65f), pColor);
        }

        public virtual void Death() 
        {
            if (showDeathText)
                DamageNumbers.Instance.DisplayNumber("Death", transform.position + Vector3.up, deathColor);

            if (disableCollidersOnDeath)
            {
                foreach (Collider c in colliders)
                {
                    c.enabled = false;
                }
            }
        }

        public GameObject GetGameObject() { return gameObject; }
        public IDamageable GetParentDamageable() { return this; }

        public void Respawn()
        {
            value = maxValue;
            UIBar.SetValue(1);
            isOut = false;
        }
    }
}