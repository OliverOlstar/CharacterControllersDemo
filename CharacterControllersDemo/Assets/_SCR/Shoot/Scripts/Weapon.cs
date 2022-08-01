using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEditor;

namespace OliverLoescher.Weapon
{
	public class Weapon : MonoBehaviour
	{
		public enum MultiMuzzleType
		{
			FirstOnly,
			Loop,
			PingPong,
			Random,
			RandomNotOneAfterItself,
			RandomAllOnce
		}

		[Required] public SOWeapon data = null;
		public SOTeam team = null;
		[ShowIf("@muzzlePoints.Length > 1"), SerializeField] protected MultiMuzzleType multiMuzzleType = MultiMuzzleType.RandomNotOneAfterItself;
		public bool canShoot = true;

		private float nextCanShootTime = 0;
		private int burstFireActiveCount = 0;

		[Header("References")]
		[SerializeField] protected Transform[] muzzlePoints = new Transform[1];
		[SerializeField] private ParticleSystem muzzleFlash = null;
		[ShowIf("@muzzleFlash != null")] [SerializeField] private Vector3 muzzleFlashRelOffset = new Vector3();

		[Space]
		public GameObject sender = null;
		[SerializeField] private Rigidbody recoilBody = null;
		public AudioSourcePool sourcePool = null;

		[FoldoutGroup("Unity Events")] public UnityEvent OnShoot;
		[FoldoutGroup("Unity Events")] public UnityEvent OnFailedShoot;

		private void Start() 
		{
			if (sender == null)
			{
				sender = gameObject;
			}

			Init();
		}

		protected virtual void Init() {}

		[HideInInspector] public bool isShooting {get; private set;} = false;
		public void ShootStart()
		{
			data.startShoot.ShootStart(Shoot);
		}

		private void ShootStartDelayed()
		{
			switch (data.fireType)
			{
				case SOWeapon.FireType.Burst:
					if (isShooting)
						return;
					isShooting = true;

					burstFireActiveCount = data.burstFireCount - 1;
					break;

				case SOWeapon.FireType.Auto:
					isShooting = true;
					break;
			}
			Shoot();
		}

		public void ShootEnd()
		{
			if (data.fireType == SOWeapon.FireType.Auto)
			{
				isShooting = false;
			}
			data.startShoot.ShootEnd();
		}

		private void Update()
		{
			if (isShooting == true && nextCanShootTime <= Time.time)
			{
				Shoot();
			}
			else
			{
				data.spread.OnUpdate(Time.deltaTime, isShooting);
			}
		}

		public void Shoot()
		{
			if (canShoot)
			{
				// Firerate
				nextCanShootTime = Time.time + data.secondsBetweenShots;

				// If Burst
				if (data.fireType == SOWeapon.FireType.Burst)
				{
					burstFireActiveCount--;
					if (burstFireActiveCount < 1)
					{
						isShooting = false;
						return;
					}
					else
					{
						// Overrride Firerate
						nextCanShootTime = Time.time + data.secondsBetweenBurstShots;
					}
				}

				// Bullet
				Transform muzzle = GetMuzzle();
				SpawnBullet(muzzle);

				// Recoil
				if (recoilBody != null && data.recoilForce != Vector3.zero)
				{
					recoilBody.AddForceAtPosition(muzzle.TransformDirection(data.recoilForce), muzzle.position, ForceMode.VelocityChange);
				}

				// Spread
				data.spread.OnShoot();

				// Particles
				if (muzzleFlash != null)
				{
					if (muzzleFlash.transform.parent != muzzle)
					{
						muzzleFlash.transform.SetParent(muzzle);
						muzzleFlash.transform.localPosition = muzzleFlashRelOffset;
						muzzleFlash.transform.localRotation = Quaternion.identity;
					}
					muzzleFlash.Play();
				}

				// Audio
				if (sourcePool != null)
					data.shotSound.Play(sourcePool.GetSource());

				// Event
				OnShoot?.Invoke();
			}
			else
			{
				OnShootFailed();
			}
		}

		protected virtual void SpawnBullet(Transform pMuzzle)
		{
			for (int i = 0; i < data.bulletsPerShot; i++)
			{
				if (data.bulletType == SOWeapon.BulletType.Raycast)
				{
					SpawnRaycast(pMuzzle.position, pMuzzle.forward);
				}
				else
				{
					Vector3 dir = data.spread.ApplySpread(pMuzzle.forward);
					SpawnProjectile(pMuzzle.position, dir);
				}
			}
		}

