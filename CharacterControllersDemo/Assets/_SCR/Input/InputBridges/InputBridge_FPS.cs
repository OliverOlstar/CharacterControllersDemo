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
        [FoldoutGroup("Sprint")] public bool isSprinting { get; private set; } = false;
        [FoldoutGroup("Sprint")] public UnityEventsUtil.BoolEvent onSprint;
        
        // Jump
        [FoldoutGroup("Jump")] public UnityEvent onJump;

        // Crouch
        // public bool toggleCrouch = true;
        // public bool crouchInput { get; private set; } = false;
        // [HideInInspector] public UnityEventsUtil.BoolEvent onCrouch = new UnityEventsUtil.BoolEvent();
        // [HideInInspector] public UnityEvent onCrouchPerformed = new UnityEvent();
        // [HideInInspector] public UnityEvent onCrouchCanceled = new UnityEvent();
        
        // Primary
        [FoldoutGroup("Primary")] public bool isPressingPrimary { get; private set; } = false;
        [FoldoutGroup("Primary")] public UnityEventsUtil.BoolEvent onPrimary;
        [FoldoutGroup("Primary")] public UnityEvent onPrimaryPerformed;
        [FoldoutGroup("Primary")] public UnityEvent onPrimaryCanceled;

#region Initialize
        private void Start() 
        {
            InputSystem.Input.FPS.CameraMove.performed += OnCameraMove;
            InputSystem.Input.FPS.CameraMoveDelta.performed += OnCameraMoveDelta;
            InputSystem.Input.FPS.Move.performed += OnMove;
            InputSystem.Input.FPS.Move.canceled += OnMove;
            InputSystem.Input.FPS.Sprint.performed += OnSprintPerformed;
            InputSystem.Input.FPS.Sprint.canceled += OnSprintCanceled;
            InputSystem.Input.FPS.Jump.performed += OnJumpPerformed;
            // InputSystem.Input.FPS.Crouch.performed += OnCrouchPerformed;
            // InputSystem.Input.FPS.Crouch.canceled += OnCrouchCanceled;
            InputSystem.Input.FPS.Primary.performed += OnPrimaryPerformed;
            InputSystem.Input.FPS.Primary.canceled += OnPrimaryCanceled;

            PauseSystem.onPause += ClearInputs;
        }

        private void OnDestroy() 
        {
            InputSystem.Input.FPS.CameraMove.performed -= OnCameraMove;
            InputSystem.Input.FPS.CameraMoveDelta.performed -= OnCameraMoveDelta;
            InputSystem.Input.FPS.Move.performed -= OnMove;
            InputSystem.Input.FPS.Move.canceled -= OnMove;
            InputSystem.Input.FPS.Sprint.performed -= OnSprintPerformed;
            InputSystem.Input.FPS.Sprint.canceled -= OnSprintCanceled;
            InputSystem.Input.FPS.Jump.performed -= OnJumpPerformed;
            // InputSystem.Input.FPS.Crouch.performed -= OnCrouchPerformed;
            // InputSystem.Input.FPS.Crouch.canceled -= OnCrouchCanceled;
            InputSystem.Input.FPS.Primary.performed -= OnPrimaryPerformed;
            InputSystem.Input.FPS.Primary.canceled -= OnPrimaryCanceled;

            PauseSystem.onPause -= ClearInputs;
        }

        private void OnEnable()
        {
            InputSystem.Input.FPS.Enable();
        }

        private void OnDisable() 
        {
            InputSystem.Input.FPS.Disable();
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

            isSprinting = true;
            onSprint?.Invoke(isSprinting);
        }
        private void OnSprintCanceled(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

            isSprinting = false;
            onSprint?.Invoke(isSprinting);
        }

        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            if (IsValid() == false)
                return;

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

        // private void OnCrouchPerformed(InputAction.CallbackContext ctx)
        // {
        //     // True if not toggle crouch || not currently crouching
        //     // False if toggle crouch && currently crouching
        //     SetCrouch(toggleCrouch == false || crouchInput == false);
        // }
        // private void OnCrouchCanceled(InputAction.CallbackContext ctx)
        // {
        //     if (toggleCrouch == false)
        //         SetCrouch(false);
        // }
        // public void SetCrouch(bool pCrouch)
        // {
        //     if (crouchInput != pCrouch)
        //     {
        //         crouchInput = pCrouch;

        //         // Events
        //         onCrouch.Invoke(crouchInput);
        //         if (crouchInput)
        //             onCrouchPerformed?.Invoke();
        //         else
        //             onCrouchCanceled?.Invoke();
        //     }
        // }
    }
}