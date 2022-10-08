using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Creator
{
	public class WeaponFXGeneratorModColor : WeaponFXGeneratorModBase<Color>
    {
		protected override void OnInitalize()
		{

		}

		protected override void ApplyMods()
		{
			ParticleSystem.MainModule main = Particle.main;

			if (modifiers.Count == 0)
			{
				main.startColor = Color.white;
				return;
			}

			Color color = Color.white;
			float weight = 1 / modifiers.Count;
			foreach (Color mod in modifiers.Values)
			{
				color = Color.Lerp(color, mod, weight);
			}
			main.startColor = color;
		}
	}
}
