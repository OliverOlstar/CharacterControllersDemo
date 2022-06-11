using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Photon.Pun;
using Sirenix.OdinInspector;

namespace OliverLoescher.FPS
{
    public class InputBridge_FPS : MonoBehaviour
    {
        // Camera
        [FoldoutGroup("Look")] public Vector2 lookInput { get; private set; } = Vector2.zero;

        [FoldoutGroup("Look")] public float lookSensitivity = 5.0f;
        [FoldoutGroup("Look")] public Vector2 lookMultipler = Vector2.one;
        [FoldoutGroup("Look")] public UnityEventsUtil.Vector2Event onLook;

        [FoldoutGroup("Look")] public float lookDeltaSensitivity = 0.05f;
        [FoldoutGroup("Look")] public Vector2 lookDeltaMultipler = Vector2.one;
        [FoldoutGroup("Look")] public UnityEventsUtil.Vector2Event onLookDelta;
        
        [FoldoutGroup("Look")] public bool lookInvertY = false;

        // Move
        [FoldoutGroup("Move")] public Vector2 moveInput { get; private set; } = new Vector2();
        [FoldoutGroup("Move")] public Vector3 moveInputVector3 => new Vector3(moveInput.x, 0.0f, moveInput.y);
        [FoldoutGroup("Move")] public UnityEventsUtil.Vector2Event onMove;

        // Sprint
        [FoldoutGroup("Sprint")] public bool toggleSprint = true;
        [FoldoutGroup("Sprint")] public bool isSprinting { get; private set; } = false;
        [FoldoutGroup("Sprint")] public UnityEventsUtil.BoolEvent onSprint;
        [FoldoutGroup("Sprint")] public UnityEvent onSprintPerformed;
        [FoldoutGroup("Sprint")] public UnityEvent onSprintCanceled;

        // Crouch
        [FoldoutGroup("Crouch")] public bool toggleCrouch = true;
        [FoldoutGroup("Crouch")] public bool isCrouching { get; private set; } = false;
        [FoldoutGroup("Crouch")] public UnityEventsUtil.BoolEvent onCrouch;
        [FoldoutGroup("Crouch")] public UnityEvent onCrouchPerformed;
        [FoldoutGroup("Crouch")] public UnityEvent onCrouchCanceled;

        // Jump
        [FoldoutGroup("Jump")] public UnityEvent onJump;
        
        // Primary
        [FoldoutGroup("Primary")] public bool isPressingPrimary { get; private set; } = false;
        [FoldoutGroup("Primary")] public UnityEventsUtil.BoolEvent onPrimary;
        [FoldoutGroup("Primary")] public UnityEvent onPrimaryPerformed;
        [FoldoutGroup("Primary")] public UnityEvent onPrimaryCanceled;

#region Initialize
        private void Start() 
        {
            InputSystem.Instance.FPS.CameraMove.performed += OnCameraMove;
            InputSystem.Instance.FPS.CameraMoveDelta.performed += OnCameraMoveDelta;
            InputSystem.Instance.FPS.Move.performed += OnMove;
            InputSystem.Instance.FPS.Move.canceled += OnMove;
            InputSystem.Instance.FPS.Sprint.performed += OnSprintPerformed;
            InputSystem.Instance.FPS.Sprint.canceled += OnSprintCanceled;
            InputSystem.Instance.FPS.Jump.performed += OnJumpPerformed;
            InputSystem.Instance.FPS.Crouch.performed += OnCrouchPerformed;
            InputSystem.Instance.FPS.Crouch.canceled += OnCrouchCanceled;
            InputSystem.Instance.FPS.Primary.performed += OnPrimaryPerformed;
            InputSystem.Instance.FPS.Primary.canceled += OnPrimaryCanceled;

            PauseSystem.onPause += ClearInputs;
        }

