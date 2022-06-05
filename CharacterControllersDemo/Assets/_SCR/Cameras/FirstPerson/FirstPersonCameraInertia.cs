using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

namespace OliverLoescher.Camera
{
    public class FirstPersonCameraInertia : MonoBehaviour
    {
        [SerializeField] private new CinemachineVirtualCamera camera = null;
        [SerializeField] private Rigidbody rigid = null;
        private Vector3 lastVelocity;
        private Vector3 lastPosition;

        [Header("Tilt")]
        [SerializeField, Range(0, 4999)] private float tiltMagnitude = 1.0f;
        [SerializeField, Range(0, 45)] private float tiltMax = 12.5f;
        [SerializeField, Range(0, 50)] private float tiltDampening = 10.0f;

        [Header("Spring")]
        [SerializeField, Range(0, 50)] private float sprintSpring = 45.0f;
        [SerializeField, Range(0, 50)] private float springDamper = 10.0f;
        private float springVel = 0;

        [Header("FOV")]
        [SerializeField, MinMaxSlider(0, 180, true)] private Vector2 fovMinMax = new Vector2(70.0f, 110.0f);
        [SerializeField, MinMaxSlider(0, 20, true)] private Vector2 fovVelocity = new Vector2(0.0f, 10.0f);
        [SerializeField, Min(0)] private float fovDampening = 10.0f;

        private void Start() 
        {
            lastPosition = transform.position;
        }

        private void FixedUpdate() 
        {
            Vector3 motion = transform.parent.position - lastPosition;
            Vector3 relMotion = transform.InverseTransformDirection(motion);
            lastPosition = transform.parent.position;

            Vector3 velocity = rigid.velocity - lastVelocity;
            lastVelocity = rigid.velocity;

            DoTilt(relMotion);
            DoSpring(velocity);
            DoFOV();
        }

		private void DoTilt(Vector3 pRelMotion)
        {
            Vector3 rot = transform.localEulerAngles;
            rot.z = pRelMotion.x * -tiltMagnitude;
			rot.z = Mathf.Clamp(rot.z, -tiltMax, tiltMax);
			rot.z = Mathf.Lerp(FuncUtil.SafeAngle(transform.localEulerAngles.z), rot.z, Time.fixedDeltaTime * tiltDampening);
            transform.localRotation = Quaternion.Euler(rot);
        }

        private void DoSpring(Vector3 pVelocity)
        {
            springVel += -pVelocity.y;
            springVel += sprintSpring * -transform.localPosition.y * Time.fixedDeltaTime;
            springVel += springDamper * -springVel * Time.fixedDeltaTime;

            Vector3 pos = transform.localPosition;
            pos.y += springVel * Time.fixedDeltaTime;
            transform.localPosition = pos;
        }

        private void DoFOV()
		{
            float fov01 = FuncUtil.SmoothStep(fovVelocity, MathUtil.Horizontalize(rigid.velocity).magnitude);
            camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, Mathf.Lerp(fovMinMax.x, fovMinMax.y, fov01), Time.fixedDeltaTime * fovDampening);
        }
    }
}