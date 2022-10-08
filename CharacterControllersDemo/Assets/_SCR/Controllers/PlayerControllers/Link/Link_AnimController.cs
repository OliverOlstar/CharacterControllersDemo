using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Link
{
    [RequireComponent(typeof(Animator))]
    public class Link_AnimController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private OnGround grounded = null;

        [Header("Animator")]
        [SerializeField] private float speedDampening = 1.0f;
        private float targetSpeed = 0.0f;
        private float targetStrafeSpeed = 0.0f;
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TriggerTurnAround()
        {
            animator.SetTrigger("TurnAround");
        }

        public void TriggerJump()
        {
            animator.SetTrigger("Jump");
        }

        public void TriggerRoll()
        {
            animator.SetTrigger("Roll");
        }

        public void SetCrouch(bool pBool)
        {
            animator.SetBool("Crouched", pBool);
        }

        public void TriggerClimb()
        {
            animator.SetTrigger("Climb");
        }

        public void SetSpeed01(float pValue01, bool pInstant = false)
        {
            targetSpeed = pValue01;
            if (pInstant)
                animator.SetFloat("Speed", pValue01);
        }
        public float GetSpeed01()
        {
            return animator.GetFloat("Speed");
        }

        public void SetStrafeSpeed01(float pValue01, bool pInstant = false)
        {
            targetStrafeSpeed = pValue01;
            if (pInstant)
                animator.SetFloat("StrafeSpeed", pValue01);
        }
        public float GetStrafeSpeed01()
        {
            return animator.GetFloat("StrafeSpeed");
        }

        private void Update() 
        {
            LerpValue("Speed", targetSpeed, speedDampening);
            LerpValue("StrafeSpeed", targetStrafeSpeed, speedDampening);
        }

        private void LerpValue(string pValue, float pTarget01, float pDampening)
        {
            float v = animator.GetFloat(pValue);
            if (pTarget01 != v)
            {
                if (Mathf.Abs(pTarget01 - v) < Util.NEARZERO)
                {
                    // Round if close enough
                    animator.SetFloat(pValue, (pTarget01));
                }
                else
                {
                    // Lerp value
                    animator.SetFloat(pValue, Mathf.Lerp(v, pTarget01, Time.deltaTime * pDampening));
                }
            }
        }

        private void FixedUpdate() 
        {
            animator.SetBool("Grounded", grounded.isGrounded);
        }
    }
}