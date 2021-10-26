using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
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

        [SerializeField] private Button[] titleButtons = new Button[0];
        [SerializeField] private TMP_InputField niknameInputField = null;
        [SerializeField] private Button roomNameButton = null;
        [SerializeField] private TMP_InputField roomNameInputField = null;
        [SerializeField] private TMP_Text errorText = null;
        [SerializeField] private TMP_Text roomNameText = null;
        [SerializeField] private Transform roomListContent = null;
        [SerializeField] private GameObject roomListPrefab = null;
        [SerializeField] private Transform playerListContent = null;
        [SerializeField] private GameObject playerListPrefab = null;
        [SerializeField] private GameObject[] roomHostObjects = new GameObject[0];

        private List<RoomListItem> roomListItems = new List<RoomListItem>();

        void Start()
        {
            // Clear list
            foreach (Transform child in roomListContent)
                Destroy(child.gameObject);

            Debug.Log("[Launcher.cs] Connecting to Master");
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("[Launcher.cs] OnConnectedToMaster()");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;

            if (string.IsNullOrWhiteSpace(niknameInputField.text))
            {
                string name = PlayerPrefs.GetString("Nikname", "");
                niknameInputField.text = name;
                UpdateNikname(name);
            }
        }

        public override void OnJoinedLobby()
        {
            UI.MenuManager.Instance.OpenMenu("Title");
            Debug.Log("[Launcher.cs] OnJoinedLobby()");
        }

        public void UpdateNikname(string pName)
        {
            if (string.IsNullOrWhiteSpace(pName))
            {
                foreach (Button b in titleButtons)
                    b.interactable = false;                
            }
            else
            {
                foreach (Button b in titleButtons)
                    b.interactable = true;   

                if (roomNameInputField.text == "" || roomNameInputField.text == PhotonNetwork.NickName + "'s Room")
                    roomNameInputField.text = pName + "'s Room";

                PhotonNetwork.NickName = pName;
                PlayerPrefs.SetString("Nikname", pName);
            }
        }

        public void UpdateRoomName(string pName)
        {
            if (string.IsNullOrWhiteSpace(pName))
            {
                // Name IsNullOrWhiteSpace
                roomNameButton.interactable = false;
            }
            else
            {
                // Name is already in use
                foreach (RoomListItem room in roomListItems)
                {
                    if (room.GetInfo().Name == pName)
                    {
                        roomNameButton.interactable = false;
                        return;
                    }
                }

                // Name is good!!!
                roomNameButton.interactable = true;
            }
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
            Debug.Log("[Launcher.cs] OnRoomListUpdate() " + roomList.Count);
            foreach (RoomInfo info in roomList)
                Debug.Log(info.Name + " - " + info.RemovedFromList);

            // Fill list
            for (int i = 0; i < roomList.Count; i++)
            {
                RoomListItem currRoom = null;
                foreach (RoomListItem room in roomListItems)
                {
                    if (roomList[i].Name == room.GetInfo().Name)
                    {
                        currRoom = room;
                        break;
                    }
                }

                if (roomList[i].RemovedFromList)
                {
                    // Remove from local list
                    if (currRoom != null) // If in list
                    {
                        roomListItems.Remove(currRoom);
                        Destroy(currRoom.gameObject);
                    }
                }
                else
                {
                    // Else add it to the list
                    if (currRoom == null) // If not already
                    {
                        RoomListItem item = Instantiate(roomListPrefab, roomListContent).GetComponent<RoomListItem>();
                        item.Init(roomList[i]);
                        roomListItems.Add(item);
                    }
                }
            }

            UpdateRoomName(roomNameInputField.text);
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

        public void QuitGame()
        {
            PhotonNetwork.Disconnect();
            Application.Quit();
        }
    }
}