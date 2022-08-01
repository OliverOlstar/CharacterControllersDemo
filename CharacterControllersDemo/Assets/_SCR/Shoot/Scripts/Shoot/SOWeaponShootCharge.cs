using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	public class SOWeaponShootCharge : SOWeaponShootBase
	{
		[SerializeField]
		private float chargeSeconds = 0;

		private Coroutine coroutine = null;

		public override void ShootStart(Action Shoot)
		{
			CoroutineUtil.Stop(ref coroutine);
			coroutine = CoroutineUtil.Start(ShootDelayed());
		}
		public override void ShootEnd()
		{
			CoroutineUtil.Stop(ref coroutine);
		}

		public IEnumerator ShootDelayed()
		{
			yield return new WaitForSeconds(chargeSeconds);
		}
	}
}