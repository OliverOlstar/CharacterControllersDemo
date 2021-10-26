using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace OliverLoescher.Camera
{
    public class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform = null;
        [SerializeField, MinMaxSlider(-90, 90, true)] private Vector2 cameraYClamp = new Vector2(-40, 50);
        private Vector2 moveInput = new Vector2();

        private void Update() 
        {
            if (moveInput != Vector2.zero)
            {
                RotateCamera(moveInput * Time.deltaTime);
            }
        }

        public void OnCameraMove(Vector2 pInput)
        {
            moveInput = pInput;
        }

        public void OnCameraMoveDelta(Vector2 pInput)
        {
            RotateCamera(pInput);
        }

        private void RotateCamera(Vector2 pInput)
        {
            Vector3 euler = cameraTransform.eulerAngles;
            euler.x = Mathf.Clamp(safeAngle(euler.x - pInput.y), cameraYClamp.x, cameraYClamp.y);
            euler.y = euler.y + pInput.x;
            euler.z = 0.0f;
            cameraTransform.rotation = Quaternion.Euler(euler);
        }

        private float safeAngle(float pAngle)
        {
            if (pAngle > 180)
            {
                pAngle -= 360;
            }
            return pAngle;
        }
    }
}