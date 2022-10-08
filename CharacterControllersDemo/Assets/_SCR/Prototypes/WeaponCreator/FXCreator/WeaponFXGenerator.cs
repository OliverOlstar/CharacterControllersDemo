using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponFXGenerator : MonoBehaviour
    {
		[SerializeField]
		private ParticleSystem particle = null;
		public ParticleSystem Particle => particle;

		public WeaponFXGeneratorModScale scale = new WeaponFXGeneratorModScale();
		public WeaponFXGeneratorModColor color = new WeaponFXGeneratorModColor();

		private void Awake()
		{
			scale.Initalize(this);
			color.Initalize(this);
		}
	}
}
