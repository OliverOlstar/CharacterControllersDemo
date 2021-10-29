using UnityEngine;
using Photon.Pun;
using System.IO;

namespace OliverLoescher.Multiplayer
{
    [RequireComponent(typeof(PhotonView))]
    public class PlayerManager : MonoBehaviour
    {
        private PhotonView photonView;
        private GameObject playerObject;

        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] private GameObject othersPrefab = null; 

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
        }

        public void SpawnPlayer()
        {
            playerObject = Instantiate(playerPrefab);
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

        // private void CreateController()
        // {
        //     playerObject = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CharacterController Root"), Vector3.zero, Quaternion.identity);
        // }
    }
}