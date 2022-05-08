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
			playerObject.GetComponent<Health>().onValueOut.AddListener(OnPlayerDeath);
			PhotonView pv = playerObject.GetComponent<PhotonView>();

			if (PhotonNetwork.AllocateViewID(pv))
			{
				photonView.RPC("RPC_SpawnOtherPlayer", RpcTarget.OthersBuffered, pv.ViewID);
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
			GameObject player = (GameObject) Instantiate(othersPrefab);
			PhotonView photonView = player.GetComponent<PhotonView>();
			photonView.ViewID = pViewID;
		}

		private void OnPlayerDeath()
		{
			Camera.SpectatorCamera.Instance.gameObject.SetActive(true);
		}
	}
}