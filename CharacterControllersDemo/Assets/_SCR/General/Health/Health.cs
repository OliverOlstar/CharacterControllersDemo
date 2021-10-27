using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
    public class Health : CharacterValue, IDamageable
    {
        [Header("Death")]
        [SerializeField] private bool disableCollidersOnDeath = true;
        private Collider[] colliders = new Collider[0];

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
        }

        [Button()]
        public virtual void Damage(int pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection)
        {
            Modify(-pValue);
        }

        public virtual void Death() 
        {
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
            UIBar.InitValue(1);
            isOut = false;
        }
    }
}