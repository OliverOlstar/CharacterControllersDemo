using System.Collections;
using System.Collections.Generic;
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

        private void Awake() 
        {
            photonView = GetComponent<PhotonView>();
        }

        private void Start() 
        {
            if (photonView.IsMine)
            {
                CreateController();
            }
        }

        private void CreateController()
        {
            playerObject = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CharacterController Root"), Vector3.zero, Quaternion.identity);
        }
    }
}