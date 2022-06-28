using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
	public class ParticlePoolElement : PoolElement
	{
		[SerializeField]
		private ParticleSystem particle = null;

		public override void Init(string pPoolKey, Transform pParent)
		{
			base.Init(pPoolKey, pParent);
		}

		private void Update()
		{
			if (!particle.IsAlive())
			{
				ReturnToPool();
			}
		}

		public override void ReturnToPool()
		{
			base.ReturnToPool();
			particle.Stop();
		}

		public override void OnExitPool()
		{
			base.OnExitPool();
		}
	}
}