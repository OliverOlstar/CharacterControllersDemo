using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponSettings : MonoBehaviour
	{
		[SerializeField]
		private WeaponFXGenerator fxGenerator = null;
		[SerializeField]
		private Weapon displayWeapon = null;

		[Header("Widgets")]
		[SerializeField]
		private WeaponSettingsTab TabButton = null;
		public WeaponSettingWidgetFloat FloatWidget = null;
		public WeaponSettingWidgetVector2 Vector2Widget = null;
		public WeaponSettingWidgetDropdown DropdownWidget = null;

		[Header("Debug")]
		[InlineEditor, DisableInPlayMode, HideInEditorMode]
		public SOWeapon weapon = null;
		[InlineEditor, DisableInPlayMode, HideInEditorMode]
		public SOProjectile projectile = null;

		private Dictionary<string, WeaponSettingsTab> tabs = new Dictionary<string, WeaponSettingsTab>();

		private void Start()
		{
			weapon = Instantiate(new SOWeapon());
			projectile = Instantiate(new SOProjectile());
			CreateTab(new WeaponSettingsCollectionBasic(), "Base", true);
			displayWeapon.SetData(weapon);

			TabButton.gameObject.SetActive(false);
			FloatWidget.gameObject.SetActive(false);
			Vector2Widget.gameObject.SetActive(false);
			DropdownWidget.gameObject.SetActive(false);
		}

		public WeaponSettingsTab CreateTab(WeaponSettingsCollectionBase pCollection, string pName, bool pSelected)
		{
			RemoveTab(pName); // Try remove first
			WeaponSettingsTab tab = Instantiate(TabButton, TabButton.transform.parent);
			tab.gameObject.SetActive(true);
			tab.Initialize(this, pCollection, fxGenerator, pName, pSelected);
			tabs.Add(pName, tab);
			if (pSelected)
			{
				SetTab(tab, false);
			}
			return tab;
		}

		public void RemoveTab(string pName)
		{
			if (Util.TryGetAndRemove(ref tabs, pName, out WeaponSettingsTab tab))
			{
				tab.Destroy();
			}
		}

		private WeaponSettingsTab activeTab = null;
		public void SetTab(WeaponSettingsTab pToTab, bool pCallOpen = true)
		{
			if (activeTab != null)
			{
				activeTab.SetSelected(false);
			}
			activeTab = pToTab;
			if (pCallOpen)
			{
				activeTab.SetSelected(true);
			}
		}
	}
}
