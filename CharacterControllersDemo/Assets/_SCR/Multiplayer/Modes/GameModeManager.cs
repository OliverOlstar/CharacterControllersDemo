using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace OliverLoescher.Multiplayer
{
	public class GameModeManager : MonoBehaviourSingleton<GameModeManager>
	{
		[SerializeField] private SOGameMode gameMode = null;

		private void Awake()
		{
			gameMode.Play();
			PlayerManager.OnPlayerJoined += OnPlayerJoined;
			PlayerManager.OnPlayerLeft += OnPlayerLeft;
			PlayerManager.OnPlayerDied += OnPlayerDied;
		}

        private void OnDestroy()
		{
			gameMode.Stop();
			PlayerManager.OnPlayerJoined -= OnPlayerJoined;
			PlayerManager.OnPlayerLeft -= OnPlayerLeft;
			PlayerManager.OnPlayerDied -= OnPlayerDied;
		}

        private void OnPlayerJoined(PlayerManager newPlayer)
		{
			if (gameMode.IsActive)
			{
				gameMode.OnPlayerJoined(newPlayer);
			}
		}

		private void OnPlayerLeft(PlayerManager player)
		{
			if (gameMode.IsActive)
			{
				gameMode.OnPlayerLeft(player);
			}
		}

		private void OnPlayerDied(PlayerManager deadPlayer)
		{
			if (gameMode.IsActive)
			{
				gameMode.OnPlayerDied(deadPlayer);
			}
		}

		public void TogglePaused()
        {
			if (gameMode.IsPaused)
            {
				gameMode.Resume();
            }
			else
            {
				gameMode.Pause();
            }
        }
	}
}
