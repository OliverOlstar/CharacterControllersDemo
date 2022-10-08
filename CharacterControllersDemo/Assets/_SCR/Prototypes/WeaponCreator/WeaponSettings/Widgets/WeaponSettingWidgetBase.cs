using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace OliverLoescher.Weapon.Creator
{
	public interface IWeaponSettingWidget
	{
		public GameObject gameObject { get; }
		public Transform transform { get; }
		public void Destroy();
	}

    public abstract class WeaponSettingWidgetBase : MonoBehaviour, IWeaponSettingWidget
	{
		[SerializeField]
		private TMP_Text title = null;

		public int modID { get; private set; }
		public IWeaponFXGeneratorMod mod { get; private set; }

		private bool isInitalized = false;

		protected void InitalizeInternal(string pName, IWeaponFXGeneratorMod pMod)
		{
			title.text = pName;
			mod = pMod;
			modID = mod.CreateEmptyMod();
			isInitalized = true;
		}

		void IWeaponSettingWidget.Destroy()
		{
			if (isInitalized)
			{
				isInitalized = false;
				mod.RemoveMod(modID);
				OnInitalizedDestroy();
			}
			MonoUtil.Destroy(gameObject);
		}
		protected virtual void OnInitalizedDestroy() { }
	}
}
