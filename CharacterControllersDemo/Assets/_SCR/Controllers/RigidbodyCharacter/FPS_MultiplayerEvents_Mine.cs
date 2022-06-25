using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace OliverLoescher.FPS
{
	[RequireComponent(typeof(PhotonView))]
	public class FPS_MultiplayerEvents_Mine : MonoBehaviour
	{
		private PhotonView photonView = null;
		//[SerializeField] private FPS.InputBridge_FPS input = null;
		[SerializeField] private RigidbodyCharacter movement = null;

		private void Start()
		{
			photonView = GetComponent<PhotonView>();
			if (photonView.IsMine)
			{
				movement.onStateChanged.AddListener(OnStateChanged);
			}
		}

		private void OnDestroy()
		{
			if (photonView.IsMine)
			{
				movement.onStateChanged.AddListener(OnStateChanged);
			}
		}

		public void OnStateChanged(RigidbodyCharacter.State pState)
		{
			photonView.RPC(nameof(FPS_MultiplayerEvents_Other.OnStateChanged), RpcTarget.Others, pState);
		}
	}
}