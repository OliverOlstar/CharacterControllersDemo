using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

namespace OliverLoescher.Multiplayer
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
#region Singleton
        public static Launcher Instance = null;
        private void Awake() 
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Debug.LogError("[Launcher.cs] Instance != null, destroying self", gameObject);
                Destroy(this);
            }
        }
#endregion    

        [SerializeField] private TMP_InputField niknameInputField = null;
        [SerializeField] private TMP_InputField roomNameInputField = null;
        [SerializeField] private TMP_Text errorText = null;
        [SerializeField] private TMP_Text roomNameText = null;
        [SerializeField] private Transform roomListContent = null;
        [SerializeField] private GameObject roomListPrefab = null;
        [SerializeField] private Transform playerListContent = null;
        [SerializeField] private GameObject playerListPrefab = null;
        [SerializeField] private GameObject[] roomHostObjects = new GameObject[0];

        void Start()
        {
            Debug.Log("[Launcher.cs] Connecting to Master");
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("[Launcher.cs] OnConnectedToMaster()");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            UI.MenuManager.Instance.OpenMenu("Title");
            Debug.Log("[Launcher.cs] OnJoinedLobby()");
            PhotonNetwork.NickName = "Player " + Random.Range(1, 100).ToString("00");
        }

        public void CreateRoom()
        {
            if (string.IsNullOrWhiteSpace(roomNameInputField.text))
                return; // Must enter name of room
            PhotonNetwork.CreateRoom(roomNameInputField.text);
            UI.MenuManager.Instance.OpenMenu("Loading");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("[Launcher.cs] OnJoinedRoom()");
            UI.MenuManager.Instance.OpenMenu("Room");
            roomNameText.text = PhotonNetwork.CurrentRoom.Name;

            // Clear list
            foreach (Transform child in playerListContent)
                Destroy(child.gameObject);

            // Fill list
            foreach (Player player in PhotonNetwork.PlayerList)
                AddPlayerToPlayerList(player);

            // Host options
            ToggleHostObjects(PhotonNetwork.IsMasterClient);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            ToggleHostObjects(PhotonNetwork.IsMasterClient);
        }

        private void ToggleHostObjects(bool pActive)
        {
            foreach (GameObject o in roomHostObjects)
                o.SetActive(pActive);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError("[Launcher.cs] OnCreateRoomFailed() Return code: " + returnCode + ", Message: " + message);
            errorText.text = "ROOM CREATION FAILED: " + message;
            UI.MenuManager.Instance.OpenMenu("Error");
        }

        public void StartGame()
        {
            PhotonNetwork.LoadLevel(1);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            UI.MenuManager.Instance.OpenMenu("Loading");
        }

        public void JoinRoom(RoomInfo pInfo)
        {
            PhotonNetwork.JoinRoom(pInfo.Name);
            UI.MenuManager.Instance.OpenMenu("Loading");
        }

        public override void OnLeftRoom()
        {
            Debug.Log("[Launcher.cs] OnLeftRoom()");
            UI.MenuManager.Instance.OpenMenu("Title");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            //Debug.Log("[Launcher.cs] OnRoomListUpdate() " + roomList.Count);
            // Clear list
            foreach (Transform child in roomListContent)
                Destroy(child.gameObject);

            // Fill list
            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].RemovedFromList)
                    continue;
                GameObject o = Instantiate(roomListPrefab, roomListContent);
                o.GetComponent<RoomListItem>().Init(roomList[i]);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            AddPlayerToPlayerList(newPlayer);
        }

        public void AddPlayerToPlayerList(Player pPlayer)
        {
            GameObject o = Instantiate(playerListPrefab, playerListContent);
            o.GetComponent<PlayerListItem>().Init(pPlayer);
        }
    }
}