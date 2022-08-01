using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OliverLoescher.Weapon
{
	public abstract class SOWeaponShootBase : ScriptableObject
	{
		public abstract void ShootStart(Action Shoot);
		public abstract void ShootEnd();
	}
}