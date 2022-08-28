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
		private PhotonView mPhotonView = null;
		private Dictionary<int, ProjectileMultiplayer> mActiveProjectiles = new Dictionary<int, ProjectileMultiplayer>();

		public bool IsValid => PhotonNetwork.IsConnected && mPhotonView.IsMine;

		protected override void Init()
		{
			mPhotonView = GetComponent<PhotonView>();
		}

		#region Self
		protected override Projectile SpawnProjectile(Vector3 pPoint, Vector3 pForce)
		{
			Projectile projectile = base.SpawnProjectile(pPoint, pForce);
			int id = projectile.gameObject.GetInstanceID();
			if (projectile is ProjectileMultiplayer projectileMultiplayer)
			{
				projectileMultiplayer.InitMultiplayer(id, this, true);
				mActiveProjectiles.Add(id, projectileMultiplayer);
			}

			if (IsValid)
			{
				mPhotonView.RPC(nameof(RPC_ShootProjectile), RpcTarget.Others, id, pPoint, pForce);
			}
			return projectile;
		}

		protected override void SpawnRaycast(Vector3 pPoint, Vector3 pForward)
		{
			base.SpawnRaycast(pPoint, pForward);
			if (IsValid)
			{
				mPhotonView.RPC(nameof(RPC_ShootRaycast), RpcTarget.Others, pPoint, pForward);
			}
		}

		protected override void OnShootFailed()
		{
			base.OnShootFailed();
			if (IsValid)
			{
				mPhotonView.RPC(nameof(RPC_ShootFailed), RpcTarget.Others);
			}
		}

		public void Projectile_DoLifeEnd(int pID, Vector3 pPosition)
		{
			if (mActiveProjectiles.Remove(pID) && IsValid)
			{
				mPhotonView.RPC(nameof(RPC_Projectile_DoLifeEnd), RpcTarget.Others, pID, pPosition);
			}
		}

		public void Projectile_DoCollision(int pID, Vector3 pPosition, bool pDidDamage)
		{
			if (mActiveProjectiles.Remove(pID) && IsValid)
			{
				mPhotonView.RPC(nameof(RPC_Projectile_DoCollision), RpcTarget.Others, pID, pPosition, pDidDamage);
			}
		}
		#endregion Self

		#region Other
		[PunRPC]
		public void RPC_ShootFailed()
		{
			OnShootFailed();
		}

		[PunRPC]
		public void RPC_ShootProjectile(int pID, Vector3 pPoint, Vector3 pForce)
		{
			Projectile projectile = base.SpawnProjectile(pPoint, pForce);
			if (!(projectile is ProjectileMultiplayer projectileMultiplayer))
			{
				LogError("ProjectilePrefab has Projectile.cs when it should have a ProjectileMultiplayer.cs, this will not work correctly.");
				return;
			}
			projectileMultiplayer.InitMultiplayer(pID, this, false);
			mActiveProjectiles.Add(pID, projectileMultiplayer);
		}

		[PunRPC]
		public void RPC_ShootRaycast(Vector3 pPoint, Vector3 pForward)
		{
			SpawnRaycast(pPoint, pForward);
		}

		[PunRPC]
		public void RPC_Projectile_DoLifeEnd(int pID, Vector3 pPosition)
		{
			if (FuncUtil.TryGetAndRemove(ref mActiveProjectiles, pID, out ProjectileMultiplayer projectile))
			{
				projectile.transform.position = pPosition;
				projectile.ForceLifeEnd();
			}
		}

		[PunRPC]
		public void RPC_Projectile_DoCollision(int pID, Vector3 pPosition, bool pDidDamage)
		{
			if (FuncUtil.TryGetAndRemove(ref mActiveProjectiles, pID, out ProjectileMultiplayer projectile))
			{
				projectile.transform.position = pPosition;
				projectile.ForceCollision(pDidDamage);
			}
		}

		private void ForceProjectileCollision()
		{

		}
		#endregion
	}
}