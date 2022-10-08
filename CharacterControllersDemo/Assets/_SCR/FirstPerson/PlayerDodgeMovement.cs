using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
    public class PlayerDodgeMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rigid = null;
        [SerializeField] private Transform cameraForward = null;

        [Header("Movement")]
        [SerializeField, Min(int.MinValue)] private float dodgeDistance = 1.0f;
        [SerializeField, Min(int.MinValue)] private float dodgeSeconds = 1.0f;
        [SerializeField] private float dodgeEndVelocityMagnitude = 1.0f;

        [Header("Input")]
        [SerializeField, Range(0, 1)] private float dodgeThumbstickDeadZone = 0.2f;
        [SerializeField, Min(int.MinValue)] private float dodgeInputSeconds = 0.2f;
        [SerializeField, Min(0)] private float dodgeAgainDelay = 0.5f;

        [Header("Stamina")]
        [SerializeField] private PlayerStamina stamina = null;
        [SerializeField, ShowIf("@stamina != null"), Min(0)] private float staminaUse = 50.0f;
        [SerializeField, ShowIf("@stamina != null"), Min(0)] private float staminaRequired = 0.0f;

        private int currQuadrent = 0;
        private int lastQuadrent = 0;
        private bool isDodging = false;

        // Events
        [FoldoutGroup("Events")] public UnityEvent DodgeStart;
        [FoldoutGroup("Events")] public UnityEvent DodgeEnd;

        public void OnMoveInput(Vector2 pInput) 
        {
            if (isDodging)
                return;

            int nextQuadrent = GetQuadrent(pInput);
            if (nextQuadrent != currQuadrent)
            {
                if (nextQuadrent == 0) // Moved to deadzone
                {
                    // Cache exiting quadrent to dodge if returned
                    lastQuadrent = currQuadrent;
                    Invoke(nameof(ClearLastQuadrent), dodgeInputSeconds);
                }
                else if (lastQuadrent != 0) // Moved away from deadzone
                {
                    // Returned to last quadrent, do dodge
                    if (nextQuadrent == lastQuadrent && ValidStamina())
                    {
                        StartCoroutine(DodgeRoutine(nextQuadrent));
                    }

                    // Clear lastQuadrent
                    lastQuadrent = 0;
                    CancelInvoke(nameof(ClearLastQuadrent));
                }
                currQuadrent = nextQuadrent;
            }
        }

        private IEnumerator DodgeRoutine(int pQuadrent)
        {
            isDodging = true;
            DodgeStart?.Invoke();
            // rigid.useGravity = false;

            if (stamina != null)
                stamina.Modify(-staminaUse);

            Vector3 dir = Util.Horizontalize(GetQuadrentVector(pQuadrent));
            float inverseDodgeSeconds = 1 / dodgeSeconds;
            Vector3 velocity = dir * dodgeDistance * inverseDodgeSeconds;

            // Dodge Movement
            float progress = 0;
            while (progress < 1)
            {
                progress += Time.fixedDeltaTime * inverseDodgeSeconds;
                rigid.velocity = velocity;
                yield return new WaitForFixedUpdate();
            }

            // rigid.useGravity = true;
            rigid.velocity = rigid.velocity.normalized * dodgeEndVelocityMagnitude;
            DodgeEnd?.Invoke();
            
            // Dodge again delay
            if (dodgeAgainDelay > 0)
            {
                yield return new WaitForSeconds(dodgeAgainDelay);
            }

            // Reset to allowing dodge input
            currQuadrent = 0;
            lastQuadrent = 0;
            isDodging = false;
        }

        private void ClearLastQuadrent()
        {
            lastQuadrent = 0;
        }

        private int GetQuadrent(Vector2 pInput)
        {
            if (pInput.sqrMagnitude > Mathf.Pow(dodgeThumbstickDeadZone, 2))
            {
                if (Mathf.Abs(pInput.x) > Mathf.Abs(pInput.y))
                {
                    if (pInput.x > 0)
                    {
                        return 4; // Right
                    }
                    else
                    {
                        return 2; // Left
                    }
                }
                else
                {
                    if (pInput.y > 0)
                    {
                        return 1; // Forward
                    }
                    else
                    {
                        return 3; // Back
                    }
                }
            }
            return 0; // Deadzone
        }

        private Vector3 GetQuadrentVector(int pQuadrent)
        {
            switch (pQuadrent)
            {
                case 1:
                    return cameraForward.forward;
                    
                case 2:
                    return -cameraForward.right;
                    
                case 3:
                    return -cameraForward.forward;
                    
                case 4:
                    return cameraForward.right;
            }
            return Vector3.zero;
        }

        private bool ValidStamina()
        {
            return stamina == null || (stamina.Get() >= staminaRequired && stamina.isOut == false);
        }
    }
}