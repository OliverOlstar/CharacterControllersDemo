using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Creator
{
    public abstract class WeaponSettingsCollectionBase
	{
		protected WeaponSettings settings = null;
		protected WeaponFXGenerator fxGenerator = null;

		protected bool isOpen = false;

		private List<IWeaponSettingWidget> widgets = new List<IWeaponSettingWidget>();
		private List<string> subTabs = new List<string>();

		public void Initalize(WeaponSettings pSettings, WeaponFXGenerator pFX, bool pOpen)
		{
			isOpen = pOpen;
			settings = pSettings;
			fxGenerator = pFX;
			OnInitalize();
		}
		public abstract void OnInitalize();

		protected T CreateWidget<T>(in T prefab) where T : WeaponSettingWidgetBase
		{
			T widget = MonoUtil.Instantiate(prefab, prefab.transform.parent);
			widget.gameObject.SetActive(isOpen);
			widgets.Add(widget);
			return widget;
		}

		protected WeaponSettingsTab CreateTab(WeaponSettingsCollectionBase pCollection, string pName, bool pSelected)
		{
			subTabs.Add(pName);
			return settings.CreateTab(pCollection, pName, pSelected);
		}

		protected void RemoveTab(string pName)
		{
			subTabs.Remove(pName);
			settings.RemoveTab(pName);
		}

		public void HideWidgets(bool pHide)
		{
			isOpen = !pHide;
			foreach (IWeaponSettingWidget widget in widgets)
			{
				widget.gameObject.SetActive(isOpen);
			}
		}

		public virtual void Destroy()
		{
			foreach (string tab in subTabs)
			{
				settings.RemoveTab(tab);
			}
			subTabs.Clear();

			foreach (IWeaponSettingWidget widget in widgets)
			{
				widget.Destroy();
			}
			widgets.Clear();
		}
	}
}
