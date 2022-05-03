using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Sirenix.OdinInspector;

namespace OliverLoescher.Weapon
{
    [RequireComponent(typeof(PhotonView))]
    public class WeaponMultiplayer : Weapon
    {
        private PhotonView photonView = null;

        protected override void Init()
        {
            photonView = GetComponent<PhotonView>();
        }
        
        protected override void SpawnProjectile(Vector3 pPoint, Vector3 pForce)
        {
            if (IsValid())
                photonView.RPC(nameof(RPC_ShootProjectile), RpcTarget.All, pPoint, pForce);
            else
                RPC_ShootProjectile(pPoint, pForce);
        }

        protected override void SpawnRaycast(Vector3 pPoint, Vector3 pForward)
        {
            if (IsValid())
                photonView.RPC(nameof(RPC_ShootRaycast), RpcTarget.All, pPoint, pForward);
            else
                RPC_ShootRaycast(pPoint, pForward);
        }

        protected override void OnShootFailed()
        {
            base.OnShootFailed();

            if (IsValid())
                photonView.RPC(nameof(RPC_ShootFailed), RpcTarget.Others);
            else
                RPC_ShootFailed();
        }

        [PunRPC]
        private void RPC_ShootFailed()
        {
            base.OnShootFailed();
        }

        [PunRPC]
        public void RPC_ShootProjectile(Vector3 pPoint, Vector3 pForce)
        {
            base.SpawnProjectile(pPoint, pForce);
        }

        [PunRPC]
        private void RPC_ShootRaycast(Vector3 pPoint, Vector3 pForward)
        {
            base.SpawnRaycast(pPoint, pForward);
        }
        public bool IsValid() => PhotonNetwork.IsConnected && photonView.IsMine;
    }
}