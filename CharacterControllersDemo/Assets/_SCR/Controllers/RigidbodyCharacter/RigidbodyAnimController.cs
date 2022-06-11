using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
    public class RigidbodyAnimController : MonoBehaviour
    {
        private readonly string FORWARD = "MoveZ";
        private readonly string RIGHT = "MoveX";
        private readonly string CROUCHING = "Crouching";
        private readonly string SLIDING = "Sliding";
        private readonly string GROUNDED = "Grounded";
        private readonly string DODGING = "Dodging";
        private readonly string HARDLANDED = "HardLanding";

        [SerializeField] private Animator animator = null;

        [Header("Movement")]
        [SerializeField] private new Rigidbody rigidbody = null;
        [SerializeField] private RigidbodyCharacter movement = null;
        [SerializeField] private PlayerDodgeMovement dodgeMovement = null;
        [SerializeField, Min(0.01f)] private float scalar = 1.0f;
        [SerializeField, Min(0.01f)] private float dampening = 20.0f;

        [Header("Grounded")]
        [SerializeField] private OnGround onGround = null;

        [Header("Rotation")]
        [SerializeField] private Transform lookTransform = null;
        [SerializeField, Min(0)] private float rotationDampening = 0.0f;

		private void Start()
		{
            if (onGround != null)
            {
                onGround.OnEnter.AddListener(OnGroundedEnter);
                onGround.OnExit.AddListener(OnGroundedExit);
            }
            if (dodgeMovement != null)
            {
                dodgeMovement.DodgeStart.AddListener(DodgeStart);
                dodgeMovement.DodgeEnd.AddListener(DodgeEnd);
            }
            if (movement != null)
            {
                movement.OnHardLandStart.AddListener(OnHardLanded);
            }
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
        }

		private void FixedUpdate()
        {
            if (rigidbody != null)
            {
                Vector3 localVelocity = transform.InverseTransformDirection(rigidbody.velocity) * scalar;
                SetFloat(FORWARD, localVelocity.z, Time.fixedDeltaTime);
                SetFloat(RIGHT, localVelocity.x, Time.fixedDeltaTime);
            }
        }

        public void SetCrouched(bool pValue)
		{
            animator.SetBool(CROUCHING, pValue);
        }
        public void SetSlide(bool pValue)
        {
            animator.SetBool(SLIDING, pValue);
        }

        public void OnGroundedEnter() => animator.SetBool(GROUNDED, true);
        public void OnGroundedExit() => animator.SetBool(GROUNDED, false);
        public void OnHardLanded() => animator.SetTrigger(HARDLANDED);
        public void DodgeStart() => animator.SetBool(DODGING, true);
        public void DodgeEnd() => animator.SetBool(DODGING, false);

        private void SetFloat(in string key, in float value, in float deltaTime)
        {
            animator.SetFloat(key, Mathf.Lerp(animator.GetFloat(key), value, dampening * deltaTime));
        }
	}
}