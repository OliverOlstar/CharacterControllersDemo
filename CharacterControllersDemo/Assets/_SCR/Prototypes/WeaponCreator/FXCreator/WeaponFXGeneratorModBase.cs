using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Weapon.Creator
{
	public interface IWeaponFXGeneratorMod
	{
		public int CreateEmptyMod();
		public void RemoveMod(int pID);
	}

    public abstract class WeaponFXGeneratorModBase<T> : IWeaponFXGeneratorMod
	{
		protected WeaponFXGenerator generator = null;
		protected Dictionary<int, T> modifiers = new Dictionary<int, T>();

		protected ParticleSystem Particle => generator.Particle;
		protected Transform ParticleTransform => generator.Particle.transform;

		public void Initalize(WeaponFXGenerator pGenerator)
		{
			generator = pGenerator;
			OnInitalize();
		}
		protected abstract void OnInitalize();

		public int CreateMod(T pScalar)
		{
			int id = Random.Range(int.MinValue, int.MaxValue); // Yeah... not 100% trust worthy
			modifiers.Add(id, pScalar);
			ApplyMods();
			Util.Log($"[{GetType()}] CreateMod()\nModifiers:", modifiers.Values);
			return id;
		}

		public int CreateEmptyMod()
		{
			int id = Random.Range(int.MinValue, int.MaxValue); // Yeah... not 100% trust worthy
			modifiers.Add(id, default);
			return id;
		}

		public void SetMod(int pID, T pScale)
		{
			if (!modifiers.ContainsKey(pID))
			{
				Debug.LogError($"[{GetType()}] id {pID} does not exist in modifiers. Call CreateMod() first.");
				return;
			}
			modifiers[pID] = pScale;
			ApplyMods();
			Util.Log($"[{GetType()}] SetMod({pID}, {pScale})\nModifiers:", modifiers.Values);
		}

		public void RemoveMod(int pId)
		{
			modifiers.Remove(pId);
			ApplyMods();
			Util.Log($"[{GetType()}] RemoveMod()\nModifiers:", modifiers.Values);
		}

		protected abstract void ApplyMods();
	}
}
