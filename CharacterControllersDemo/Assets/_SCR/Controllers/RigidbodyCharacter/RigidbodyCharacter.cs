using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
    [RequireComponent(typeof(Rigidbody), typeof(OnGround))]
    public class RigidbodyCharacter : MonoBehaviour
    {
        [SerializeField] private Transform cameraForward = null;
        private Rigidbody rigid = null;
        private OnGround grounded = null;
        
        [SerializeField, Min(0)] private float maxVelocity = 2.0f;
        [SerializeField, Min(0)] private float acceleration = 5.0f;

        [Space]
        [SerializeField, Min(0)] private float sprintMaxSpeed = 10.0f;
        [SerializeField, Min(0)] private float sprintAcceleration = 10.0f;
        
        [Space]
        [SerializeField, Min(0)] private float airAcceleration = 2.0f;
        [SerializeField, Min(0)] private float airDrag = 0.0f;

        [Header("Stamina")]
        [SerializeField] private PlayerStamina stamina = null;
        [SerializeField, ShowIf("@stamina != null")] private float staminaPerSecond = 25.0f;

        private Vector2 moveInput = Vector2.zero;
        private float accel = 0;
        private float maxVel = 0;
        private float initialDrag;
        private bool isGrounded = false;
        private bool isSprinting = false;

        private void Start() 
        {
            rigid = GetComponent<Rigidbody>();
            grounded = GetComponent<OnGround>();
            
            accel = acceleration;
            maxVel = maxVelocity;
            initialDrag = rigid.drag;

            if (stamina != null)
            {
                stamina.onValueIn.AddListener(OnStaminaIn);
                stamina.onValueOut.AddListener(OnStaminaOut);
            }

            grounded.OnEnter.AddListener(OnGroundedEnter);
            grounded.OnExit.AddListener(OnGroundedExit);
        }

        public void OnMoveInput(Vector2 pInput)
        {
            moveInput = pInput;
        }

        public void OnSprintInput(bool pBool)
        {
            isSprinting = pBool;
            UpdateSpeeds();
        }

        private void FixedUpdate() 
        {
            if (grounded.isGrounded)
            {
                if (isGrounded == false)
                    OnGroundedEnter();
            }
            else
            {
                if (isGrounded == true)
                    OnGroundedExit();
            }

            if (accel != 0 && moveInput != Vector2.zero)
            {
                // Move values
                Vector3 move = cameraForward.TransformDirection(new Vector3(moveInput.x, 0.0f, moveInput.y));
                move = new Vector3(move.x, 0.0f, move.z).normalized * moveInput.magnitude;
                move = move * accel * Time.fixedDeltaTime;

                // Clamp to max speed
                Vector3 vel = new Vector3(rigid.velocity.x, 0.0f, rigid.velocity.z);
                if ((vel + move).sqrMagnitude >= Mathf.Pow(maxVel, 2))
                {
                    float maxMag = vel.magnitude;
                    move = Vector3.ClampMagnitude(move + vel, vel.magnitude) - vel;
                }
                
                // Stamina
                if (isSprinting && isGrounded && !stamina.isOut && stamina != null)
                    stamina.Modify(-Time.deltaTime * staminaPerSecond);

                // Actually move
                if (move != Vector3.zero)
                    rigid.AddForce(move, ForceMode.VelocityChange);
            }
        }

        private void OnGroundedEnter()
        {
            isGrounded = true;
            UpdateSpeeds();
        }

        private void OnGroundedExit()
        {
            isGrounded = false;
            UpdateSpeeds();
        }

        public void OnStaminaOut()
        {
            if (isSprinting)
                UpdateSpeeds();
        }

        public void OnStaminaIn()
        {
            if (isSprinting)
                UpdateSpeeds();
        }

        private void UpdateSpeeds()
        {
            if (isGrounded == false)
            {
                accel = airAcceleration;
                maxVel = maxVelocity;
                rigid.drag = airDrag;
            }
            else if (isSprinting && !stamina.isOut)
            {
                accel = sprintAcceleration;
                maxVel = sprintMaxSpeed;
                rigid.drag = initialDrag;
            }
            else
            {
                accel = acceleration;
                maxVel = maxVelocity;
                rigid.drag = initialDrag;
            }
        }
    }
}
