using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
	public class InputBridge_Camera : MonoBehaviour
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

		// Zoom
		[FoldoutGroup("Zoom")] public float zoomInput { get; protected set; } = 0.0f;
		[FoldoutGroup("Zoom")] public UnityEventsUtil.FloatEvent onZoom;

		#region Initialize
		private void Start()
		{
			InputSystem.Instance.Camera.Look.performed += OnLook;
			InputSystem.Instance.Camera.LookDelta.performed += OnLookDelta;
			InputSystem.Instance.Camera.Zoom.performed += OnZoom;

			PauseSystem.onPause += ClearInputs;
		}

		private void OnDestroy()
		{
			InputSystem.Instance.Camera.Look.performed -= OnLook;
			InputSystem.Instance.Camera.LookDelta.performed -= OnLookDelta;
			InputSystem.Instance.Camera.Zoom.performed -= OnZoom;

			PauseSystem.onPause -= ClearInputs;
		}

		private void OnEnable()
		{
			InputSystem.Instance.Camera.Enable();
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void OnDisable()
		{
			InputSystem.Instance.Camera.Disable();
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

		protected virtual void OnZoom(InputAction.CallbackContext ctx)
		{
			if (IsValid() == false)
				return;

			zoomInput = ctx.ReadValue<float>();
			onZoom?.Invoke(zoomInput);
		}
	}
}