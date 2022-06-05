using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher 
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private InputBridge_Camera input = null;

        [Header("Follow")]
        public Transform followTransform = null;
        [SerializeField] private Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
        public Transform cameraTransform = null; // Should be child
        [SerializeField] private Vector3 childOffset = new Vector3(0.0f, 2.0f, -5.0f);
        
        [Header("Look")]
        [SerializeField] private Transform lookTransform = null;
        [SerializeField, MinMaxSlider(-90, 90, true)] private Vector2 lookYClamp = new Vector2(-40, 50);
        [SerializeField] private float sensitivityDelta = 1.0f;
        [SerializeField] private float sensitivityUpdate = 1.0f;
        private Vector2 lookInput = new Vector2();

        [Header("Zoom")]
        [SerializeField] private float zoomSpeed = 1.0f;
        [SerializeField] private Vector2 zoomDistanceClamp = new Vector2(1.0f, 5.0f);
        private float currZoom = 0.5f;

        [Header("Collision")]
        [SerializeField] private LayerMask collisionLayers = new LayerMask();
        [SerializeField] private float collisionRadius = 0.2f;

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
            DoFollow();

            currZoom = childOffset.magnitude;
            cameraTransform.localPosition = childOffset;

            if (input != null)
            {
                input.onLook.AddListener(OnLook);
                input.onLookDelta.AddListener(OnLookDelta);
                input.onZoom.AddListener(OnZoom);
            }
        }

        private void LateUpdate() 
        {
            DoFollow();
            
            if (lookInput != Vector2.zero)
            {
                RotateCamera(lookInput * sensitivityUpdate * Time.deltaTime);
            }

            DoZoomUpdate();
            DoCollision();
        }

        private void DoFollow()
        {
            if (followTransform != null)
            {
                transform.position = followTransform.position + offset;
            }
        }

        private void RotateCamera(Vector2 pInput)
        {
            if (lookTransform == null)
			{
                return;
			}
            Vector3 euler = lookTransform.eulerAngles;
            euler.x = Mathf.Clamp(FuncUtil.SafeAngle(euler.x - pInput.y), lookYClamp.x, lookYClamp.y);
            euler.y = euler.y + pInput.x;
            euler.z = 0.0f;
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
            RotateCamera(pInput * sensitivityDelta);
        }

        public void OnZoom(float pInput)
        {
            DoZoom(pInput);
        }
#endregion

        private void OnDrawGizmosSelected() 
        {
            if (Application.isPlaying == false)
            {
                DoFollow();
                cameraTransform.localPosition = childOffset;
            }
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(childOffset) * (cameraTransform.localPosition.magnitude + collisionRadius));
        }
    }
}