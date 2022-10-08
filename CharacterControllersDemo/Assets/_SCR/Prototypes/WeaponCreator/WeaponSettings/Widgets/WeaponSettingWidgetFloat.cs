using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using OliverLoescher.UI;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponSettingWidgetFloat : WeaponSettingWidgetBase
	{
		[SerializeField]
		private Slider slider = null;
		[SerializeField]
		private SliderButtons buttons = null;
		[SerializeField]
		private SliderText text = null;

		private UnityAction<float, int> action;

		public WeaponSettingWidgetFloat Initalize(string pName, IWeaponFXGeneratorMod pMod, float pValue, float pMin, float pMax, float pDelta, string pTextFormat, bool pShowPlusText, UnityAction<float, int> pAction)
		{
			InitalizeInternal(pName, pMod);

			action = pAction;

			slider.value = pValue;
			slider.minValue = pMin;
			slider.maxValue = pMax;
			slider.onValueChanged.AddListener(OnValueChanged);
			buttons.SetDelta(pDelta, pDelta);
			text.textFormat = pTextFormat;
			text.addPlusIfPositive = pShowPlusText;

			OnValueChanged(pValue);
			return this;
		}

		private void OnValueChanged(float pValue)
		{
			action.Invoke(pValue, modID);
		}

		protected override void OnInitalizedDestroy()
		{
			slider.onValueChanged.RemoveListener(OnValueChanged);
		}
	}
}