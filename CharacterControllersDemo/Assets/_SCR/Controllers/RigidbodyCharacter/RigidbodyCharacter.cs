using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace OliverLoescher
{
	[RequireComponent(typeof(Rigidbody), typeof(OnGround))]
    public class RigidbodyCharacter : MonoBehaviour
    {
        public enum State
		{
            Default,
            InAir,
            Crouch,
            Slide,
            HardLand,
        }

        [SerializeField] private Transform cameraForward = null;
        private Rigidbody rigid = null;
        private OnGround grounded = null;
        
        [Header("Default")]
        [SerializeField, Min(0)] private float maxVelocity = 2.0f;
        [SerializeField, Min(0)] private float acceleration = 5.0f;

        [Space]
        [SerializeField, Min(0)] private float sprintMaxSpeed = 10.0f;
        [SerializeField, Min(0)] private float sprintAcceleration = 10.0f;
        
        [Space]
        [SerializeField, Min(0)] private float airAcceleration = 2.0f;
        [SerializeField, Min(0)] private float airDrag = 0.0f;

        [Header("Crouch")]
        [SerializeField, Min(0)] private float crouchMaxSpeed = 2.0f;
        [SerializeField, Min(0)] private float crouchAcceleration = 5.0f;

        [Space]
        [SerializeField, Min(0)] private float slideMaxSpeed = 2.0f;
        [SerializeField, Min(0)] private float slideAcceleration = 5.0f;
        [SerializeField, Min(0)] private float slideDrag = 5.0f;

        [Header("Hard Landing")]
        [SerializeField, MaxValue(0)] private float hardLandingYVelocityTrigger = -2f;
        [SerializeField, Min(0)] private float hardLandingDrag = 2.0f;
		[SerializeField, Min(0)] private float hardLandingSeconds = 0.5f;
        [HideInInspector] public UnityEvent OnHardLandStart;
        [HideInInspector] public UnityEvent OnHardLandEnd;

        [Header("Stamina")]
        [SerializeField] private PlayerStamina stamina = null;
        [SerializeField, ShowIf("@stamina != null")] private float staminaPerSecond = 25.0f;

        private Vector2 moveInput = Vector2.zero;
        private float accel = 0;
        private float maxVel = 0;
        private float initialDrag;
        private bool isSprinting = false;
        public State state { get; private set; } = State.Default;

        public UnityEvent<State> onStateChanged;

        private void Start() 
        {
            rigid = GetComponent<Rigidbody>();
            grounded = GetComponent<OnGround>();
            
            accel = acceleration;
            maxVel = maxVelocity;
            initialDrag = rigid.drag;

            if (stamina != null)
            {
                stamina.onValueIn.AddListener(OnStaminaIn);
                stamina.onValueOut.AddListener(OnStaminaOut);
            }

            grounded.OnEnter.AddListener(OnGroundedEnter);
            grounded.OnExit.AddListener(OnGroundedExit);
        }

		private void OnDisable()
		{
            ClearHardLanding();
        }

        private void FixedUpdate() 
        {
            if (accel != 0 && moveInput != Vector2.zero)
            {
                // Move values
                Vector3 move = cameraForward.TransformDirection(new Vector3(moveInput.x, 0.0f, moveInput.y));
                move = new Vector3(move.x, 0.0f, move.z).normalized * moveInput.magnitude;
                move = move * accel * Time.fixedDeltaTime;

                // Clamp to max speed
                Vector3 vel = new Vector3(rigid.velocity.x, 0.0f, rigid.velocity.z);
                if ((vel + move).sqrMagnitude >= Mathf.Pow(maxVel, 2))
                {
                    move = Vector3.ClampMagnitude(move + vel, vel.magnitude) - vel;
                }
                
                // Stamina
                if (isSprinting && state == State.Default && !stamina.isOut && stamina != null)
                    stamina.Modify(-Time.deltaTime * staminaPerSecond);

                // Actually move
                if (move != Vector3.zero)
                    rigid.AddForce(move, ForceMode.VelocityChange);
            }
        }

        public void OnMoveInput(Vector2 pInput)
        {
            moveInput = pInput;
        }

        public void OnSprintInput(bool pInput)
        {
            isSprinting = pInput;
            UpdateDefaultState();
        }

        public void OnCrouch(bool pInput)
		{
            if (pInput)
			{
                SetState(State.Crouch);
			}
            else if (state == State.Crouch)
            {
                SetState(State.Default);
            }
		}

        private void OnGroundedEnter()
        {
            if (rigid.velocity.y < hardLandingYVelocityTrigger)
            {
                SetState(State.HardLand);
                Invoke(nameof(ClearHardLanding), hardLandingSeconds);
                OnHardLandStart?.Invoke();
                return;
            }
            SetState(State.Default);
        }

        private void OnGroundedExit()
        {
            SetState(State.InAir);
        }

        public void ClearHardLanding()
        {
            if (state == State.HardLand)
            {
                SetState(State.Default);
                OnHardLandEnd?.Invoke();
            }
        }

        public void OnStaminaOut()
        {
            if (isSprinting)
                UpdateDefaultState();
        }

        public void OnStaminaIn()
        {
            if (isSprinting)
                UpdateDefaultState();
        }

        public void SetState(State pState)
		{
            if (state == pState)
			{
                return;
			}

            state = pState;
            switch (state)
			{
                case State.Default:
                    UpdateDefaultState();
                    break;

                case State.InAir:
                    accel = airAcceleration;
                    maxVel = maxVelocity;
                    rigid.drag = airDrag;
                    break;

                case State.Crouch:
                    accel = crouchAcceleration;
                    maxVel = crouchMaxSpeed;
                    rigid.drag = initialDrag;
                    break;

                case State.Slide:
                    accel = slideAcceleration;
                    maxVel = slideMaxSpeed;
                    rigid.drag = slideDrag;
                    break;

                case State.HardLand:
                    accel = 0;
                    maxVel = 0;
                    rigid.drag = hardLandingDrag;
                    break;
            }
            onStateChanged?.Invoke(state);

        }

        private void UpdateDefaultState()
        {
            if (state != State.Default)
            {
                return;
            }

            if (isSprinting && !stamina.isOut)
            {
                accel = sprintAcceleration;
                maxVel = sprintMaxSpeed;
                rigid.drag = initialDrag;
            }
            else
            {
                accel = acceleration;
                maxVel = maxVelocity;
                rigid.drag = initialDrag;
            }
        }
    }
}
