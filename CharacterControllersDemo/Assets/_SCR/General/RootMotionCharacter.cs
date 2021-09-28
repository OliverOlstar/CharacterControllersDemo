using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher 
{
    [RequireComponent(typeof(Animator))]
    public class RootMotionCharacter : MonoBehaviour
    {
        [Header("Root Motion")]
        [SerializeField] private CharacterController character = null;
        [SerializeField] private OnGround grounded = null;
        private Animator animator;

        [Space, SerializeField] private float gravity = 9.81f;
        [SerializeField] private float stepDown = 0.1f; 
        [SerializeField] private float slopeLimit = 45.0f;
        [SerializeField] private float slideFriction = 1.0f;
        private bool inAir = false;
        private Vector3 rootMotion = new Vector3();
        private Vector3 velocity = Vector3.zero;

        [Space, SerializeField] private float pushPower = 2.0f;

        void Start()
        {
            animator = GetComponent<Animator>();

            RootMotionCharacterReciever reciever = character.gameObject.AddComponent<RootMotionCharacterReciever>();
            reciever.Init(this);
        }

        private void OnAnimatorMove() 
        {
            rootMotion += animator.deltaPosition;

            character.transform.rotation *= animator.deltaRotation;
        }

        private void FixedUpdate() 
        {
            if (inAir)
            {
                UpdateInAir();
            }
            else
            {
                UpdateOnGround();
            }
        }

        private void UpdateInAir()
        {
            // Gravity
            velocity.y -= gravity * Time.fixedDeltaTime;

            character.Move(velocity * Time.fixedDeltaTime);

            if (character.isGrounded == true)
            {
                inAir = false;
                rootMotion = Vector3.zero;
            }
        }

        private void UpdateOnGround()
        {
            character.Move(rootMotion);
            rootMotion = Vector3.zero;

            if (grounded.IsGrounded())
            {
                character.Move(Vector3.down * stepDown);
            }

            if (character.isGrounded == false)
            {
                inAir = true;
                velocity = animator.velocity;
            }
        }

        public void DoJump(float pUp, float pForward)
        {
            inAir = true;
            velocity = FuncUtil.Horizontalize(animator.velocity, true) * pForward;
            velocity.y = Mathf.Sqrt(2 * gravity * pUp);
        }

        public void OnControllerColliderHit(ControllerColliderHit hit)
        {

            Rigidbody body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null || body.isKinematic)
                return;

            // We dont want to push objects below us
            if (hit.moveDirection.y < -0.3f)
                return;

            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // Apply the push
            body.velocity = pushDir * pushPower;
        }
    }
}