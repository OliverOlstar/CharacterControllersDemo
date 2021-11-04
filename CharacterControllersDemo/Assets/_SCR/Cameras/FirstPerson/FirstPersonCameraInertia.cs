using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Camera
{
    public class FirstPersonCameraInertia : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigid = null;
        private Vector3 lastVelocity;
        private Vector3 lastPosition;

        [Header("Tilt")]
        [SerializeField, Range(0, 4999)] private float tiltMagnitude = 1.0f;
        [SerializeField, Range(0, 45)] private float tiltMax = 12.5f;
        [SerializeField, Range(0, 50)] private float tiltDampening = 10.0f;

        [Header("Spring")]
        [SerializeField, Range(0, 50)] private float spring = 1.0f;
        [SerializeField, Range(0, 50)] private float damper = 1.0f;
        private float vel = 0;

        private void Start() 
        {
            lastPosition = transform.position;
        }

        private void LateUpdate() 
        {
            Vector3 motion = transform.position - lastPosition;
            Vector3 relMotion = transform.InverseTransformDirection(motion);
            lastPosition = transform.position;

            Vector3 velocity = rigid.velocity - lastVelocity;
            lastVelocity = rigid.velocity;

            DoTilt(relMotion);
            DoSpring(velocity);
        }

        private void DoTilt(Vector3 pRelMotion)
        {
            Vector3 rot = transform.localEulerAngles;
            rot.z = pRelMotion.x * -tiltMagnitude;
            rot.z = Mathf.Clamp(rot.z, -tiltMax, tiltMax);
            rot.z = Mathf.Lerp(FuncUtil.SafeAngle(transform.localEulerAngles.z), rot.z, Time.deltaTime * tiltDampening);
            transform.localRotation = Quaternion.Euler(rot);
        }

        private void DoSpring(Vector3 pVelocity)
        {
            vel += -pVelocity.y;
            vel += spring * -transform.localPosition.y * Time.deltaTime;
            vel += damper * -vel * Time.deltaTime;

            Vector3 pos = transform.localPosition;
            pos.y += vel * Time.deltaTime;
            transform.localPosition = pos;
        }
    }
}