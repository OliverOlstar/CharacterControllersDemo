using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Photon.Pun;

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

        [Header("Photon")]
        [SerializeField] private PhotonView photonView = null;

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

        public virtual void Damage(float pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection, Color pColor)
        {
            Damage(pValue, pSender, pPoint, pDirection);
        }

        [Button()]
        public virtual void Damage(float pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection)
        {
            if (photonView != null && photonView.IsMine)
            {
                photonView.RPC("RPC_Damage", RpcTarget.Others, pValue);
                Modify(-pValue);
            }
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
            foreach (BarValue bar in UIBars)
                bar.InitValue(1);
            isOut = false;
        }

        [PunRPC]
        private void RPC_Damage(float pValue, PhotonMessageInfo pInfo)
        {
            // if (!photonView.IsMine)
            //     return;

            Debug.Log(pInfo.Sender.NickName + " Damaged " + photonView.Owner.NickName + ": " + pValue, gameObject);
            Modify(-pValue);
        }
    }
}