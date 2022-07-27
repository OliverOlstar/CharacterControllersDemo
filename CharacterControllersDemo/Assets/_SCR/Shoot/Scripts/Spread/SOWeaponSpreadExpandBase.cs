using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon
{
	public abstract class SOWeaponSpreadExpandBase : SOWeaponSpreadBase
	{
		[Range(0, 1)] public float spreadIncrease = 0.4f;
		[Range(0.0001f, 3)] public float spreadDecrease = 0.6f;

		protected float spread01 = 0.0f;

		public override void OnShoot()
		{
			spread01 = Mathf.Min(1, spread01 + spreadIncrease);
		}

		public override void OnUpdate(in float pDeltaTime, in bool pIsShooting)
		{
			spread01 = Mathf.Max(0, spread01 - (Time.deltaTime * spreadDecrease));
		}
	}
}