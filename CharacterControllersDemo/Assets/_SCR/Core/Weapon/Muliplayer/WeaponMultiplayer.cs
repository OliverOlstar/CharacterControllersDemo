using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

namespace OliverLoescher.Weapon.Multiplayer
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
			{
				photonView.RPC(nameof(WeaponMultiplayerReciever.RPC_ShootProjectile), RpcTarget.Others, pPoint, pForce);
			}
			base.SpawnProjectile(pPoint, pForce);
		}

		protected override void SpawnRaycast(Vector3 pPoint, Vector3 pForward)
		{
			if (IsValid())
			{
				photonView.RPC(nameof(WeaponMultiplayerReciever.RPC_ShootRaycast), RpcTarget.Others, pPoint, pForward);
			}
			base.SpawnRaycast(pPoint, pForward);
		}

		protected override void OnShootFailed()
		{
			if (IsValid())
			{
				photonView.RPC(nameof(WeaponMultiplayerReciever.RPC_ShootFailed), RpcTarget.Others);
			}
			base.OnShootFailed();
		}

		public bool IsValid() => PhotonNetwork.IsConnected && photonView.IsMine;
	}
}