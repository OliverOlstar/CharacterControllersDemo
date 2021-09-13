using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCar_Movement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rigid = null;
    private PlayerInput input = null;

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

    [Header("Grounded")]
    [SerializeField] private Vector3[] groundCheckPoints = new Vector3[1];
    [SerializeField] private float groundDownDistance = 0.1f;
    [SerializeField] private LayerMask groundLayers = new LayerMask();

    private Vector3 offset;
    private Vector2 moveInput = new Vector2();

    private void Awake() 
    {
        input = new PlayerInput();
        input.SimpleCar.Move.performed += OnMovePerformed;
        input.SimpleCar.Drift.performed += OnDriftPerformed;
        input.SimpleCar.Drift.canceled += OnDriftCanceled;

        offset = transform.position - rigid.transform.position;
    }

    private void OnEnable() 
    {
        input.SimpleCar.Move.Enable();
        input.SimpleCar.Drift.Enable();
    }

    private void OnDisable() 
    {
        input.SimpleCar.Move.Disable();
        input.SimpleCar.Drift.Disable();
    }

    private void FixedUpdate() 
    {
        if (GroundedRaycast())
        {
            bool movingForward = Vector3.Dot(Horizontalize(transform.forward), Horizontalize(rigid.velocity)) >= 0;
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

    private bool GroundedRaycast()
    {
        foreach (Vector3 point in groundCheckPoints)
        {
            Vector3 start = transform.TransformPoint(point);
            if (Physics.Linecast(start, start - (transform.up * groundDownDistance), out RaycastHit hit, groundLayers))
            {
                return true;
            }
        }
        return false;
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

    private Vector3 Horizontalize(Vector3 pVector)
    {
        return new Vector3(pVector.x, 0.0f, pVector.z);
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.cyan;
        foreach (Vector3 point in groundCheckPoints)
        {
            Vector3 start = transform.TransformPoint(point);
            Gizmos.DrawLine(start, start - (transform.up * groundDownDistance));
        }
    }
}
