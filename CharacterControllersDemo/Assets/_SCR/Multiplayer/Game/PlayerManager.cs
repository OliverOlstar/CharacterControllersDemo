using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;

namespace OliverLoescher.Multiplayer
{
	[RequireComponent(typeof(PhotonView))]
	public class PlayerManager : MonoBehaviour
	{
		public delegate void PlayerStateChanged(PlayerManager player);
		public static List<PlayerManager> Players = new List<PlayerManager>();
		public static PlayerStateChanged OnPlayerJoined;
		public static PlayerStateChanged OnPlayerLeft;
		public static PlayerStateChanged OnPlayerDied;

		private PhotonView photonView;
		private GameObject playerObject;
		private Health playerHealth;

		[SerializeField, AssetsOnly] private GameObject playerPrefab = null;
		[SerializeField, AssetsOnly] private GameObject othersPrefab = null; 

		private void Awake() 
		{
			Players.Add(this);
			OnPlayerJoined?.Invoke(this);

			photonView = GetComponent<PhotonView>();
		}

		private void OnDestroy()
		{
			Players.Remove(this);
			OnPlayerLeft?.Invoke(this);
		}

		private void Start() 
		{
			if (photonView.IsMine)
			{
				SpawnPlayer();
			}
			Camera.SpectatorCamera.Instance.gameObject.SetActive(false);
		}

		public void SpawnPlayer()
		{
			playerObject = Instantiate(playerPrefab);
			playerHealth = playerObject.GetComponent<Health>();
			playerHealth.onValueOut.AddListener(OnPlayerDeath);
			PhotonView pv = playerObject.GetComponent<PhotonView>();

			if (PhotonNetwork.AllocateViewID(pv))
			{
				photonView.RPC(nameof(RPC_SpawnOtherPlayer), RpcTarget.OthersBuffered, pv.ViewID);
			}
			else
			{
				Debug.LogError("Failed to allocate a ViewId.");
				Destroy(playerObject);
			}
		}

		[PunRPC]
		public void RPC_SpawnOtherPlayer(int pViewID)
		{
			GameObject player = Instantiate(othersPrefab);
			PhotonView photonView = player.GetComponent<PhotonView>();
			photonView.ViewID = pViewID;
		}

		private void OnPlayerDeath()
		{
			Camera.SpectatorCamera.Instance.gameObject.SetActive(true);
			OnPlayerDied?.Invoke(this);
		}

		private void RespawnPlayer()
		{
			Camera.SpectatorCamera.Instance.gameObject.SetActive(false);
			playerHealth.Respawn();
		}
	}
}