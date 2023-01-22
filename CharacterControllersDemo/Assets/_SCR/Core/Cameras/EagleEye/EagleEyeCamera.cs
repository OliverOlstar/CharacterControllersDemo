using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Camera
{
    public class EagleEyeCamera : MonoBehaviour
    {
		[SerializeField]
		private InputBridge_EagleEye input = null;
		[SerializeField, DisableInPlayMode]
		private MonoUtil.Updateable updateable = new MonoUtil.Updateable(MonoUtil.UpdateType.Late, MonoUtil.Priorities.Camera);

		[Header("Follow")]
		public Transform cameraTransform = null; // Should be child
		[SerializeField]
		private Vector3 childOffset = new Vector3(0.0f, 2.0f, -5.0f);

		[Header("Look")]
		[SerializeField]
		private Transform lookTransform = null;
		[SerializeField, MinMaxSlider(-90, 90, true)]
		private Vector2 lookYClamp = new Vector2(-40, 50);
		[SerializeField]
		private float sensitivityDelta = 1.0f;
		[SerializeField]
		private float sensitivityUpdate = 1.0f;
		private Vector2 lookInput = new Vector2();
		private float RotateInput => input.Rotate.Input;
		[SerializeField]
		private float rotateSpeed = 1.0f;

		[Header("Zoom")]
		[SerializeField]
		private float zoomSpeed = 1.0f;
		[SerializeField]
		private Vector2 zoomDistanceClamp = new Vector2(1.0f, 5.0f);
		private float currZoom = 0.5f;

		[Header("Collision")]
		[SerializeField]
		private LayerMask collisionLayers = new LayerMask();
		[SerializeField]
		private float collisionRadius = 0.2f;

		private void Reset()
		{
			lookTransform = transform;
			if (transform.childCount > 0)
			{
				cameraTransform = transform.GetChild(0);
			}
		}

		private void Start()
		{
			currZoom = childOffset.magnitude;
			cameraTransform.localPosition = childOffset;

			if (input != null)
			{
				input.Move.onChanged.AddListener(OnLook);
				input.MoveDelta.Value.onChanged.AddListener(OnLookDelta);
				input.Zoom.onChanged.AddListener(OnZoom);
			}

			updateable.Register(Tick);
		}

		private void OnDestroy()
		{
			updateable.Deregister();
		}

		private void Tick(float pDeltaTime)
		{
			RotateCamera(RotateInput * rotateSpeed * pDeltaTime);
			DoZoomUpdate();
			DoCollision();
		}

		private void RotateCamera(float pInput)
		{
			if (lookTransform == null || pInput == 0.0f)
			{
				return;
			}
			Vector3 euler = lookTransform.eulerAngles;
			euler.y += pInput;
			lookTransform.rotation = Quaternion.Euler(euler);
		}

		private void DoZoom(float pInput)
		{
			currZoom += (pInput * zoomSpeed);
			currZoom = Mathf.Clamp(currZoom, zoomDistanceClamp.x, zoomDistanceClamp.y);
		}

		private void DoZoomUpdate()
		{
			cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, childOffset.normalized * currZoom, Time.deltaTime * 15.0f);
		}

		private void DoCollision()
		{
			if (Physics.Raycast(transform.position, transform.TransformDirection(childOffset.normalized), out RaycastHit hit, cameraTransform.localPosition.magnitude + collisionRadius, collisionLayers))
			{
				cameraTransform.localPosition = childOffset.normalized * (hit.distance - collisionRadius);
			}
		}

		#region Input
		public void OnLook(Vector2 pInput)
		{
			lookInput = pInput;
		}

		public void OnLookDelta(Vector2 pInput)
		{
			//RotateCamera(pInput * sensitivityDelta);
		}

		public void OnZoom(float pInput)
		{
			DoZoom(pInput);
		}
		#endregion

		private void OnDrawGizmosSelected()
		{
			if (!Application.isPlaying && cameraTransform != null)
			{
				cameraTransform.localPosition = childOffset;
			}
		}

		private void OnDrawGizmos()
		{
			if (cameraTransform == null)
			{
				return;
			}
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(childOffset) * (cameraTransform.localPosition.magnitude + collisionRadius));
		}
	}
}
