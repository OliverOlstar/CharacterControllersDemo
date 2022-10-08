using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Multiplayer
{
    public class ProjectileMultiplayer : Projectile
    {
		private int mID = 0;
		public int ID => mID;
		private WeaponMultiplayer mWeapon = null;
		private bool mIsMine = false;

		public void InitMultiplayer(int pID, WeaponMultiplayer pWeapon, bool pIsMine)
		{
			mID = pID;
			mWeapon = pWeapon;
			mIsMine = pIsMine;

			if (!mIsMine)
			{
				hitboxCollider.enabled = false;
				canDamage = false;
			}
		}

		public override void ReturnToPool()
		{
			mID = 0;
			mIsMine = false;
			mWeapon = null;
			base.ReturnToPool();
		}

		protected override void OnTriggerEnter(Collider other)
		{
			if (mIsMine)
			{
				base.OnTriggerEnter(other);
			}
		}
		protected override void Update()
		{
			if (mIsMine)
			{
				base.Update();
			}
		}

		protected override void DoLifeEnd()
		{
			if (mIsMine)
			{
				mWeapon.Projectile_DoLifeEnd(mID, transform.position);
			}
			base.DoLifeEnd();
		}

		protected override bool DoHitOtherInternal(Collider pOther, bool pDidDamage)
		{
			if (mIsMine)
			{
				mWeapon.Projectile_DoCollision(ID, transform.position, pDidDamage);
			}
			return base.DoHitOtherInternal(pOther, pDidDamage);
		}

		public void ForceLifeEnd() => base.DoLifeEnd();
		public bool ForceCollision(bool pDidDamage) => base.DoHitOtherInternal(null, pDidDamage);
	}
}
