using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponSettingsCollectionBasic : WeaponSettingsCollectionBase
	{
		protected const string SPREADTABNAME = "Spread";
		protected const string SHOOTSTARTTABNAME = "Start";
		protected const string SHOOTYPETABNAME = "Start";

		public override void OnInitalize()
		{
			CreateWidget(settings.FloatWidget).Initalize("Per Shot", fxGenerator.color, 1, 1, 9, 1, "F0", false, OnProjectilesPerShotChanged);
			CreateWidget(settings.DropdownWidget).Initalize(SPREADTABNAME, fxGenerator.scale, OnSpreadChanged, "None", "Square", "Circle", "Ellipse");
			CreateWidget(settings.DropdownWidget).Initalize(SHOOTSTARTTABNAME, fxGenerator.scale, OnShootStartChanged, "Instant", "Limited", "Charge");
			CreateWidget(settings.DropdownWidget).Initalize(SHOOTYPETABNAME, fxGenerator.scale, OnShootTypeChanged, "Single", "Burst", "Auto");
		}

		private void OnProjectilesPerShotChanged(float pValue, int pModID)
		{
			settings.weapon.projectilesPerShot = (int)pValue;
			fxGenerator.color.SetMod(pModID, Color.Lerp(Color.white, Color.red, (pValue - 1) / 9));
		}

		private void OnSpreadChanged(int pValue, int pModID)
		{
			switch (pValue)
			{
				case 0:
					SOWeaponSpreadNone none = MonoUtil.Instantiate(new SOWeaponSpreadNone());
					settings.weapon.spread = none;
					fxGenerator.scale.SetMod(pModID, Vector3.zero);
					RemoveTab(SPREADTABNAME);
					break;
				case 1:
					SOWeaponSpreadSquare square = MonoUtil.Instantiate(new SOWeaponSpreadSquare());
					settings.weapon.spread = square;
					fxGenerator.scale.SetMod(pModID, new Vector3(0.0f, 1.0f, 0.0f));
					CreateTab(new WeaponSettingsCollectionSpreadSquare(square), SPREADTABNAME, false);
					break;
				case 2:
					SOWeaponSpreadCircle circle = MonoUtil.Instantiate(new SOWeaponSpreadCircle());
					settings.weapon.spread = circle;
					fxGenerator.scale.SetMod(pModID, Vector3.one * -0.5f);
					CreateTab(new WeaponSettingsCollectionSpreadCircle(circle), SPREADTABNAME, false);
					break;
				case 3:
					SOWeaponSpreadEllipse ellipse = MonoUtil.Instantiate(new SOWeaponSpreadEllipse());
					settings.weapon.spread = ellipse;
					fxGenerator.scale.SetMod(pModID, Vector3.one * 0.5f);
					CreateTab(new WeaponSettingsCollectionSpreadEllipse(ellipse), SPREADTABNAME, false);
					break;
			}
		}

		private void OnShootStartChanged(int pValue, int pModID)
		{
			switch (pValue)
			{
				case 0:
					SOWeaponShootStartInstant instant = MonoUtil.Instantiate(new SOWeaponShootStartInstant());
					settings.weapon.shootStart = instant;
					fxGenerator.scale.SetMod(pModID, Vector3.zero);
					RemoveTab(SHOOTSTARTTABNAME);
					break;
				case 1:
					SOWeaponShootStartInstantLimitedByFirerate limited = MonoUtil.Instantiate(new SOWeaponShootStartInstantLimitedByFirerate());
					settings.weapon.shootStart = limited;
					fxGenerator.scale.SetMod(pModID, Vector3.zero);
					RemoveTab(SHOOTSTARTTABNAME);
					break;
				case 2:
					SOWeaponShootStartCharge charge = MonoUtil.Instantiate(new SOWeaponShootStartCharge());
					settings.weapon.shootStart = charge;
					fxGenerator.scale.SetMod(pModID, Vector3.one * -0.2f);
					CreateTab(new WeaponSettingsCollectionShootStartCharge(charge), SHOOTSTARTTABNAME, false);
					break;
			}
		}

		private void OnShootTypeChanged(int pValue, int pModID)
		{
			switch (pValue)
			{
				case 0:
					SOWeaponShootTypeSingle single = MonoUtil.Instantiate(new SOWeaponShootTypeSingle());
					settings.weapon.shootStart.shootType = single;
					fxGenerator.scale.SetMod(pModID, Vector3.zero);
					//RemoveTab(SHOOTSTARTTABNAME);
					break;
				case 1:
					SOWeaponShootTypeBurst burst = MonoUtil.Instantiate(new SOWeaponShootTypeBurst());
					settings.weapon.shootStart.shootType = burst;
					fxGenerator.scale.SetMod(pModID, new Vector3(0.0f, 0.0f, 0.2f));
					//RemoveTab(SHOOTSTARTTABNAME);
					break;
				case 2:
					SOWeaponShootTypeAuto auto = MonoUtil.Instantiate(new SOWeaponShootTypeAuto());
					settings.weapon.shootStart.shootType = auto;
					fxGenerator.scale.SetMod(pModID, Vector3.one * 0.2f);
					//CreateTab(new WeaponSettingsCollectionShootStartCharge(auto), SHOOTSTARTTABNAME, false);
					break;
			}
		}

		public override void Destroy()
		{
			settings.weapon.projectilesPerShot = 1;
			settings.weapon.spread = MonoUtil.Instantiate(new SOWeaponSpreadNone());
			base.Destroy();
		}
	}
}
