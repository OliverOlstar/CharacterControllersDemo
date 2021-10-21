using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace OliverLoescher.FPS
{
    public class FPS_InputBridge : MonoBehaviour
    {
        // Camera
        public Vector2 cameraMoveInput { get; private set; } = Vector2.zero;
        [HideInInspector] public UnityEventsUtil.Vector2Event onCameraMoveDelta;

        public bool cameraInvertY = false;
        public float cameraMoveSensitivity = 1.0f;
        public Vector2 cameraMoveMultipler = Vector2.one;
        public float cameraMoveDeltaSensitivity = 1.0f;
        public Vector2 cameraMoveDeltaMultipler = Vector2.one;

        // Move
        public Vector2 moveInput { get; private set; } = new Vector2();
        public Vector3 moveInputVector3 => new Vector3(moveInput.x, 0.0f, moveInput.y);
        public UnityEventsUtil.Vector2Event onMove;

        // Sprint
        public bool isSprinting { get; private set; } = false;
        public UnityEventsUtil.BoolEvent onSprint;

        // Crouch
        // public bool toggleCrouch = true;
        // public bool crouchInput { get; private set; } = false;
        // [HideInInspector] public UnityEventsUtil.BoolEvent onCrouch = new UnityEventsUtil.BoolEvent();
        // [HideInInspector] public UnityEvent onCrouchPerformed = new UnityEvent();
        // [HideInInspector] public UnityEvent onCrouchCanceled = new UnityEvent();

        private void Awake() 
        {
            InputSystem.Input.FPS.CameraMove.performed += OnCameraMove;
            InputSystem.Input.FPS.CameraMoveDelta.performed += OnCameraMoveDelta;
            InputSystem.Input.FPS.Move.performed += OnMove;
            InputSystem.Input.FPS.Move.canceled += OnMove;
            InputSystem.Input.FPS.Sprint.performed += OnSprintPerformed;
            InputSystem.Input.FPS.Sprint.canceled += OnSprintCanceled;
            // InputSystem.Input.FPS.Crouch.performed += OnCrouchPerformed;
            // InputSystem.Input.FPS.Crouch.canceled += OnCrouchCanceled;
        }

        private void OnEnable()
        {
            InputSystem.Input.FPS.Enable();
        }

        private void OnDisable() 
        {
            InputSystem.Input.FPS.Disable();
        }

        private Vector2 ConvertCameraValues(InputAction.CallbackContext ctx, Vector2 pAxisMult, float pMult)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            input.x *= pAxisMult.x;
            input.y *= pAxisMult.y * (cameraInvertY ? -1 : 1);
            return input * pMult;
        }
        public void OnCameraMove(InputAction.CallbackContext ctx) => cameraMoveInput = ConvertCameraValues(ctx, cameraMoveMultipler, cameraMoveSensitivity);
        public void OnCameraMoveDelta(InputAction.CallbackContext ctx) => onCameraMoveDelta?.Invoke(ConvertCameraValues(ctx, cameraMoveDeltaMultipler, cameraMoveDeltaSensitivity));
        private void OnMove(InputAction.CallbackContext ctx) 
        {
            moveInput = ctx.ReadValue<Vector2>();
            onMove?.Invoke(moveInput);
        }

        private void OnSprintPerformed(InputAction.CallbackContext ctx)
        {
            isSprinting = true;
            onSprint?.Invoke(isSprinting);
        }
        private void OnSprintCanceled(InputAction.CallbackContext ctx)
        {
            isSprinting = false;
            onSprint?.Invoke(isSprinting);
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