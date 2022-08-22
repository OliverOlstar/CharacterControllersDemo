using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.Weapon.Multiplayer
{
	[CreateAssetMenu(menuName = "ScriptableObject/Weapon/Weapon Multiplayer Data")]
	public class SOWeaponMultiplayer : SOWeapon
	{
		[Title("Multiplayer")]
		public GameObject dummyPrefab = null;
	}
}