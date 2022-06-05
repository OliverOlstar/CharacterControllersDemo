using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
    public class RigidbodyAnimController : MonoBehaviour
    {
        private readonly string FORWARD = "MoveZ";
        private readonly string RIGHT = "MoveX";
        private readonly string GROUNDED = "Grounded";
        private readonly string DODGING = "Dodging";
        private readonly string HARDLANDED = "HardLanding";

        [SerializeField] private Animator animator = null;

        [Header("Movement")]
        [SerializeField] private new Rigidbody rigidbody = null;
        [SerializeField] private RigidbodyCharacter movement = null;
        [SerializeField] private PlayerDodgeMovement dodgeMovement = null;
        [SerializeField, Min(0.01f)] private float scalar = 1.0f;

        [Header("Grounded")]
        [SerializeField] private OnGround onGround = null;

        [Header("Rotation")]
        [SerializeField] private Transform lookTransform = null;
        [SerializeField, Min(0)] private float rotationDampening = 0.0f;

		private void Start()
		{
            onGround.OnEnter.AddListener(OnGroundedEnter);
            onGround.OnExit.AddListener(OnGroundedExit);

            dodgeMovement.DodgeStart.AddListener(DodgeStart);
            dodgeMovement.DodgeEnd.AddListener(DodgeEnd);

            movement.OnHardLandStart.AddListener(OnHardLanded);
        }

		void Update()
        {
            Quaternion toRotation = Quaternion.LookRotation(MathUtil.Horizontalize(lookTransform.forward));
            if (rotationDampening > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationDampening * Time.deltaTime);
            }
            else
			{
                transform.rotation = toRotation;
			}

            Vector3 localVelocity =  transform.InverseTransformDirection(rigidbody.velocity) * scalar;
            animator.SetFloat(FORWARD, localVelocity.z);
            animator.SetFloat(RIGHT, localVelocity.x);

        }

        public void OnGroundedEnter() => animator.SetBool(GROUNDED, true);
        public void OnGroundedExit() => animator.SetBool(GROUNDED, false);
        public void OnHardLanded() => animator.SetTrigger(HARDLANDED);
        public void DodgeStart() => animator.SetBool(DODGING, true);
        public void DodgeEnd() => animator.SetBool(DODGING, false);

        private void OnDrawGizmos()
		{
            Gizmos.DrawLine(transform.position, transform.position + transform.InverseTransformDirection(rigidbody.velocity) * scalar);
		}
	}
}