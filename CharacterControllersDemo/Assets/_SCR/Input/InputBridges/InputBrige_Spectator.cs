using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
    public class InputBrige_Spectator : MonoBehaviour
    {
        // Camera
        [FoldoutGroup("Camera")] public Vector2 lookInput { get; private set; } = Vector2.zero;

        [FoldoutGroup("Camera")] public float lookSensitivity = 150.0f;
        [FoldoutGroup("Camera")] public Vector2 lookMultipler = Vector2.one;
        [FoldoutGroup("Camera")] public UnityEventsUtil.Vector2Event onLook;

        [FoldoutGroup("Camera")] public float lookDeltaSensitivity = 0.05f;
        [FoldoutGroup("Camera")] public Vector2 lookDeltaMultipler = Vector2.one;
        [FoldoutGroup("Camera")] public UnityEventsUtil.Vector2Event onLookDelta;
        
        [FoldoutGroup("Camera")] public bool lookInvertY = false;

        // Move
        [FoldoutGroup("Move")] public Vector2 moveInput { get; private set; } = new Vector2();
        [FoldoutGroup("Move")] public UnityEventsUtil.Vector2Event onMove;

        [FoldoutGroup("Move")] public float moveVerticalInput { get; private set; } = 0.0f;
        [FoldoutGroup("Move")] public UnityEventsUtil.FloatEvent onMoveVertical;

        // Sprint
        [FoldoutGroup("Sprint")] public bool isSprinting { get; private set; } = false;
        [FoldoutGroup("Sprint")] public UnityEventsUtil.BoolEvent onSprint;

#region Initialize
        private void Start() 
        {
            InputSystem.Input.SpectatorCamera.Look.performed += OnLook;
            InputSystem.Input.SpectatorCamera.LookDelta.performed += OnLookDelta;
            InputSystem.Input.SpectatorCamera.MoveHorizontal.performed += OnMove;
            InputSystem.Input.SpectatorCamera.MoveHorizontal.canceled += OnMove;
            InputSystem.Input.SpectatorCamera.MoveVertical.performed += OnMoveVertical;
            InputSystem.Input.SpectatorCamera.MoveVertical.canceled += OnMoveVertical;
            InputSystem.Input.SpectatorCamera.Sprint.performed += OnSprintPerformed;
            InputSystem.Input.SpectatorCamera.Sprint.canceled += OnSprintCanceled;
        }

        private void OnDestroy() 
        {
            InputSystem.Input.SpectatorCamera.Look.performed -= OnLook;
            InputSystem.Input.SpectatorCamera.LookDelta.performed -= OnLookDelta;
            InputSystem.Input.SpectatorCamera.MoveHorizontal.performed -= OnMove;
            InputSystem.Input.SpectatorCamera.MoveHorizontal.canceled -= OnMove;
            InputSystem.Input.SpectatorCamera.Sprint.performed -= OnSprintPerformed;
            InputSystem.Input.SpectatorCamera.Sprint.canceled -= OnSprintCanceled;
        }

        private void OnEnable()
        {
            InputSystem.Input.SpectatorCamera.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable() 
        {
            InputSystem.Input.SpectatorCamera.Disable();
            Cursor.lockState = CursorLockMode.None;
        }
#endregion

        private Vector2 ConvertCameraValues(InputAction.CallbackContext ctx, Vector2 pAxisMult, float pMult)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            input.x *= pAxisMult.x;
            input.y *= pAxisMult.y * (lookInvertY ? -1 : 1);
            return input * pMult;
        }
        public void OnLook(InputAction.CallbackContext ctx) 
        {
            lookInput = ConvertCameraValues(ctx, lookMultipler, lookSensitivity);
            onLook.Invoke(lookInput);
        }  
        public void OnLookDelta(InputAction.CallbackContext ctx) 
        {
            onLookDelta?.Invoke(ConvertCameraValues(ctx, lookDeltaMultipler, lookDeltaSensitivity));
        }

        private void OnMove(InputAction.CallbackContext ctx) 
        {
            moveInput = ctx.ReadValue<Vector2>();
            onMove?.Invoke(moveInput);
        }
        private void OnMoveVertical(InputAction.CallbackContext ctx) 
        {
            moveVerticalInput = ctx.ReadValue<float>();
            onMoveVertical?.Invoke(moveVerticalInput);
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
    }
}