using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	[CreateAssetMenu(menuName = "ScriptableObject/Weapon/Collision/Destroy")]
	public class SOProjectileCollisionDestroy : SOProjectileCollisionBase
	{
		public override bool DoCollision(Projectile projectile, Collider other, ref bool canDamage, ref bool activeSelf)
		{
			projectile.rigidbody.isKinematic = true;
			canDamage = false;
			return true;
		}
	}
}
