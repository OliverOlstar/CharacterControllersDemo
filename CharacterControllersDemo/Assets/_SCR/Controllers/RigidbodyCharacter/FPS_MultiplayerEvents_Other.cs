using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace OliverLoescher.FPS
{
	[RequireComponent(typeof(PhotonView))]
	public class FPS_MultiplayerEvents_Other : MonoBehaviour
	{
		[SerializeField] private CharacterCrouchMovement crouchMovement = null;
		[SerializeField] private RigidbodyAnimController animController = null;

		private PhotonView photonView = null;
		private RigidbodyCharacter.State state = RigidbodyCharacter.State.Default;

		private void Start()
		{
			photonView = GetComponent<PhotonView>();
		}

		[PunRPC]
		public void OnStateChanged(RigidbodyCharacter.State pState)
		{
			// Exit State
			switch (state)
			{
				case RigidbodyCharacter.State.Default:

					break;

				case RigidbodyCharacter.State.InAir:

					break;

				case RigidbodyCharacter.State.Crouch:
				case RigidbodyCharacter.State.Slide:
					if (pState != RigidbodyCharacter.State.Slide && pState != RigidbodyCharacter.State.Crouch)
					{
						crouchMovement.Cancel();
					}
					break;

				case RigidbodyCharacter.State.HardLand:
					
					break;
			}

			// Start State
			state = pState;
			switch (state)
			{
				case RigidbodyCharacter.State.Default:

					break;

				case RigidbodyCharacter.State.InAir:

					break;

				case RigidbodyCharacter.State.Crouch:
					crouchMovement.StartCrouch();
					break;

				case RigidbodyCharacter.State.Slide:
					crouchMovement.StartSlide();
					break;

				case RigidbodyCharacter.State.HardLand:
					animController.OnHardLanded();
					break;
			}
		}
	}
}