using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

namespace OliverLoescher.Multiplayer
{
	[RequireComponent(typeof(PhotonView))]
	public class PlayerManager : MonoBehaviour
	{
		private PhotonView photonView;
		private GameObject playerObject;
		private Health playerHealth;

		[SerializeField, AssetsOnly] private GameObject playerPrefab = null;
		[SerializeField, AssetsOnly] private GameObject othersPrefab = null; 

		private void Awake() 
		{
			photonView = GetComponent<PhotonView>();
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
			Invoke(nameof(RespawnPlayer), 5);
		}

		private void RespawnPlayer()
		{
			Camera.SpectatorCamera.Instance.gameObject.SetActive(false);
			playerHealth.Respawn();
		}
	}
}