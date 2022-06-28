using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OliverLoescher.Multiplayer
{
	public abstract class SOGameMode : ScriptableObject
	{
		private enum State
		{
			InActive,
			Active,
			Paused
		}

		private State state = State.InActive;

		public virtual void Play(List<PlayerManager> players) { state = State.Active; }
		public virtual void Stop() { state = State.InActive; }
		public virtual void Pause() { state = State.Paused; }

		public virtual void OnPlayerJoined(PlayerManager player) { }
		public virtual void OnPlayerLeft(PlayerManager player) { }
		public virtual void OnPlayerDied(PlayerManager player) { }

		public bool IsActive => state == State.Active;
		public bool IsInActive => state == State.InActive;
		public bool IsPaused => state == State.Paused;
	}
}