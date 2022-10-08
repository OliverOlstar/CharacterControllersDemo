using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponSettingsCollectionShootStartCharge : WeaponSettingsCollectionBase
	{
		public SOWeaponShootStartCharge start = null;

		public WeaponSettingsCollectionShootStartCharge(SOWeaponShootStartCharge pStart) => start = pStart;

		public override void OnInitalize()
		{
			//CreateWidget(settings.FloatWidget).Initalize("Increase", fxGenerator.scale, 0.5f, 0, 1, 0.1f, "F1", false, OnIncreaseChanged);
		}

		//private void OnIncreaseChanged(float pValue, int pModID)
		//{
		//	start. = (int)pValue;
		//	fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		//}
	}
}
