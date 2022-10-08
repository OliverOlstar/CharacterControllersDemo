using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using OliverLoescher.UI;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponSettingWidgetVector2 : WeaponSettingWidgetBase
	{
		[Space, SerializeField]
		private Slider sliderA = null;
		[SerializeField]
		private SliderButtons buttonsA = null;
		[SerializeField]
		private SliderText textA = null;

		[Space, SerializeField]
		private Slider sliderB = null;
		[SerializeField]
		private SliderButtons buttonsB = null;
		[SerializeField]
		private SliderText textB = null;

		private Vector2 input = Vector2.zero;
		private UnityAction<Vector2, int> action;

		public WeaponSettingWidgetVector2 Initalize(string pName, IWeaponFXGeneratorMod pMod, Vector2 pValue, float pMin, float pMax, float pDelta, string pTextFormat, bool pShowPlusText, UnityAction<Vector2, int> pAction)
		{
			InitalizeInternal(pName, pMod);

			action = pAction;

			sliderA.value = pValue.x;
			sliderA.minValue = pMin;
			sliderA.maxValue = pMax;
			sliderA.onValueChanged.AddListener(OnSliderChanged);
			buttonsA.SetDelta(pDelta, pDelta);
			textA.textFormat = pTextFormat;
			textA.addPlusIfPositive = pShowPlusText;

			sliderB.value = pValue.y;
			sliderB.minValue = pMin;
			sliderB.maxValue = pMax;
			sliderB.onValueChanged.AddListener(OnSliderChanged);
			buttonsB.SetDelta(pDelta, pDelta);
			textB.textFormat = pTextFormat;
			textB.addPlusIfPositive = pShowPlusText;

			OnSliderChanged(0);
			return this;
		}

		private void OnSliderChanged(float _)
		{
			input.x = sliderA.value;
			input.y = sliderB.value;
			action.Invoke(input, modID);
		}

		protected override void OnInitalizedDestroy()
		{
			sliderA.onValueChanged.RemoveListener(OnSliderChanged);
			sliderB.onValueChanged.RemoveListener(OnSliderChanged);
		}
	}
}
