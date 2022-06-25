using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
	public class CharacterDeath : MonoBehaviour
	{
		[SerializeField] private Health health = null;
		[SerializeField] private GameObject[] disableObjects = new GameObject[1];
		[SerializeField] private RootMotion.Dynamics.PuppetMaster puppetMaster = null;

		private void Start()
		{
			health.onValueOutEvent += OnDeath;
			health.onValueInEvent += OnRespawn;
			puppetMaster.SwitchToKinematicMode();
		}

		private void OnDeath()
		{
			puppetMaster.Kill();
			foreach (GameObject o in disableObjects)
			{
				o.SetActive(false);
			}
		}

		private void OnRespawn()
		{
			puppetMaster.Resurrect();
			puppetMaster.SwitchToKinematicMode();
			foreach (GameObject o in disableObjects)
			{
				o.SetActive(true);
			}
		}
	}
}