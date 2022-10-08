using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponSettingsCollectionSpreadCircle : WeaponSettingsCollectionBase
	{
		public SOWeaponSpreadCircle spread = null;

		public WeaponSettingsCollectionSpreadCircle(SOWeaponSpreadCircle pSpread) => spread = pSpread;

		public override void OnInitalize()
		{
			CreateWidget(settings.FloatWidget).Initalize("Increase", fxGenerator.scale, 0.5f, 0, 1, 0.1f, "F1", false, OnIncreaseChanged);
			CreateWidget(settings.FloatWidget).Initalize("Decrease", fxGenerator.scale, 1, 0, 2, 0.2f, "F1", false, OnDecreaseChanged);
			CreateWidget(settings.FloatWidget).Initalize("Radius", fxGenerator.scale, 1, 1, 9, 1, "F0", false, OnRadiusChanged);
			CreateWidget(settings.FloatWidget).Initalize("Radius Max", fxGenerator.scale, 1, 1, 9, 1, "F0", false, OnRadiusMaxChanged);
		}

		private void OnIncreaseChanged(float pValue, int pModID)
		{
			spread.spreadIncrease = (int)pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		}
		private void OnDecreaseChanged(float pValue, int pModID)
		{
			spread.spreadDecrease = (int)pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		}
		private void OnRadiusChanged(float pValue, int pModID)
		{
			spread.spreadRadius = (int)pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		}
		private void OnRadiusMaxChanged(float pValue, int pModID)
		{
			spread.spreadRadiusMax = (int)pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		}
	}

	public class WeaponSettingsCollectionSpreadSquare : WeaponSettingsCollectionBase
	{
		public SOWeaponSpreadSquare spread = null;

		public WeaponSettingsCollectionSpreadSquare(SOWeaponSpreadSquare pSpread) => spread = pSpread;

		public override void OnInitalize()
		{
			CreateWidget(settings.FloatWidget).Initalize("Increase", fxGenerator.scale, 0.5f, 0, 1, 0.1f, "F1", false, OnIncreaseChanged);
			CreateWidget(settings.FloatWidget).Initalize("Decrease", fxGenerator.scale, 1, 0, 2, 0.2f, "F1", false, OnDecreaseChanged);
			CreateWidget(settings.Vector2Widget).Initalize("Radius", fxGenerator.scale, Vector2.one, 1, 5, 1, "F0", false, OnVectorChanged);
			CreateWidget(settings.Vector2Widget).Initalize("Radius Max", fxGenerator.scale, Vector2.one, 1, 5, 1, "F0", false, OnVectorMaxChanged);
		}

		private void OnIncreaseChanged(float pValue, int pModID)
		{
			spread.spreadIncrease = (int)pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		}
		private void OnDecreaseChanged(float pValue, int pModID)
		{
			spread.spreadDecrease = (int)pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		}
		private void OnVectorChanged(Vector2 pValue, int pModID)
		{
			spread.spreadVector = pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * (((pValue.x + pValue.y) / 5) + 1));
		}
		private void OnVectorMaxChanged(Vector2 pValue, int pModID)
		{
			spread.spreadVectorMax = pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * (((pValue.x + pValue.y) / 5) + 1));
		}
	}

	public class WeaponSettingsCollectionSpreadEllipse : WeaponSettingsCollectionBase
	{
		public SOWeaponSpreadEllipse spread = null;

		public WeaponSettingsCollectionSpreadEllipse(SOWeaponSpreadEllipse pSpread) => spread = pSpread;

		public override void OnInitalize()
		{
			CreateWidget(settings.FloatWidget).Initalize("Increase", fxGenerator.scale, 0.5f, 0, 1, 0.1f, "F1", false, OnIncreaseChanged);
			CreateWidget(settings.FloatWidget).Initalize("Decrease", fxGenerator.scale, 1, 0, 2, 0.2f, "F1", false, OnDecreaseChanged);
			CreateWidget(settings.Vector2Widget).Initalize("Radius", fxGenerator.scale, Vector2.one, 1, 5, 1, "F0", false, OnVectorChanged);
			CreateWidget(settings.Vector2Widget).Initalize("Radius Max", fxGenerator.scale, Vector2.one, 1, 5, 1, "F0", false, OnVectorMaxChanged);
		}

		private void OnIncreaseChanged(float pValue, int pModID)
		{
			spread.spreadIncrease = (int)pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		}
		private void OnDecreaseChanged(float pValue, int pModID)
		{
			spread.spreadDecrease = (int)pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * ((pValue / 5) + 1));
		}
		private void OnVectorChanged(Vector2 pValue, int pModID)
		{
			spread.spreadVector = pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * (((pValue.x + pValue.y) / 5) + 1));
		}
		private void OnVectorMaxChanged(Vector2 pValue, int pModID)
		{
			spread.spreadVectorMax = pValue;
			fxGenerator.scale.SetMod(pModID, Vector3.one * (((pValue.x + pValue.y) / 5) + 1));
		}
	}
}
