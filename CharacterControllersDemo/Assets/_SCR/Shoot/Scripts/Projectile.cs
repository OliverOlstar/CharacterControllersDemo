using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.Weapon
{
	public class Projectile : PoolElement
	{
		[Required] public SOProjectile data = null;

		public new Rigidbody rigidbody = null;
		public Collider hitboxCollider = null;
		public Collider physicsCollider = null;
		public new ProjectileSFX audio = null;
		public GameObject sender = null;
		private SOTeam team = null;

		private bool canDamage = true;
		private bool activeSelf = true;
		private int currentFrame = 0;
		private int lastHitFrame = 0;
		private Collider lastHitCollider = null;

		[Header("Impact")]
		public ParticleSystem impactParticle = null;

		[Header("Floating Numbers")]
		[ColorPalette("UI"), SerializeField] private Color hitColor = new Color(1, 0, 0, 1);
		[ColorPalette("UI"), SerializeField] private Color critColor = new Color(1, 1, 0, 1);

		private Vector3 startPos = new Vector3();
		private Vector3 previousPosition = new Vector3();

		[SerializeField] private float spawnOffsetZ = 0;

		public override void ReturnToPool()
		{
			if (lastHitFrame != currentFrame) // if death of natural cause (lifeTime) and not because of a hit
			{
				PlayParticle(impactParticle, transform.position);
			}

			activeSelf = false;

			CancelInvoke();
			base.ReturnToPool();
		}

		public override void OnExitPool()
		{
			currentFrame = 0;
			rigidbody.isKinematic = false;
			rigidbody.useGravity = false;
			canDamage = true;
			lastHitCollider = null;
			hitboxCollider.enabled = true;
			physicsCollider.enabled = false;
			activeSelf = true;

			base.OnExitPool();
		}

		public void Init(Vector3 pPosition, Vector3 pDirection, SOTeam pTeam = null)
		{
			transform.position = pPosition;
			transform.rotation = Quaternion.LookRotation(pDirection);

			rigidbody.velocity = pDirection.normalized * RandUtil.Range(data.shootForce);
			transform.position += transform.forward * spawnOffsetZ;

			startPos = transform.position;
			previousPosition = transform.position;

			team = pTeam;
			Invoke(nameof(ReturnToPool), RandUtil.Range(data.lifeTime));
		}

		private void FixedUpdate() 
		{
			if (activeSelf == false)
				return;

			bool updateRot = false;
			if (data.bulletGravity > 0)
			{
				rigidbody.AddForce(Vector3.down * data.bulletGravity * Time.fixedDeltaTime, ForceMode.VelocityChange);
				updateRot = true;
			}

			if (updateRot)
			{
				transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
			}
		}

		private void Update() 
		{
			if (activeSelf == false)
				return;

			currentFrame++; // Used to ignore collision on first two frames

			if (currentFrame >= 1 && data.useRaycast) // Raycast Projectile
			{
				if (canDamage && Physics.Linecast(previousPosition, transform.position, out RaycastHit hit, data.layerMask, QueryTriggerInteraction.Ignore))
				{
					// if (data.bulletCollision != WeaponData.BulletCollision.Penetrate)
					//	 transform.position = hit.point;
					DoHitOther(hit.collider, hit.point);
				}
			}

			previousPosition = transform.position;
		}

		private void OnTriggerEnter(Collider other) 
		{
			if (activeSelf == false)
				return;
				
			DoHitOther(other, transform.position);
		}

	#region Hit/Damage
		private void DoHitOther(Collider other, Vector3 point)
		{
			if (canDamage == false || currentFrame < 1 || other.isTrigger || other == lastHitCollider || IsSender(other.transform))
			{
				return;
			}

			IDamageable damageable = other.GetComponent<IDamageable>();
			if (damageable != null)
			{
				bool isSameTeam = SOTeam.Compare(damageable.GetTeam(), team);
				if (isSameTeam && team.ignoreTeamCollisions)
				{
					return;
				}
				if (!isSameTeam || team.teamDamage)
				{
					Debug.Log($"[{nameof(Projectile)}] {nameof(DamageOther)}({other.name}, {damageable.GetGameObject().name}, {(damageable.GetTeam() == null ? "No Team" : damageable.GetTeam().name)})", other);
					DamageOther(damageable, point);
				}
			}

			lastHitFrame = currentFrame;
			lastHitCollider = other;

			PlayParticle(impactParticle, point);
			if (data.bulletCollision.DoCollision(this, other, ref canDamage, ref activeSelf))
			{
				ReturnToPool();
			}
		}

		private void DamageOther(IDamageable damageable, Vector3 point)
		{
			// Rigidbody otherRb = other.GetComponentInParent<Rigidbody>();
			// if (otherRb != null)
			//	 otherRb.AddForceAtPosition(rigidbody.velocity.normalized * data.hitForce, point);
			
			if (Random.value > data.critChance01)
			{
				damageable.Damage(data.damage, sender, transform.position, rigidbody.velocity);
			}
			else
			{
				damageable.Damage(Mathf.RoundToInt(data.critDamageMultiplier * data.damage), sender, transform.position, rigidbody.velocity, critColor);
			}

			// Audio
			if (audio != null)
				audio.OnCollision();
		}

		private bool IsSender(Transform other)
		{
			if (other == sender.transform)
			{
				return true;
			}
			if (other.parent == null)
			{
				return false;
			}
			return IsSender(other.parent);
		}
		#endregion

	#region Explosive
	//	 public virtual void DoExplosion(Vector3 pPoint) 
	//	 {
	//		 PlayParticle(explosiveParticle, pPoint); // Moves explosive & particle to point

	//		 explosive.ExplosionRadius = data.explosionRadius;
	//		 explosive.ExplosionDamage = data.explosionDamage;
	//		 explosive.ExplosionForce = data.explosionForce;
	//		 explosive.ExplosiveUpwardsModifier = data.explosiveUpwardsModifier;

	//		 explosive.DoExplosion();

	//		 // Audio
	//		 if (audio != null)
	//			 audio.OnExplode();
	//	 }
	#endregion

		private void PlayParticle(ParticleSystem pParticle, Vector3 pPosition)
		{
			if (pParticle != null)
			{
				pParticle.gameObject.SetActive(true);
				pParticle.Play();
				pParticle.transform.position = pPosition;
				pParticle.transform.SetParent(null);
			}
		}

		private void OnDrawGizmosSelected() 
		{
			Gizmos.color = Color.green;
			Vector3 endPos = transform.position + (transform.forward * -spawnOffsetZ);
			Gizmos.DrawLine(transform.position, endPos);
			Gizmos.DrawWireSphere(endPos, 0.01f);
		}
	}
}