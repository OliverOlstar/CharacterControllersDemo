using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.UI
{
	public class MenuManager : MonoBehaviour
	{
#region Singleton
		public static MenuManager Instance = null;
		private void Awake() 
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else if (Instance != this)
			{
				Debug.LogError("[MenuManager.cs] Instance != null, destroying self", gameObject);
				Destroy(this);
			}
		}
#endregion	

		[SerializeField] private Menu[] menus = new Menu[0];
		[SerializeField, DisableInPlayMode] private Menu activeMenu = null;

		private void Start() 
		{
			foreach (Menu m in menus)
			{
				m.Toggle(m == activeMenu);
			}
		}

		public void OpenMenu(string pName)
		{
			pName = pName.ToLower();

			foreach (Menu m in menus)
			{
				if (m.menuName.ToLower() == pName)
				{
					OpenMenu(m);
					return;
				}
			}

			Debug.LogError("[MenuManager.cs] Failed to open/find menu.cs " + pName);
		}
		
		public void OpenMenu(Menu pMenu)
		{
			if (activeMenu != null)
				activeMenu.Toggle(false);

			activeMenu = pMenu;

			if (activeMenu != null)
				activeMenu.Toggle(true);
		}
	}
}