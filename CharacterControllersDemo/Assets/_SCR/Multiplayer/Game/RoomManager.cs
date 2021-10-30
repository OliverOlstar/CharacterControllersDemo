using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

namespace OliverLoescher.Multiplayer
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
    #region Singleton
            public static RoomManager Instance = null;
            private void Awake() 
            {
                if (Instance == null)
                {
                    DontDestroyOnLoad(gameObject);
                    Instance = this;
                }
                else if (Instance != this)
                {
                    Debug.LogError("[RoomManager.cs] Instance != null, destroying self", gameObject);
                    Destroy(this);
                }
            }
    #endregion    

        public override void OnEnable() 
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene pScene, LoadSceneMode pMode)
        {
            if (pScene.buildIndex == 1) // Game scene
            {
                // if (PhotonNetwork.IsConnected == false)
                // {
                //     SceneManager.LoadScene(0);
                //     return;
                // }

                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            }
        }
    }
}