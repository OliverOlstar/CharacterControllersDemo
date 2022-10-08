using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Creator
{
    public class WeaponFXGeneratorModScale : WeaponFXGeneratorModBase<Vector3>
    {
		private Vector3 initalScale;

		protected override void OnInitalize()
		{
			initalScale = ParticleTransform.localScale;
		}

		public IEnumerable<float> xValues() { foreach (Vector3 v in modifiers.Values) { yield return v.x; } }
		public IEnumerable<float> yValues() { foreach (Vector3 v in modifiers.Values) { yield return v.y; } }
		public IEnumerable<float> zValues() { foreach (Vector3 v in modifiers.Values) { yield return v.z; } }

		protected override void ApplyMods()
		{
			Vector3 v = initalScale;
			v.x = Util.AddPercents(xValues());
			v.y = Util.AddPercents(yValues());
			v.z = Util.AddPercents(zValues());
			ParticleTransform.localScale = v;
		}
	}
}
