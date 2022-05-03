using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher.Link
{
    public class Link_Jump : BaseState
    {
        [SerializeField] private Link_AnimController animController = null;
        [SerializeField] private RootMotionCharacter rootMotion = null;
        [SerializeField] private OnGround grounded = null;

        [Header("Jump")]
        [SerializeField] private float jumpForwardForce = 5.0f;
        [SerializeField] private float jumpUpForce = 5.0f;

        [Header("Linecast")]
        [SerializeField] private float minSpeed = 5.0f;
        [FoldoutGroup("Line"), SerializeField] private float jumpCheckDistance = 1.0f;
        [FoldoutGroup("Line"), SerializeField] private float jumpCheckUp = 1.0f;
        [FoldoutGroup("Line"), SerializeField] private float jumpCheckDown = 1.0f;
        [FoldoutGroup("Line"), SerializeField] private LayerMask jumpLayermask = new LayerMask();

        private float canExitTime;

        [FoldoutGroup("Gizmos"), SerializeField] private float debugLineSpacing = 0.1f;
        [FoldoutGroup("Gizmos"), SerializeField] private int debugLineRepeat = 20;
        [FoldoutGroup("Gizmos")] private Vector3 debugGroundPoint = new Vector3();

        public override void OnEnter()
        {
            animController.TriggerJump();
            rootMotion.DoJump(jumpUpForce, jumpForwardForce);
            canExitTime = Time.time + 0.5f;
        }

        public override bool CanEnter()
        {
            if (animController.GetSpeed01() >= minSpeed)
            {
                Vector3 start = transform.position + (transform.forward * jumpCheckDistance) + (Vector3.up * jumpCheckUp);
                Vector3 end = start + (Vector3.down * jumpCheckDown);
                if (grounded.isGrounded && Physics.Linecast(start, end, jumpLayermask) == false)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool CanExit()
        {
            return (Time.time > canExitTime && grounded.isGrounded);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;

            if (Application.isPlaying && animController.GetSpeed01() >= minSpeed)
            {
                Vector3 start = transform.position + (transform.forward * jumpCheckDistance) + (Vector3.up * jumpCheckUp);
                Vector3 end = start + (Vector3.down * jumpCheckDown);
                Gizmos.DrawLine(start, end);
            }

            // Jump Line
            if (grounded.isGrounded)
                debugGroundPoint = transform.position;

            Vector3 velocity = Vector3.up * jumpForwardForce + FuncUtil.Horizontalize(transform.forward) * Mathf.Sqrt(2 * 9.81f * jumpUpForce);
            Vector3 point = debugGroundPoint;
            Vector3 lastPoint = point;
            for (int i = 0; i < debugLineRepeat; i++)
            {
                velocity.y -= 9.81f * debugLineSpacing;
                point += velocity * debugLineSpacing;
                Gizmos.DrawLine(lastPoint, point);
                lastPoint = point;
            }
        }
    }
}
