using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.Link
{
    public class Link_Movement : BaseState
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5.0f;
        private float _moveSpeed = 0.0f;

        [Header("Slope")]
        [SerializeField, MinMaxSlider(0.0f, 180.0f, true)] private Vector2 slowAngleRange = new Vector2(10.0f, 90.0f);
        [SerializeField, Range(0, 1)] private float maxAngleSlow = 0.2f;

        [Header("Rotation")]
        [SerializeField] private float rotationDampening = 5.0f;
        [SerializeField] private float rotationIdleDampening = 25.0f;
        private Quaternion targetRotation = Quaternion.identity;

        [Header("Components")]
        [SerializeField] private InputBridge_Platformer input = null;
        [SerializeField] private Transform cameraForward = null;
        [SerializeField] private OnGround grounded = null;
        [SerializeField] private Link_AnimController animController = null;

        public override void Init(StateMachine pMachine)
        {
            base.Init(pMachine);

            _moveSpeed = moveSpeed;
        }
        
        public override void OnFixedUpdate()
        {
            if (grounded.isGrounded)
            {
                if (input.Move.Input != Vector2.zero)
                {
                    // Move values
                    Vector3 move = cameraForward.TransformDirection(input.Move.InputHorizontal);
                    move = new Vector3(move.x, 0.0f, move.z);

                    float angle = Vector3.Angle(transform.up, Vector3.ProjectOnPlane(grounded.GetAverageNormal(), transform.right));
                    float maxSpeed01 = 1 - (Util.SmoothStep(slowAngleRange, angle) * (1 - maxAngleSlow));

                    targetRotation = Quaternion.LookRotation(move);

                    // Movement
                    if (animController.GetSpeed01() > 0.1f || Quaternion.Angle(transform.rotation, targetRotation) < 30)
                        animController.SetSpeed01(input.Move.Input.magnitude * maxSpeed01);
                    
                    // Rotation
                    float rotSpeed = Mathf.Lerp(rotationIdleDampening, rotationDampening, animController.GetSpeed01()); 
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotSpeed);
                }
                else
                {
                    animController.SetSpeed01(0.0f);
                }            
            }
            else
            {
                animController.SetSpeed01(0.0f);
            }
        }
    }
}