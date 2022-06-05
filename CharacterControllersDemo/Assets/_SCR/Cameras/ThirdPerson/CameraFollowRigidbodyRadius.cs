using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher 
{
    public class CameraFollowRigidbodyRadius : MonoBehaviour
    {
        [SerializeField] private Rigidbody target = null;

        [Header("Look")]
        [SerializeField] private float lookVelocity = 1.0f;
        [SerializeField] private Vector3 lookOffset = new Vector3();
        [SerializeField] private float lookDampening = 5.0f;

        [Header("Follow")]
        [SerializeField] private float followDistance = 9.0f;
        [SerializeField] private float followHeight = 2.0f;
        [SerializeField] private float followDampening = 1.0f;

        void Update()
        {
			Vector3 offset = MathUtil.Horizontalize(transform.position - target.position, true) * followDistance; // x, z
            offset.y = followHeight; // y
            transform.position = Vector3.Lerp(transform.position, target.position + offset, followDampening * Time.deltaTime);

            Vector3 lookAtTarget = target.transform.position + (target.velocity * lookVelocity) + lookOffset;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAtTarget - transform.position), Time.deltaTime * lookDampening);
        }

        private void OnDrawGizmosSelected() 
        {
            // transform.position = target.position + followOffset;
            transform.LookAt(target.transform.position + (target.velocity * lookVelocity) + lookOffset);
        }
    }
}