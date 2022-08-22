using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.FPS
{
    public class PlayerCrouchMovement : CharacterCrouchMovement
	{
		[Header("Player")]
		[SerializeField] private InputBridge_FPS fpsInput = null;
		[SerializeField] private RigidbodyCharacter movement = null;
        [SerializeField] private PlayerDodgeMovement dodgeMovement = null;

		protected override void Initalize()
		{
			if (fpsInput != null)
			{
				fpsInput.Crouch.onPerformed.AddListener(Play);
				fpsInput.Crouch.onCanceled.AddListener(Cancel);
				fpsInput.Sprint.onPerformed.AddListener(Cancel);
			}
			if (dodgeMovement != null)
			{
				dodgeMovement.DodgeStart.AddListener(Cancel);
			}
			if (movement != null)
			{
				movement.onStateChanged.AddListener(OnMoveStateChanged);
			}
		}

		protected override void OnStartCrouch()
		{
			movement.SetState(RigidbodyCharacter.State.Crouch);
		}
		protected override void OnStartSlide()
		{
			movement.SetState(RigidbodyCharacter.State.Slide);
		}
		protected override void OnCancel()
		{
			if (IsCrouchState())
			{
				movement.SetState(RigidbodyCharacter.State.Default);
			}
			fpsInput.ClearCrouchIfToggle();
		}

		protected override bool CanPlay() => movement.state == RigidbodyCharacter.State.Default;
		private bool IsCrouchState() => movement.state == RigidbodyCharacter.State.Crouch || movement.state == RigidbodyCharacter.State.Slide;

		public void OnMoveStateChanged(RigidbodyCharacter.State state)
		{
			if (state == RigidbodyCharacter.State.Default && fpsInput.Crouch.Input && fpsInput.Sprint.Input)
			{
				Play();
			}
			else if (!IsCrouchState())
			{
				Cancel();
			}
		}

		private void Update()
		{
			if (movement != null && movement.state == RigidbodyCharacter.State.Slide && rigidbody.velocity.magnitude < sliderVelocity)
			{
				StartCrouch();
			}
		}
	}
}