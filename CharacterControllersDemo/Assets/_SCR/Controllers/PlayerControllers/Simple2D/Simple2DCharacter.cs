using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

namespace OliverLoescher
{
    public class Simple2DCharacter : MonoBehaviour
    {
        [SerializeField] private float force = 5;
        [SerializeField] private float jumpForce = 5;

        private PhotonView photonView;
        private Rigidbody2D rigid;
        private Vector2 moveInput = new Vector2();

        private void Awake() 
        {
            photonView = GetComponentInParent<PhotonView>();
            rigid = GetComponent<Rigidbody2D>();

            if (photonView == null || photonView.IsMine)
            {
                InputSystem.Input.Simple2D.Move.performed += OnMove;
                InputSystem.Input.Simple2D.Move.canceled += OnMove;
                InputSystem.Input.Simple2D.Jump.performed += OnJump;
            }
        }

        private void OnDestroy() 
        {
            if (photonView == null || photonView.IsMine)
            {
                InputSystem.Input.Simple2D.Move.performed -= OnMove;
                InputSystem.Input.Simple2D.Move.canceled -= OnMove;
                InputSystem.Input.Simple2D.Jump.performed -= OnJump;
            }
        }

        private void OnEnable()
        {
            if (photonView == null || photonView.IsMine)
            {
                InputSystem.Input.Simple2D.Enable();
            }
        }

        private void OnDisable() 
        {
            if (photonView == null || photonView.IsMine)
            {
                InputSystem.Input.Simple2D.Disable();
            }
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
            moveInput.y = 0;
        }

        private void FixedUpdate() 
        {
            if (moveInput != Vector2.zero)
            {
                // Move
                rigid.AddForce(moveInput * force * Time.fixedDeltaTime);
            }
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            // Jump
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}