        private void OnDestroy() 
        {
            InputSystem.Instance.FPS.CameraMove.performed -= OnCameraMove;
            InputSystem.Instance.FPS.CameraMoveDelta.performed -= OnCameraMoveDelta;
            InputSystem.Instance.FPS.Move.performed -= OnMove;
            InputSystem.Instance.FPS.Move.canceled -= OnMove;
            InputSystem.Instance.FPS.Sprint.performed -= OnSprintPerformed;
            InputSystem.Instance.FPS.Sprint.canceled -= OnSprintCanceled;
            InputSystem.Instance.FPS.Jump.performed -= OnJumpPerformed;
            InputSystem.Instance.FPS.Crouch.performed -= OnCrouchPerformed;
            InputSystem.Instance.FPS.Crouch.canceled -= OnCrouchCanceled;
            InputSystem.Instance.FPS.Primary.performed -= OnPrimaryPerformed;
            InputSystem.Instance.FPS.Primary.canceled -= OnPrimaryCanceled;

            PauseSystem.onPause -= ClearInputs;
        }

        private void OnEnable()
        {
            InputSystem.Instance.FPS.Enable();
        }

        private void OnDisable() 
        {
            InputSystem.Instance.FPS.Disable();
        }
#endregion

        public bool IsValid()
        {
            return PauseSystem.isPaused == false;
        }

        public void ClearInputs()
        {
            lookInput = Vector2.zero;
            onLook?.Invoke(lookInput);

            moveInput = Vector2.zero;
            onMove?.Invoke(moveInput);

            isPressingPrimary = false;
            onPrimaryCanceled?.Invoke();

            SetSprinting(false);
        }

        private Vector2 ConvertCameraValues(InputAction.CallbackContext ctx, Vector2 pAxisMult, float pMult)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            input.x *= pAxisMult.x;
            input.y *= pAxisMult.y * (lookInvertY ? -1 : 1);
            return input * pMult;
        }

        public void OnCameraMove(InputAction.CallbackContext ctx) 
        {
            if (IsValid() == false)
                return;

            lookInput = ConvertCameraValues(ctx, lookMultipler, lookSensitivity);
            onLook.Invoke(lookInput);
        }  
        public void OnCameraMoveDelta(InputAction.CallbackContext ctx) 
        {
            if (IsValid() == false)
                return;

            onLookDelta?.Invoke(ConvertCameraValues(ctx, lookDeltaMultipler, lookDeltaSensitivity));
        }
        private void OnMove(InputAction.CallbackContext ctx) 
        {
            if (IsValid() == false)
                return;

            moveInput = ctx.ReadValue<Vector2>();
            onMove?.Invoke(moveInput);
        }

        private void OnSprintPerformed(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

            // True if not toggle sprint || not currently sprinting
            // False if toggle sprint && currently sprinting
            SetSprinting(toggleSprint == false || isSprinting == false);
        }
		private void OnSprintCanceled(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

            if (toggleSprint == false)
                SetSprinting(false);
        }
        public void SetSprinting(bool pSprinting)
        {
            if (isSprinting != pSprinting)
            {
                isSprinting = pSprinting;

                // Events
                onSprint.Invoke(isSprinting);
                if (isSprinting)
                    onSprintPerformed?.Invoke();
                else
                    onSprintCanceled?.Invoke();
            }
        }

        private void OnCrouchPerformed(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

            // True if not toggle crouch || not currently crouching
            // False if toggle crouch && currently crouching
            SetCrouch(toggleCrouch == false || isCrouching == false);
        }
        private void OnCrouchCanceled(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

            if (toggleCrouch == false)
                SetCrouch(false);
        }
        public void SetCrouch(bool pCrouch)
        {
            if (isCrouching != pCrouch)
            {
                isCrouching = pCrouch;

                // Events
                onCrouch.Invoke(isCrouching);
                if (isCrouching)
                    onCrouchPerformed?.Invoke();
                else
                    onCrouchCanceled?.Invoke();
            }
        }
        public void ClearCrouchIfToggle()
		{
            if (toggleCrouch)
			{
                SetCrouch(false);
			}
		}

        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

            SetCrouch(false);
            onJump?.Invoke();
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
    }
}