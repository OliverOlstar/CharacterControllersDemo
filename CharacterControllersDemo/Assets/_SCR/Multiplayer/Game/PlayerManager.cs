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
				SpawnSelfPlayer();
			}
			Camera.SpectatorCamera.Instance.gameObject.SetActive(false);
		}

		public void SpawnSelfPlayer()
		{
			Log($"{nameof(SpawnSelfPlayer)}()");
			playerObject = Instantiate(playerPrefab);
			playerObject.SetActive(false); // Disable player until RPC_SpawnPlayer is complete to prevent sending rpcs too soon
			playerHealth = playerObject.GetComponent<Health>();
			playerHealth.onValueOut.AddListener(OnSelfDeath);
			PhotonView pv = playerObject.GetComponent<PhotonView>();

			if (PhotonNetwork.AllocateViewID(pv))
			{
				photonView.RPC(nameof(RPC_SpawnPlayer), RpcTarget.AllBuffered, pv.ViewID);
			}
			else
			{
				Debug.LogError("Failed to allocate a ViewId.");
				Destroy(playerObject);
			}
		}

		[PunRPC]
		public void RPC_SpawnPlayer(int pViewID)
		{
			if (photonView.IsMine)
			{
				Log($"{nameof(RPC_SpawnPlayer)}() Self");
				playerObject.SetActive(true);
			}
			else
			{
				Log($"{nameof(RPC_SpawnPlayer)}() Other");
				othersPrefab.SetActive(false);
				GameObject playerObject = Instantiate(othersPrefab);
				playerHealth = playerObject.GetComponent<Health>();
				PhotonView photonView = playerObject.GetComponent<PhotonView>();
				photonView.ViewID = pViewID;
			}
		}

		[PunRPC]
		public void RPC_RespawnOtherPlayer()
		{
			Log($"{nameof(RPC_RespawnOtherPlayer)}()");
			playerHealth.Respawn();
		}

		private void OnSelfDeath()
		{
			Log($"{nameof(OnSelfDeath)}()");
			Camera.SpectatorCamera.Instance.gameObject.SetActive(true);
			OnPlayerDied?.Invoke(this);

			// Temp
			Invoke(nameof(RespawnSelf), 6.0f);
		}

		private void RespawnSelf()
		{
			Log($"{nameof(RespawnSelf)}()");
			Camera.SpectatorCamera.Instance.gameObject.SetActive(false);
			playerHealth.Respawn();
			photonView.RPC(nameof(RPC_RespawnOtherPlayer), RpcTarget.Others);
		}

		private void Log(in string pMessage)
		{
			string player = $"{photonView.ViewID}-{(photonView.IsMine ? "Mine" : "Other")}-{photonView.Owner.NickName}";
			Debug.Log($"[{nameof(PlayerManager)}] {player}: {pMessage}");
		}
	}
}