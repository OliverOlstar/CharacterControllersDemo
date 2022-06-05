using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Link
{
    public class Link_Crawl : BaseState
    {
        [SerializeField] private float rotateSpeed = 5.0f;
        [SerializeField] private float rotateBackwardSpeed = 4.0f;
        [SerializeField] private float colliderHeight = 1.5f;

        [Header("Components")]
        [SerializeField] private InputBridge_Platformer input = null;
        [SerializeField] private Transform cameraForward = null;
        [SerializeField] private CharacterController character = null;
        [SerializeField] private OnGround grounded = null;
        [SerializeField] private Link_AnimController animController = null;

        private float initalCharacterHeight;

        public override void Init(StateMachine pMachine)
        {
            base.Init(pMachine);
            
            initalCharacterHeight = character.height;

            input.onCrouchPerformed.AddListener(OnCrouchPerformed);
        }

        public override void OnEnter()
        {
            character.height = colliderHeight;
            character.center = new Vector3(0.0f, (colliderHeight * 0.5f) - 1, 0.0f);

            animController.SetCrouch(true);

            input.onCrouchCanceled.AddListener(OnCrouchCanceled);
        }

        public override void OnExit()
        {
            character.height = initalCharacterHeight;
            character.center = new Vector3(0.0f, (initalCharacterHeight * 0.5f) - 1, 0.0f);
            
            animController.SetCrouch(false);

            input.onCrouchCanceled.RemoveListener(OnCrouchCanceled);
            input.SetCrouch(false);
        }
        
        public override void OnFixedUpdate()
        {
            if (grounded.isGrounded)
            {
                if (input.moveInput != Vector2.zero)
                {
                    // Rotate input to camera
                    Vector3 move = cameraForward.TransformDirection(input.moveInputVector3);
                    // Convert to local space
                    move = transform.InverseTransformDirection(move);

                    if (move.z != 0)
                    {
                        bool forward = move.z >= 0;
                        if (move.x != 0)
                        {
                            transform.Rotate(new Vector3(0.0f, move.x * (forward ? rotateSpeed : -rotateBackwardSpeed) * Time.fixedDeltaTime, 0.0f), Space.World);
                        }
                        
                        animController.SetSpeed01(forward ? 1.0f : -1.0f);
                    }
                }
                else
                {
                    animController.SetSpeed01(0.0f);
                }
            }
            else
            {
                machine.ReturnToDefault();
            }
        }

        public void OnCrouchPerformed()
        {
            if (grounded.isGrounded && machine.IsDefaultState())
            {
                machine.SwitchState(this);
            }
        }

        public void OnCrouchCanceled()
        {
            machine.ReturnToDefault();
        }
    }
}