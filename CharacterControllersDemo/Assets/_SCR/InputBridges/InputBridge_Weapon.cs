using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Photon.Pun;
using Sirenix.OdinInspector;
using OliverLoescher.Input;

namespace OliverLoescher.FPS
{
    public class InputBridge_Weapon : InputBridge_Base
    {
		[SerializeField]
		private InputModule_Toggle primaryInput = new InputModule_Toggle();
		[SerializeField]
		private InputModule_Toggle secondaryInput = new InputModule_Toggle();

		public InputModule_Toggle Primary => primaryInput;
		public InputModule_Toggle Secondary => secondaryInput;

		public override InputActionMap Actions => InputSystem.Instance.Weapon.Get();
		public override IEnumerable<InputModule_Base> GetAllInputModules()
		{
			yield return primaryInput;
			yield return secondaryInput;
		}

		protected override void Awake()
		{
			primaryInput.Initalize(InputSystem.Instance.Weapon.Primary, IsValid);
			secondaryInput.Initalize(InputSystem.Instance.Weapon.Secondary, IsValid);

			base.Awake();
		}
	}
}