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
		[FoldoutGroup("Camera")] public Vector2 lookInput { get; protected set; } = Vector2.zero;

		[FoldoutGroup("Camera")] public float lookSensitivity = 150.0f;
		[FoldoutGroup("Camera")] public Vector2 lookMultipler = Vector2.one;
		[FoldoutGroup("Camera")] public UnityEventsUtil.Vector2Event onLook;

		[FoldoutGroup("Camera")] public float lookDeltaSensitivity = 0.05f;
		[FoldoutGroup("Camera")] public Vector2 lookDeltaMultipler = Vector2.one;
		[FoldoutGroup("Camera")] public UnityEventsUtil.Vector2Event onLookDelta;
		
		[FoldoutGroup("Camera")] public bool lookInvertY = false;

		// Move
		[FoldoutGroup("Move")] public Vector2 moveInput { get; protected set; } = new Vector2();
		[FoldoutGroup("Move")] public UnityEventsUtil.Vector2Event onMove;

		[FoldoutGroup("Move")] public float moveVerticalInput { get; protected set; } = 0.0f;
		[FoldoutGroup("Move")] public UnityEventsUtil.FloatEvent onMoveVertical;

		// Zoom
		[FoldoutGroup("Zoom")] public float zoomInput { get; protected set; } = 0.0f;
		[FoldoutGroup("Zoom")] public UnityEventsUtil.FloatEvent onZoom;

		// Sprint
		[FoldoutGroup("Sprint")] public bool isSprinting { get; protected set; } = false;
		[FoldoutGroup("Sprint")] public UnityEventsUtil.BoolEvent onSprint;

		// Spectate
		[FoldoutGroup("Spectate")] public UnityEvent onMode;
		[FoldoutGroup("Spectate")] public UnityEvent onTarget;

#region Initialize
		private void Start() 
		{
			InputSystem.Instance.SpectatorCamera.Look.performed += OnLook;
			InputSystem.Instance.SpectatorCamera.LookDelta.performed += OnLookDelta;
			InputSystem.Instance.SpectatorCamera.MoveHorizontal.performed += OnMove;
			InputSystem.Instance.SpectatorCamera.MoveHorizontal.canceled += OnMove;
			InputSystem.Instance.SpectatorCamera.MoveVertical.performed += OnMoveVertical;
			InputSystem.Instance.SpectatorCamera.MoveVertical.canceled += OnMoveVertical;
			InputSystem.Instance.SpectatorCamera.Zoom.performed += OnZoom;
			InputSystem.Instance.SpectatorCamera.Sprint.performed += OnSprintPerformed;
			InputSystem.Instance.SpectatorCamera.Sprint.canceled += OnSprintCanceled;
			InputSystem.Instance.SpectatorCamera.ModeToggle.performed += OnMode;
			InputSystem.Instance.SpectatorCamera.TargetToggle.performed += OnTarget;

			PauseSystem.onPause += ClearInputs;
		}

		private void OnDestroy() 
		{
			InputSystem.Instance.SpectatorCamera.Look.performed -= OnLook;
			InputSystem.Instance.SpectatorCamera.LookDelta.performed -= OnLookDelta;
			InputSystem.Instance.SpectatorCamera.MoveHorizontal.performed -= OnMove;
			InputSystem.Instance.SpectatorCamera.MoveHorizontal.canceled -= OnMove;
			InputSystem.Instance.SpectatorCamera.MoveVertical.performed -= OnMoveVertical;
			InputSystem.Instance.SpectatorCamera.MoveVertical.canceled -= OnMoveVertical;
			InputSystem.Instance.SpectatorCamera.Zoom.performed -= OnZoom;
			InputSystem.Instance.SpectatorCamera.Sprint.performed -= OnSprintPerformed;
			InputSystem.Instance.SpectatorCamera.Sprint.canceled -= OnSprintCanceled;
			InputSystem.Instance.SpectatorCamera.ModeToggle.performed -= OnMode;
			InputSystem.Instance.SpectatorCamera.TargetToggle.performed -= OnTarget;

			PauseSystem.onPause -= ClearInputs;
		}

		private void OnEnable()
		{
			InputSystem.Instance.SpectatorCamera.Enable();
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void OnDisable() 
		{
			InputSystem.Instance.SpectatorCamera.Disable();
			Cursor.lockState = CursorLockMode.None;
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

			moveVerticalInput = 0.0f;
			onMoveVertical?.Invoke(moveVerticalInput);
		}

		private Vector2 ConvertCameraValues(InputAction.CallbackContext ctx, Vector2 pAxisMult, float pMult)
		{
			Vector2 input = ctx.ReadValue<Vector2>();
			input.x *= pAxisMult.x;
			input.y *= pAxisMult.y * (lookInvertY ? -1 : 1);
			return input * pMult;
		}
		protected virtual void OnLook(InputAction.CallbackContext ctx) 
		{
			if (IsValid() == false)
				return;

			lookInput = ConvertCameraValues(ctx, lookMultipler, lookSensitivity);
			onLook.Invoke(lookInput);
		}
		protected virtual void OnLookDelta(InputAction.CallbackContext ctx) 
		{
			if (IsValid() == false)
				return;

			onLookDelta?.Invoke(ConvertCameraValues(ctx, lookDeltaMultipler, lookDeltaSensitivity));
		}

		protected virtual void OnMove(InputAction.CallbackContext ctx) 
		{
			if (IsValid() == false)
				return;

			moveInput = ctx.ReadValue<Vector2>();
			onMove?.Invoke(moveInput);
		}
		protected virtual void OnMoveVertical(InputAction.CallbackContext ctx) 
		{
			if (IsValid() == false)
				return;

			moveVerticalInput = ctx.ReadValue<float>();
			onMoveVertical?.Invoke(moveVerticalInput);
		}

		protected virtual void OnZoom(InputAction.CallbackContext ctx) 
		{
			if (IsValid() == false)
				return;

			zoomInput = ctx.ReadValue<float>();
			onZoom?.Invoke(zoomInput);
		}

		protected virtual void OnSprintPerformed(InputAction.CallbackContext ctx)
		{
			isSprinting = true;
			onSprint?.Invoke(isSprinting);
		}
		protected virtual void OnSprintCanceled(InputAction.CallbackContext ctx)
		{
			isSprinting = false;
			onSprint?.Invoke(isSprinting);
		}
		
		protected virtual void OnMode(InputAction.CallbackContext ctx)
		{
			if (IsValid() == false)
				return;

			onMode?.Invoke();
		}
		
		protected virtual void OnTarget(InputAction.CallbackContext ctx)
		{
			if (IsValid() == false)
				return;

			onTarget?.Invoke();
		}
	}
}