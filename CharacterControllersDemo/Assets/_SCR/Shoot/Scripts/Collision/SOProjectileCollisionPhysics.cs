using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	[CreateAssetMenu(menuName = "ScriptableObject/Weapon/Collision/Physics")]
	public class SOProjectileCollisionPhysics : SOProjectileCollisionBase
	{
		public override bool DoCollision(Projectile projectile, Collider other, ref bool canDamage, ref bool activeSelf)
		{
			projectile.rigidbody.useGravity = true;
			projectile.hitboxCollider.enabled = false;
			projectile.physicsCollider.enabled = true;
			projectile.transform.position += projectile.rigidbody.velocity.normalized * -0.25f;
			activeSelf = false;
			DoCollision(projectile, other);
			return false;
		}
	}
}