using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	public class SOWeaponShootInstantLimitedByFirerate : SOWeaponShootBase
	{
		private float nextCanShootTime = 0.0f;

		public override void ShootStart(Action Shoot)
		{
			if (nextCanShootTime <= Time.time)
			{
				Shoot.Invoke();
			}
		}
		public override void ShootEnd() 
		{

		}
	}
}