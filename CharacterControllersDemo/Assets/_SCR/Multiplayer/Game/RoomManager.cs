using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

namespace OliverLoescher.Multiplayer
{
	[RequireComponent(typeof(PhotonView))]
	public class RoomManager : MonoBehaviourPunCallbacks
	{
		public static RoomManager Instance = null;

		private void Awake()
		{
			#region Singleton
			if (Instance != null)
			{
				Debug.LogError("[RoomManager.cs] Instance != null, destroying other", gameObject);
				Destroy(Instance);
			}

			//DontDestroyOnLoad(gameObject);
			Instance = this;
			#endregion
		}

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
				if (PhotonNetwork.IsConnected == false)
				{
					SceneManager.LoadScene(0);
					return;
				}

				PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
			}
			else if (pScene.buildIndex == 0) // Menu
			{
				if (Instance == this)
					Instance = null;
				Destroy(gameObject);
			}
		}

		public void ExitGame()
        {
			PhotonNetwork.LeaveRoom();
			SceneManager.LoadScene(0);
        }
	}
}