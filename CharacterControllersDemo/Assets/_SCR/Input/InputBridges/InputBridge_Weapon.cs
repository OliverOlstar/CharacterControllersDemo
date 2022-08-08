using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Photon.Pun;
using Sirenix.OdinInspector;

namespace OliverLoescher.FPS
{
    public class InputBridge_Weapon : MonoBehaviour
    {
        // Primary
        [FoldoutGroup("Primary")] public bool isPressingPrimary { get; private set; } = false;
        [FoldoutGroup("Primary")] public UnityEventsUtil.BoolEvent onPrimary;
        [FoldoutGroup("Primary")] public UnityEvent onPrimaryPerformed;
        [FoldoutGroup("Primary")] public UnityEvent onPrimaryCanceled;

		// Secondary
		[FoldoutGroup("Secondary")] public bool isPressingSecondary { get; private set; } = false;
		[FoldoutGroup("Secondary")] public UnityEventsUtil.BoolEvent onSecondary;
		[FoldoutGroup("Secondary")] public UnityEvent onSecondaryPerformed;
		[FoldoutGroup("Secondary")] public UnityEvent onSecondaryCanceled;

		#region Initialize
		private void Start() 
        {
            InputSystem.Instance.Weapon.Primary.performed += OnPrimaryPerformed;
            InputSystem.Instance.Weapon.Primary.canceled += OnPrimaryCanceled;
			InputSystem.Instance.Weapon.Secondary.performed += OnSecondaryPerformed;
			InputSystem.Instance.Weapon.Secondary.canceled += OnSecondaryCanceled;

			PauseSystem.onPause += ClearInputs;
        }

        private void OnDestroy() 
        {
            InputSystem.Instance.Weapon.Primary.performed -= OnPrimaryPerformed;
            InputSystem.Instance.Weapon.Primary.canceled -= OnPrimaryCanceled;
			InputSystem.Instance.Weapon.Secondary.performed -= OnSecondaryPerformed;
			InputSystem.Instance.Weapon.Secondary.canceled -= OnSecondaryCanceled;

			PauseSystem.onPause -= ClearInputs;
        }

        private void OnEnable()
        {
            InputSystem.Instance.Weapon.Enable();
        }

        private void OnDisable() 
        {
            InputSystem.Instance.Weapon.Disable();
        }
#endregion

        public bool IsValid()
        {
            return PauseSystem.isPaused == false;
        }

        public void ClearInputs()
        {
            isPressingPrimary = false;
            onPrimaryCanceled?.Invoke();

			isPressingSecondary = false;
			onSecondaryCanceled?.Invoke();
        }

        private void OnPrimaryPerformed(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

            isPressingPrimary = true;
            onPrimary?.Invoke(true);
            onPrimaryPerformed?.Invoke();
        }
        private void OnPrimaryCanceled(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

            isPressingPrimary = false;
            onPrimary?.Invoke(false);
            onPrimaryCanceled?.Invoke();
		}

		private void OnSecondaryPerformed(InputAction.CallbackContext ctx)
		{
			if (IsValid() == false)
				return;

			isPressingSecondary = true;
			onSecondary?.Invoke(true);
			onSecondaryPerformed?.Invoke();
		}
		private void OnSecondaryCanceled(InputAction.CallbackContext ctx)
		{
			if (IsValid() == false)
				return;

			isPressingSecondary = false;
			onSecondary?.Invoke(false);
			onSecondaryCanceled?.Invoke();
		}
	}
}