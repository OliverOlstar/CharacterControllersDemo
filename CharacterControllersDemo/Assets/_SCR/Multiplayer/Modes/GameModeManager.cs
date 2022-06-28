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
			gameMode.Play(PlayerManager.Players);
			PlayerManager.OnPlayerJoined += OnPlayerJoined;
			PlayerManager.OnPlayerLeft += OnPlayerLeft;
			PlayerManager.OnPlayerDied += OnPlayerDied;
		}

		private void OnPlayerJoined(PlayerManager newPlayer)
		{
			if (!gameMode.IsInActive)
			{
				gameMode.OnPlayerJoined(newPlayer);
			}
		}

		private void OnPlayerLeft(PlayerManager player)
		{
			if (!gameMode.IsInActive)
			{
				gameMode.OnPlayerLeft(player);
			}
		}

		private void OnPlayerDied(PlayerManager deadPlayer)
		{
			if (!gameMode.IsInActive)
			{
				gameMode.OnPlayerDied(deadPlayer);
			}
		}
	}
}
