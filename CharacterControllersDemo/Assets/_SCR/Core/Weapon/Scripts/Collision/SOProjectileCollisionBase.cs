using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	public abstract class SOProjectileCollisionBase : ScriptableObject
	{
		[SerializeField]
		protected PoolElement particlePrefab = null;
		[SerializeField]
		protected AudioUtil.AudioPiece audio = null;
		[SerializeField, Min(0.0f)]
		protected float knockbackForce = 300.0f;

		public abstract bool DoCollision(Projectile projectile, Collider other, ref bool canDamage, ref bool activeSelf);
		protected void DoCollision(Projectile projectile, Collider other)
		{
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.AddForce(projectile.transform.forward * knockbackForce, ForceMode.Impulse);
			}
			ObjectPoolDictionary.Play(particlePrefab, projectile.transform.position, projectile.transform.rotation);
			audio.Play(projectile.audioSources);
		}
		public virtual void DrawGizmos(Projectile pProjectile) { }
	}
}