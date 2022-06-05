using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace OliverLoescher.Link
{
    public class InputBridge_Platformer : MonoBehaviour
    {
        // Move
        public Vector2 moveInput { get; private set; } = new Vector2();
        public Vector3 moveInputVector3 => new Vector3(moveInput.x, 0.0f, moveInput.y);

        // Roll
        [HideInInspector] public UnityEvent onRollPerformed = new UnityEvent();

        // Crouch
        public bool toggleCrouch = true;
        public bool crouchInput { get; private set; } = false;
        [HideInInspector] public UnityEventsUtil.BoolEvent onCrouch = new UnityEventsUtil.BoolEvent();
        [HideInInspector] public UnityEvent onCrouchPerformed = new UnityEvent();
        [HideInInspector] public UnityEvent onCrouchCanceled = new UnityEvent();

        private void Awake() 
        {
            InputSystem.Instance.Link.Move.performed += OnMove;
            InputSystem.Instance.Link.Roll.performed += OnRollPerformed;
            InputSystem.Instance.Link.Crouch.performed += OnCrouchPerformed;
            InputSystem.Instance.Link.Crouch.canceled += OnCrouchCanceled;
        }

        private void OnEnable()
        {
            InputSystem.Instance.Link.Enable();
        }

        private void OnDisable() 
        {
            InputSystem.Instance.Link.Disable();
        }

        private void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();

        private void OnCrouchPerformed(InputAction.CallbackContext ctx)
        {
            // True if not toggle crouch || not currently crouching
            // False if toggle crouch && currently crouching
            SetCrouch(toggleCrouch == false || crouchInput == false);
        }
        private void OnCrouchCanceled(InputAction.CallbackContext ctx)
        {
            if (toggleCrouch == false)
                SetCrouch(false);
        }
        public void SetCrouch(bool pCrouch)
        {
            if (crouchInput != pCrouch)
            {
                crouchInput = pCrouch;

                // Events
                onCrouch.Invoke(crouchInput);
                if (crouchInput)
                    onCrouchPerformed?.Invoke();
                else
                    onCrouchCanceled?.Invoke();
            }
        }

        public void OnRollPerformed(InputAction.CallbackContext ctx)
        {
            onRollPerformed?.Invoke();
        }
    }
}