using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OliverLoescher.Multiplayer
{
	public abstract class SOGameMode : ScriptableObject
	{
		private enum State
		{
			None = 0,
			Playing = 1,
			Paused = 2
		}

		private State state = State.None;

		// Inputs
		public void Play() => SwitchState(State.Paused, State.None, PlayInternal);
		public void Stop() => SwitchState(State.Paused, State.Playing | State.Paused, StopInternal);
		public void Pause() => SwitchState(State.Paused, State.Playing, PauseInternal);
		public void Resume() => SwitchState(State.Playing, State.Paused, ResumeInternal);
		private void SwitchState(State pTo, State pRequired, Action pSuccessAction)
		{
			if ((state & pRequired) != 0)
			{
				state = pTo;
				pSuccessAction?.Invoke();
			}
		}

		protected abstract void PlayInternal();
		protected abstract void StopInternal();
		protected abstract void PauseInternal();
		protected abstract void ResumeInternal();

		// Events
		public abstract void OnPlayerJoined(PlayerManager pPlayer);
		public abstract void OnPlayerLeft(PlayerManager pPlayer);
		public abstract void OnPlayerDied(PlayerManager pPlayer);

		// Util
		public bool IsPlaying => state == State.Playing;
		public bool IsActive => state != State.None;
		public bool IsPaused => state == State.Paused;
	}
}