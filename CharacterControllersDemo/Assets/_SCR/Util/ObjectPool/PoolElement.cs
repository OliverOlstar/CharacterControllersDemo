using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolElement : MonoBehaviour
{
	private Transform parent = null;

	public virtual void Init(Transform pParent)
	{
		parent = pParent;
	}

	public virtual void ReturnToPool()
	{
		ObjectPoolDictionary.Return(gameObject, this, parent);
	}

	public virtual void OnExitPool()
	{

	}

	private void OnDestroy() 
	{
		ObjectPoolDictionary.ObjectDestroyed(gameObject, this);
	}
}