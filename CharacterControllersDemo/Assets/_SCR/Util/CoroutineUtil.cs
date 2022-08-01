using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
	public class CoroutineUtil : MonoBehaviourSingleton<CoroutineUtil>
	{
		private void Awake()
		{
			DontDestroyOnLoad(this);
		}
		private void OnDestroy()
		{
			StopAllCoroutines();
		}

		public static Coroutine Start(in IEnumerator pEnumerator)
		{
			return Instance.StartCoroutine(pEnumerator);
		}
		public static void Stop(ref Coroutine pCoroutine)
		{
			if (pCoroutine == null)
			{
				return;
			}
			Instance.StopCoroutine(pCoroutine);
			pCoroutine = null;
		}
	}
}