		protected virtual void OnShootFailed()
		{
			// Audio
			data.failedShotSound.Play(sourcePool.GetSource());

			// Event
			OnFailedShoot?.Invoke();
		}

		protected virtual void SpawnProjectile(Vector3 pPoint, Vector3 pDirection)
		{
			// Spawn projectile
			GameObject projectile;
			projectile = ObjectPoolDictionary.Get(data.projectilePrefab);
			projectile.SetActive(true);

			Projectile projectileScript = projectile.GetComponentInChildren<Projectile>();
			projectileScript.sender = sender;
			projectileScript.Init(pPoint, pDirection);

			// Audio
			if (sourcePool != null)
				data.shotSound.Play(sourcePool.GetSource());

			// Event
			OnShoot?.Invoke();
		}

		protected virtual void SpawnRaycast(Vector3 pPoint, Vector3 pForward)
		{
			Vector3 dir = data.spread.ApplySpread(pForward);
			if (Physics.Raycast(pPoint, dir, out RaycastHit hit, data.range, data.layerMask, QueryTriggerInteraction.Ignore)) 
			{
				ApplyParticleFX(hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal), hit.collider);

				// push object if rigidbody
				Rigidbody hitRb = hit.collider.attachedRigidbody;
				// if (hitRb != null)
				//	 hitRb.AddForceAtPosition(data.hitForce * dir, hit.point);

				// Damage my script if possible
				IDamageable a = hit.collider.GetComponent<IDamageable>();
				// if (a != null)
				//	 a.Damage(data.damage, sender, hit.point, hit.normal);
			}
		}

		public virtual void ApplyParticleFX(Vector3 position, Quaternion rotation, Collider attachTo) 
		{
			if (data.hitFXPrefab) 
			{
				GameObject impact = Instantiate(data.hitFXPrefab, position, rotation) as GameObject;
			}
		}

		private int lastMuzzleIndex = 0;
		private bool muzzlePingPongDirection = true;
		private List<int> muzzleIndexList = new List<int>();
		protected Transform GetMuzzle()
		{
			switch (multiMuzzleType)
			{
				case MultiMuzzleType.Loop: // Loop ////////////////////////////////////////
					lastMuzzleIndex++;
					if (lastMuzzleIndex == muzzlePoints.Length)
						lastMuzzleIndex = 0;
					return muzzlePoints[lastMuzzleIndex];
					
				case MultiMuzzleType.PingPong: // PingPong ////////////////////////////////
					if (muzzlePingPongDirection)
					{
						lastMuzzleIndex++; // Forward
						if (lastMuzzleIndex == muzzlePoints.Length - 1)
							muzzlePingPongDirection = false;
					}
					else
					{
						lastMuzzleIndex--; // Back
						if (lastMuzzleIndex == 0)
							muzzlePingPongDirection = true;
					}
					return muzzlePoints[lastMuzzleIndex];

				case MultiMuzzleType.Random: // Random ////////////////////////////////////
					return muzzlePoints[Random.Range(0, muzzlePoints.Length)];

				case MultiMuzzleType.RandomNotOneAfterItself: // RandomNotOneAfterItself //
					int i = Random.Range(0, muzzlePoints.Length);
					if (i == lastMuzzleIndex)
					{
						// If is previous offset to new index
						i += Random.Range(1, muzzlePoints.Length);
						// If past max, loop back around
						if (i >= muzzlePoints.Length)
							i -= muzzlePoints.Length;
					}
					lastMuzzleIndex = i;
					return muzzlePoints[i];

				case MultiMuzzleType.RandomAllOnce: // RandomAllOnce //////////////////////
					if (muzzleIndexList.Count == 0)
					{
						// If out of indexes, refill
						for (int z = 0; z < muzzlePoints.Length; z++)
							muzzleIndexList.Add(z);
					}
					
					// Get random index from list of unused indexes
					int a = Random.Range(0, muzzleIndexList.Count);
					int b = muzzleIndexList[a];
					muzzleIndexList.RemoveAt(a);
					return muzzlePoints[b];

				default: // First Only ////////////////////////////////////////////////////
					return muzzlePoints[0];
			}
		}

		private void OnDrawGizmos() 
		{
	#if UNITY_EDITOR
			if (data == null)
				return;

			foreach (Transform m in muzzlePoints)
			{
				if (m == null)
					continue;
				data.spread.DrawGizmos(transform, m);
			}
	#endif
		}
	}
}