using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	public class SOWeaponShootInstant : SOWeaponShootBase
	{
		public override void ShootStart(Action Shoot)
		{
			Shoot.Invoke();
		}
		public override void ShootEnd() { }
	}
}