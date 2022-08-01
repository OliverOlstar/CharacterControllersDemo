using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	[CreateAssetMenu(menuName = "ScriptableObject/Weapon/Collision/Stick")]
	public class SOProjectileCollisionStick : SOProjectileCollisionBase
	{
		public override bool DoCollision(Projectile projectile, Collider other, ref bool canDamage, ref bool activeSelf)
		{
			projectile.rigidbody.isKinematic = true;
			if (other.gameObject.isStatic == false)
				projectile.transform.SetParent(other.transform);
			canDamage = false;
			activeSelf = false;
			audio.Play(projectile.audioSources);
			
			return false;
		}
	}
}
