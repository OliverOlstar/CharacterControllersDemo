using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher 
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [Header("Follow")]
        public Transform followTarget = null;
        [SerializeField] private Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
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

        private void Start() 
        {
            DoFollow();

            currZoom = childOffset.magnitude;
            transform.GetChild(0).localPosition = childOffset;
            childOffset.Normalize();
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
            if (followTarget != null)
            {
                transform.position = followTarget.position + offset;
            }
        }

        private void RotateCamera(Vector2 pInput)
        {
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
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, childOffset * currZoom, Time.deltaTime * 15.0f);
        }

        private void DoCollision()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(childOffset), out RaycastHit hit, transform.GetChild(0).localPosition.magnitude + collisionRadius, collisionLayers))
            {
                transform.GetChild(0).localPosition = childOffset * (hit.distance - collisionRadius);
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
                transform.GetChild(0).localPosition = childOffset;
            }
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(childOffset) * (transform.GetChild(0).localPosition.magnitude + collisionRadius));
        }
    }
}