using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyJump : MonoBehaviour
    {
        // TODO add jump grace & cyote time & limit jump count
        [SerializeField] private float jumpUpVelocity = 5.0f;

        [Header("Stamina")]
        [SerializeField] private PlayerStamina stamina = null;
        [SerializeField, ShowIf("@stamina != null"), Min(0)] private float staminaUse = 30.0f;
        [SerializeField, ShowIf("@stamina != null"), Min(0)] private float staminaRequired = 0.0f;

        private Rigidbody rigid = null;

        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }
        
        public void OnJumpInput()
        {
            if (ValidStamina())
            {
                Vector3 vel = rigid.velocity;
                vel.y = jumpUpVelocity;
                rigid.velocity = vel;

                stamina.Modify(-staminaUse);
            }
        }

        private bool ValidStamina()
        {
            return stamina == null || (stamina.Get() >= staminaRequired && stamina.isOut == false);
        }
    }
}