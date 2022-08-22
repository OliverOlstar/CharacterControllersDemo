using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OliverLoescher.Input
{
    public abstract class InputBridge_Base : MonoBehaviour
	{
		public abstract InputActionMap Actions { get; }
		public abstract IEnumerable<InputModule_Base> GetAllInputModules();

		protected virtual void Awake()
		{
			foreach (InputModule_Base module in GetAllInputModules())
			{
				module.Enable();
			}
			PauseSystem.onPause += ClearInputs;
		}

		protected virtual void OnDestroy()
		{
			foreach (InputModule_Base module in GetAllInputModules())
			{
				module.Disable();
			}
			PauseSystem.onPause -= ClearInputs;
		}

		protected virtual void OnEnable()
		{
			Actions.Enable();
		}

		protected virtual void OnDisable()
		{
			Actions.Disable();
		}

		public virtual void ClearInputs()
		{
			foreach (InputModule_Base module in GetAllInputModules())
			{
				module.Clear();
			}
		}

		public virtual bool IsValid()
		{
			return PauseSystem.isPaused == false;
		}
	}
}
