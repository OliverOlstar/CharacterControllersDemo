using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using OliverLoescher.Input;

namespace OliverLoescher.Link
{
    public class InputBridge_Platformer : InputBridge_Base
	{
		[SerializeField]
		private InputModule_Vector2 moveInput = new InputModule_Vector2();
		[SerializeField]
		private InputModule_Toggle crouchInput = new InputModule_Toggle();
		[SerializeField]
		private InputModule_Trigger rollInput = new InputModule_Trigger();

		public InputModule_Vector2 Move => moveInput;
		public InputModule_Toggle Crouch => crouchInput;
		public InputModule_Trigger Roll => rollInput;

		public override InputActionMap Actions => InputSystem.Instance.Link.Get();
		public override IEnumerable<InputModule_Base> GetAllInputModules()
		{
			yield return moveInput;
			yield return crouchInput;
			yield return rollInput;
		}

		protected override void Awake()
		{
			moveInput.Initalize(InputSystem.Instance.Link.Move, IsValid);
			crouchInput.Initalize(InputSystem.Instance.Link.Crouch, IsValid);
			rollInput.Initalize(InputSystem.Instance.Link.Roll, IsValid);

			base.Awake();
        }
	}
}