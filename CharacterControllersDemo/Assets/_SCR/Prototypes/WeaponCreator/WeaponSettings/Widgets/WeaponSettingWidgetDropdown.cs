using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponSettingWidgetDropdown : WeaponSettingWidgetBase
	{
		[SerializeField]
		private TMP_Dropdown dropdown = null;

		private UnityAction<int, int> action;

		public WeaponSettingWidgetDropdown Initalize(string pName, IWeaponFXGeneratorMod pMod, UnityAction<int, int> pAction, params string[] pOptions)
		{
			InitalizeInternal(pName, pMod);

			action = pAction;

			dropdown.options.Clear();
			foreach (string option in pOptions)
			{
				dropdown.options.Add(new TMP_Dropdown.OptionData(option));
			}
			dropdown.onValueChanged.AddListener(OnValueChanged);

			OnValueChanged(0);
			return this;
		}

		private void OnValueChanged(int pValue)
		{
			action.Invoke(pValue, modID);
		}

		protected override void OnInitalizedDestroy()
		{
			dropdown.onValueChanged.RemoveListener(OnValueChanged);
		}
	}
}