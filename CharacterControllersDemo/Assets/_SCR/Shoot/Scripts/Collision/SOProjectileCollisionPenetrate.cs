using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	[CreateAssetMenu(menuName = "ScriptableObject/Weapon/Collision/Penetrate")]
	public class SOProjectileCollisionPenetrate : SOProjectileCollisionBase
	{
		public override bool DoCollision(Projectile projectile, Collider other, ref bool canDamage, ref bool activeSelf)
		{
			if (other.gameObject.isStatic)
			{
				projectile.rigidbody.isKinematic = true;
				canDamage = false;
				DoCollision(projectile, other);
				return true;
			}
			return false;
		}
	}
}
