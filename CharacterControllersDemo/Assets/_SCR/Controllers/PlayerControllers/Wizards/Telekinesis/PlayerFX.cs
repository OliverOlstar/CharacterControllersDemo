using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.FX
{
	public class PlayerFX : MonoBehaviour
	{
		[SerializeField] private Health health = null;
		[Space, SerializeField] private ParticlePoolElement damageDirectionalPrefab = null;
		[SerializeField] private ParticlePoolElement damagePrefab = null;
		[SerializeField] private ParticlePoolElement deathPrefab = null;
		//[SerializeField] private Transform deathPosition = null;

		private void Start()
		{
		}

		private void OnEnable()
		{
			health.onDamageEvent += OnDamageEvent;
			//health.onValueOutEvent += OnDeathEvent;
		}

		private void OnDisable()
		{
			health.onDamageEvent -= OnDamageEvent;
			//health.onValueOutEvent -= OnDeathEvent;
		}

		private void OnDamageEvent(float pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection)
		{
			GameObject fx = ObjectPoolDictionary.Get(damageDirectionalPrefab);
			fx.transform.position = pPoint;
			fx.transform.forward = pDirection;
			fx.SetActive(true);

			fx = ObjectPoolDictionary.Get(damagePrefab);
			fx.transform.position = pPoint;
			fx.transform.forward = pDirection;
			fx.SetActive(true);
		}

		//private void OnDeathEvent()
		//{
		//	GameObject fx = ObjectPoolDictionary.Get(damageDirectionalPrefab);
		//	fx.transform.position = deathPosition.position;
		//	fx.SetActive(true);
		//}
	}
}