using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher
{
	public class CharacterCrouchMovement : MonoBehaviour
	{
		[SerializeField] protected new Rigidbody rigidbody = null;
		[SerializeField] private SmoothLocalOffset cameraOffset = null;
		[SerializeField] private RigidbodyAnimController animController = null;

		[Header("Crouch")]
		[SerializeField] private float crouchCameraOffset = -0.4f;

		[Header("Sliding")]
		[SerializeField] private Collider feetCollider = null;
		[SerializeField] private PhysicMaterial slidePhyicsMat = null;
		private PhysicMaterial initialPhyicsMat = null;

		[SerializeField] private float slideCameraOffset = -0.4f;
		[SerializeField] protected float sliderVelocity = 1.0f;

		private void Start()
		{
			initialPhyicsMat = feetCollider.material;
			Initalize();
		}
		protected virtual void Initalize() { }

		public void Play()
		{
			if (!CanPlay())
			{
				return;
			}
			if (rigidbody.velocity.magnitude < sliderVelocity)
			{
				StartCrouch();
			}
			else
			{
				StartSlide();
			}
		}
		public void Cancel()
		{
			animController.SetCrouched(false);
			animController.SetSlide(false);
			cameraOffset.ResetOffset();
			feetCollider.material = initialPhyicsMat;
			OnCancel();
		}

		public void StartCrouch()
		{
			animController.SetCrouched(true);
			animController.SetSlide(false);
			cameraOffset.ModifyInitialOffsetY(crouchCameraOffset);
			feetCollider.material = initialPhyicsMat;
			OnStartCrouch();
		}
		public void StartSlide()
		{
			animController.SetCrouched(false);
			animController.SetSlide(true);
			cameraOffset.ModifyInitialOffsetY(slideCameraOffset);
			feetCollider.material = slidePhyicsMat;
			OnStartSlide();
		}

		protected virtual bool CanPlay() => true;
		protected virtual void OnStartCrouch() { }
		protected virtual void OnStartSlide() { }
		protected virtual void OnCancel() { }
	}
}