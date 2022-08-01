using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	public abstract class SOProjectileCollisionBase : ScriptableObject
	{
		[SerializeField]
		protected AudioUtil.AudioPiece audio = null;

		public abstract bool DoCollision(Projectile projectile, Collider other, ref bool canDamage, ref bool activeSelf);
	}
}