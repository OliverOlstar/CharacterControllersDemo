using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponSettingsTab : MonoBehaviour
	{
		[SerializeField]
		private Button button = null;
		[SerializeField]
		private TMP_Text text = null;

		private WeaponSettings settings = null;
		public WeaponSettingsCollectionBase collection = null;

		public void Initialize(WeaponSettings pSettings, WeaponSettingsCollectionBase pCollection, WeaponFXGenerator pFX, string pName, bool pSelected)
		{
			settings = pSettings;
			collection = pCollection;
			pCollection.Initalize(pSettings, pFX, pSelected);
			text.text = pName;
			button.interactable = !pSelected;

			button.onClick.AddListener(OnButtonClicked);
		}

		public void OnButtonClicked() => settings.SetTab(this);

		public void SetSelected(bool pSelected)
		{
			button.interactable = !pSelected;
			collection.HideWidgets(!pSelected);
		}

		public void Destroy()
		{
			collection.Destroy();
			collection = null;
			button.onClick.RemoveListener(OnButtonClicked);
			Destroy(gameObject);
		}
    }
}