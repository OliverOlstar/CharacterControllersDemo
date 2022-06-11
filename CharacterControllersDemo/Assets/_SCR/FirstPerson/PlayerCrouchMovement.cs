using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.FPS
{
    public class PlayerCrouchMovement : MonoBehaviour
	{
		[SerializeField] private InputBridge_FPS fpsInput = null;
		[SerializeField] private new Rigidbody rigidbody = null;
		[SerializeField] private RigidbodyCharacter movement = null;
        [SerializeField] private PlayerDodgeMovement dodgeMovement = null;
        [SerializeField] private SmoothLocalOffset cameraOffset = null;
		[SerializeField] private RigidbodyAnimController animController = null;

		[Header("Crouch")]
		[SerializeField] private float crouchCameraOffset = -0.4f;

		[Header("Sliding")]
		[SerializeField] private Collider feetCollider = null;
		[SerializeField] private PhysicMaterial slidePhyicsMat = null;
		private PhysicMaterial initialPhyicsMat = null;

		[SerializeField] private float slideCameraOffset = -0.4f;
		[SerializeField] private float sliderVelocity = 1.0f;

		private void Start()
		{
			if (fpsInput != null)
			{
				fpsInput.onCrouchPerformed.AddListener(CrouchStart);
				fpsInput.onCrouchCanceled.AddListener(CrouchCancel);
				fpsInput.onSprintPerformed.AddListener(CrouchCancel);
			}
			if (dodgeMovement != null)
			{
				dodgeMovement.DodgeStart.AddListener(CrouchCancel);
			}
			if (movement != null)
			{
				movement.onStateChanged.AddListener(OnMoveStateChanged);
			}

			initialPhyicsMat = feetCollider.material;
		}

		public void CrouchStart()
		{
			if (movement.state != RigidbodyCharacter.State.Default)
			{
				return;
			}
			if (rigidbody.velocity.magnitude < sliderVelocity)
			{
				StartCrouchInternal();
			}
			else
			{
				StartSlideInternal();
			}
		}
		private void StartCrouchInternal()
		{
			movement.SetState(RigidbodyCharacter.State.Crouch);
			animController.SetCrouched(true);
			animController.SetSlide(false);
			cameraOffset.ModifyInitialOffsetY(crouchCameraOffset);
			feetCollider.material = initialPhyicsMat;
		}
		private void StartSlideInternal()
		{
			movement.SetState(RigidbodyCharacter.State.Slide);
			animController.SetCrouched(false);
			animController.SetSlide(true);
			cameraOffset.ModifyInitialOffsetY(slideCameraOffset);
			feetCollider.material = slidePhyicsMat;
		}

		public void CrouchCancel(bool _) => CrouchCancel();
		public void CrouchCancel()
		{
			if (IsCrouchState(movement.state))
			{
				movement.SetState(RigidbodyCharacter.State.Default);
			}
			fpsInput.ClearCrouchIfToggle();
			animController.SetCrouched(false);
			animController.SetSlide(false);
			cameraOffset.ResetOffset();
			feetCollider.material = initialPhyicsMat;
		}

		public void OnMoveStateChanged(RigidbodyCharacter.State state)
		{
			if (state == RigidbodyCharacter.State.Default && fpsInput.isCrouching && fpsInput.isSprinting)
			{
				CrouchStart();
			}
			else if (!IsCrouchState(movement.state))
			{
				CrouchCancel();
			}
		}

		private bool IsCrouchState(RigidbodyCharacter.State state) => state == RigidbodyCharacter.State.Crouch || state == RigidbodyCharacter.State.Slide;

		private void Update()
		{
			if (movement != null && movement.state == RigidbodyCharacter.State.Slide)
			{
				if (rigidbody.velocity.magnitude < sliderVelocity)
				{
					StartCrouchInternal();
				}
			}
		}
	}
}