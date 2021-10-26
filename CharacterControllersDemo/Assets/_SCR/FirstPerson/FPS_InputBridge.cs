using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Photon.Pun;
using Sirenix.OdinInspector;

namespace OliverLoescher.FPS
{
    public class FPS_InputBridge : MonoBehaviour
    {
        // Camera
        [FoldoutGroup("Camera")] public Vector2 cameraMoveInput { get; private set; } = Vector2.zero;

        [FoldoutGroup("Camera")] public float cameraMoveSensitivity = 5.0f;
        [FoldoutGroup("Camera")] public Vector2 cameraMoveMultipler = Vector2.one;
        [FoldoutGroup("Camera")] public UnityEventsUtil.Vector2Event onCameraMove;

        [FoldoutGroup("Camera")] public float cameraMoveDeltaSensitivity = 0.05f;
        [FoldoutGroup("Camera")] public Vector2 cameraMoveDeltaMultipler = Vector2.one;
        [FoldoutGroup("Camera")] public UnityEventsUtil.Vector2Event onCameraMoveDelta;
        
        [FoldoutGroup("Camera")] public bool cameraInvertY = false;

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

        private PhotonView photonView;

        private void Awake() 
        {
            photonView = GetComponentInParent<PhotonView>();

            if (photonView == null || photonView.IsMine)
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
            }
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
        public void OnCameraMove(InputAction.CallbackContext ctx) 
        {
            cameraMoveInput = ConvertCameraValues(ctx, cameraMoveMultipler, cameraMoveSensitivity);
            onCameraMove.Invoke(cameraMoveInput);
        }  
        public void OnCameraMoveDelta(InputAction.CallbackContext ctx) 
        {
            onCameraMoveDelta?.Invoke(ConvertCameraValues(ctx, cameraMoveDeltaMultipler, cameraMoveDeltaSensitivity));
        }
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

        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            onJump?.Invoke();
        }

        private void OnPrimaryPerformed(InputAction.CallbackContext ctx)
        {
            isPressingPrimary = true;
            onPrimary?.Invoke(true);
            onPrimaryPerformed?.Invoke();
        }
        private void OnPrimaryCanceled(InputAction.CallbackContext ctx)
        {
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