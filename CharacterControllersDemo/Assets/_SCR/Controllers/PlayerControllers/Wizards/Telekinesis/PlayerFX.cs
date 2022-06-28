using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.FX
{
	public class PlayerFX : MonoBehaviour
	{
		[SerializeField] private Health health = null;
		[Space, SerializeField] private ParticlePoolElement damageImpactPrefab = null;
		[SerializeField] private ParticlePoolElement damageLoopingPrefab = null;

		private void Start()
		{
		}

		private void OnEnable()
		{
			health.onDamageEvent += OnDamageEvent;
		}

		private void OnDisable()
		{
			health.onDamageEvent -= OnDamageEvent;
		}

		private void OnDamageEvent(float pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection)
		{
			GameObject fx = ObjectPoolDictionary.Get(damageImpactPrefab);
			fx.transform.position = pPoint;
			fx.transform.forward = pDirection;
			fx.SetActive(true);

			fx = ObjectPoolDictionary.Get(damageLoopingPrefab);
			fx.transform.position = pPoint;
			fx.transform.forward = pDirection;
			fx.SetActive(true);
		}
	}
}