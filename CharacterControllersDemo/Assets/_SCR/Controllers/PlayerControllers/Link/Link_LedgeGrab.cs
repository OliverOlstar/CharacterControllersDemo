using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Link
{
    public class Link_LedgeGrab : BaseState
    {
        [SerializeField] private LayerMask castLayerMask = new LayerMask();

        [Header("Forward Raycast")]
        [SerializeField] private float forwardCastDistance = 1.0f;
        [SerializeField, Range(0, 1)] private float forwardCastAllowedNormal = 0.2f;

        
        [Header("Down Raycast")]
        [SerializeField] private float downCastDistance = 2.0f;
        [SerializeField] private float downCastUp = 3.0f;

        [Header("Holding")]
        [SerializeField] private float holdDown = 0.5f;
        [SerializeField] private float holdDistance = 0.5f;

        private Vector3 wallTopPoint = new Vector3();
        private Vector3 wallNormal = new Vector3();

        [Header("Components")]
        [SerializeField] private InputBridge_Platformer input = null;
        [SerializeField] private RootMotionCharacter rootMotion = null;
        [SerializeField] private Link_AnimController animController = null;
        [SerializeField] private Link_Jump jump = null;

        private bool climbingUp = false;

        public override bool CanEnter()
        {
            if ((machine.IsDefaultState() || machine.IsState(jump)) && animController.GetSpeed01() > 0.5f)
                if (ForwardRaycast(out RaycastHit hitForward) && Mathf.Abs(hitForward.normal.y) < forwardCastAllowedNormal)
                    if (DownRaycast(hitForward.distance + 0.01f, out RaycastHit hitDown))
                    {
                        wallNormal = hitForward.normal;
                        wallTopPoint = hitDown.point;
                        return true;
                    }
            return false;
        }
        
        private bool ForwardRaycast(out RaycastHit pHit)
        {
            return Physics.Raycast(transform.position, transform.forward, out pHit, forwardCastDistance, castLayerMask);
        }
        
        private bool DownRaycast(float pDistance, out RaycastHit pHit)
        {
            Vector3 start = transform.position + (Vector3.up * downCastUp) + (transform.forward * pDistance);
            return Physics.Raycast(start, Vector3.down, out pHit, downCastDistance, castLayerMask);
        }

        public override void OnEnter()
        {
            StartCoroutine(OnEnterRoutine());
            input.Roll.onPerformed.AddListener(OnRollPerformed);
            climbingUp = false;
        }

        public override void OnExit()
        {
            StopAllCoroutines();
            input.Roll.onPerformed.RemoveListener(OnRollPerformed);

            rootMotion.ignoreYValue = false;
            animController.SetSpeed01(0.0f);
            animController.SetStrafeSpeed01(0.0f);
        }

        private IEnumerator OnEnterRoutine()
        {
            animController.TriggerClimb();
            animController.SetSpeed01(0.0f);
            rootMotion.ignoreYValue = true;
            yield return new WaitForSeconds(0.5f);
            transform.position = wallTopPoint + (Vector3.down * holdDown) + (wallNormal * holdDistance);
            transform.rotation = Quaternion.LookRotation(Util.Horizontalize(-wallNormal));
        }

        public override void OnUpdate()
        {
            animController.SetStrafeSpeed01(input.Move.Input.x);
            if (climbingUp == false && input.Move.Input.y == 1.0f)
            {
                DoClimbUp();
            }
        }

        private void DoClimbUp()
        {
            climbingUp = true;
            animController.TriggerJump();
            StartCoroutine(ClimbUpRoutine(1.5f));
        }

        private void OnRollPerformed()
        {
            animController.TriggerRoll();
            StartCoroutine(ClimbUpRoutine(0.2f));
        }

        private IEnumerator ClimbUpRoutine(float pDelay)
        {
            yield return new WaitForSeconds(pDelay);
            machine.ReturnToDefault();
        }

        private void OnDrawGizmosSelected() 
        {
            Vector3 start = transform.position;
            Vector3 end = start + (transform.forward * forwardCastDistance);
            Gizmos.color = ForwardRaycast(out RaycastHit hit) ? Color.green : Color.red;
            Gizmos.DrawLine(start, end);
            
            float dis = hit.distance + 0.01f;
            start = transform.position + (Vector3.up * downCastUp) + (transform.forward * dis);
            end = start + (Vector3.down * downCastDistance);
            Gizmos.color = DownRaycast(dis, out hit) ? Color.green : Color.red;
            Gizmos.DrawLine(start, end);
        }
    }
}
