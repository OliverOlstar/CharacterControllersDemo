using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
	public class PoolElement : MonoBehaviour
	{
		public string poolKey { get; private set; } = string.Empty;
		public Transform parent { get; private set; } = null;

		public virtual void Init(string pPoolKey, Transform pParent)
		{
			poolKey = pPoolKey;
			parent = pParent;
		}

		public virtual void ReturnToPool()
		{
			ObjectPoolDictionary.Return(gameObject, this);
		}

		public virtual void OnExitPool()
		{

		}

		private void OnDestroy()
		{
			ObjectPoolDictionary.ObjectDestroyed(gameObject, this);
		}
	}
}