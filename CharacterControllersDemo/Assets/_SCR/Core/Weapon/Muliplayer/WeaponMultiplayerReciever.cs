using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace OliverLoescher.Weapon.Multiplayer
{
	public class WeaponMultiplayerReciever : Weapon
	{
		private SOWeaponMultiplayer mData = null;

		protected override void Init()
		{
			if (!(data is SOWeaponMultiplayer mData))
			{
				Debug.LogError($"[{nameof(WeaponMultiplayerReciever)}] data is not type {nameof(SOWeaponMultiplayer)}");
			}
		}

		[PunRPC]
		public void RPC_ShootFailed()
		{
			OnShootFailed();
		}

		[PunRPC]
		public void RPC_ShootProjectile(Vector3 pPoint, Vector3 pForce)
		{
			SpawnProjectile(pPoint, pForce);
		}

		[PunRPC]
		public void RPC_ShootRaycast(Vector3 pPoint, Vector3 pForward)
		{
			SpawnRaycast(pPoint, pForward);
		}

		protected override void SpawnProjectile(Vector3 pPoint, Vector3 pDirection)
		{
			base.SpawnProjectile(pPoint, pDirection);
		}
	}
}