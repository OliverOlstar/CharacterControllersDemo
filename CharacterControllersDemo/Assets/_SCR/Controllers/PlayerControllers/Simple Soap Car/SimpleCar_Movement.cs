using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OliverLoescher.SoapCar
{
    public class SimpleCar_Movement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rigid = null;
        [SerializeField] private OnGround grounded = null;

        [Header("Thrust")]
        [SerializeField] private float thrust = 5.0f;
        [SerializeField] private float topSpeed = 5.0f;
        [SerializeField] private float breakThrust = 2.0f;
        [SerializeField] private float thrustReverse = 2.0f;
        [SerializeField] private ForceMode forceMode = ForceMode.Acceleration;

        [Header("Rotation")]
        [SerializeField] private float rotation = 1.0f;
        [SerializeField] private float driftRotation = 2.0f;
        [SerializeField, Range(0, 1)] private float rotationVelocityPercent01 = 1.0f;

        private Vector3 offset;
        private Vector2 moveInput = new Vector2();

        private void Awake() 
        {
            InputSystem.Instance.SimpleCar.Move.performed += OnMovePerformed;
            InputSystem.Instance.SimpleCar.Drift.performed += OnDriftPerformed;
            InputSystem.Instance.SimpleCar.Drift.canceled += OnDriftCanceled;

            offset = transform.position - rigid.transform.position;
        }

        private void OnEnable() 
        {
            InputSystem.Instance.SimpleCar.Enable();
        }

        private void OnDisable() 
        {
            InputSystem.Instance.SimpleCar.Disable();
        }

        private void FixedUpdate() 
        {
            if (grounded.isGrounded)
            {
                bool movingForward = Vector3.Dot(Util.Horizontalize(transform.forward), Util.Horizontalize(rigid.velocity)) >= 0;
                bool inputingForward = moveInput.y >= 0;

                // Movement
                if (movingForward || inputingForward)
                {
                    if (rigid.velocity.magnitude < topSpeed)
                    {
                        float moveValue = moveInput.y * (moveInput.y > 0 ? thrust : breakThrust);
                        rigid.AddForce(transform.forward * moveValue * Time.fixedDeltaTime, forceMode);
                    }
                }
                else
                {
                    rigid.AddForce(transform.forward * moveInput.y * thrustReverse * Time.fixedDeltaTime, forceMode);
                }

                // Rotation
                Quaternion rot = Quaternion.Euler(0.0f, moveInput.x * (drift ? driftRotation : rotation) * Time.fixedDeltaTime * rigid.velocity.magnitude * (movingForward ? 1 : -1), 0.0f);
                transform.rotation *= rot;
                if (drift == false)
                {
                    if (rotationVelocityPercent01 < 1.0f)
                    {
                        rigid.velocity = Vector3.Lerp(rot * rigid.velocity, rigid.velocity, rotationVelocityPercent01);
                    }
                    else
                    {
                        rigid.velocity = rot * rigid.velocity;
                    }
                }
            }
        }
        
        public void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }
        
        private bool drift = false;
        public void OnDriftPerformed(InputAction.CallbackContext ctx)
        {
            drift = true;
        }
        public void OnDriftCanceled(InputAction.CallbackContext ctx)
        {
            drift = false;
        }
    }
}