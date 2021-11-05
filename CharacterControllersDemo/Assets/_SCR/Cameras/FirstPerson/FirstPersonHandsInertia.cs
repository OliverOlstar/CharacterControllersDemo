using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Camera
{
    public class FirstPersonHandsInertia : MonoBehaviour
    {
        private Vector3 lastRotation;

        [Header("Tilt")]
        [SerializeField] private Vector2 tiltMagnitude = Vector2.one;
        [SerializeField, Min(0)] private Vector2 tiltMax = new Vector2(8.0f, 6.0f);
        [SerializeField, Min(0)] private Vector2 tiltDampening = Vector2.one;

        [Header("Movement")]
        [SerializeField] private Rigidbody rigid = null;
        [SerializeField,Range(0, 0.01f)] private float targetMagnitude = 5.0f;
        [SerializeField] private float moveDampening = 1.0f;
        [SerializeField] private Vector3 moveRelOffset = Vector3.zero;
        private Vector3 initalRelOffset;
        private float moveValue = 0.0f;

        private void Start() 
        {
            lastRotation = transform.parent.eulerAngles;
            initalRelOffset = transform.localPosition;
        }

        private void LateUpdate() 
        {
            Vector3 motion = transform.parent.eulerAngles - lastRotation;
            lastRotation = transform.parent.eulerAngles;

            Vector3 rot = transform.localEulerAngles;
            rot.y = Calculate(FuncUtil.SafeAngle(rot.y), motion.y * tiltMagnitude.x, tiltMax.x, tiltDampening.x);
            rot.x = Calculate(FuncUtil.SafeAngle(rot.x), motion.x * tiltMagnitude.y, tiltMax.y, tiltDampening.y);
            transform.localRotation = Quaternion.Euler(rot);

            float v = rigid.velocity.sqrMagnitude * targetMagnitude;
            moveValue = Mathf.Lerp(moveValue, Mathf.Clamp01(v), Time.deltaTime * moveDampening);
            transform.localPosition = Vector3.Lerp(initalRelOffset, moveRelOffset, moveValue);
        }

        private float Calculate(float pValue, float pTarget, float pMax, float pDampening)
        {
            pTarget = Mathf.Clamp(pTarget, -pMax, pMax);
            pValue = Mathf.Lerp(pValue, pTarget, Time.deltaTime * tiltDampening.x);
            return pValue;
        }

        // private void DoSpring(Vector3 pVelocity)
        // {
        //     vel += -pVelocity.y;
        //     vel += spring * -transform.localPosition.y * Time.deltaTime;
        //     vel += damper * -vel * Time.deltaTime;

        //     Vector3 pos = transform.localPosition;
        //     pos.y += vel * Time.deltaTime;
        //     transform.localPosition = pos;
        // }

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.parent.position + moveRelOffset);
            Gizmos.DrawCube(transform.parent.position + moveRelOffset, Vector3.one * 0.1f);
        }
    }
}