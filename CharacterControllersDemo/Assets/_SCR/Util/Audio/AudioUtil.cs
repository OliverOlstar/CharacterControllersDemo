﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OliverLoescher
{
	// Static Util class for general playing audio functions
	public static class AudioUtil
	{
		public static AudioClip GetRandomClip(in AudioClip[] clips)
		{
			if (clips.Length == 0)
			{
				return null;
			}

			return clips[Random.Range(0, clips.Length)];
		}

		public static void PlayOneShotRandomClip(in AudioSource source, in AudioClip[] clips)
		{
			if (source == null)
			{
				Debug.LogWarning("PlayOneShotRandomClip() was given a null AudioSource");
				return;
			}

			AudioClip clip = GetRandomClip(clips);
			if (clip == null)
			{
				Debug.LogWarning("GetRandomClip() returned a null reference - " + source.gameObject.name);
				return;
			}

			source.clip = clip;
			source.Play();
		}

		// Overload to randomize pitch
		public static void PlayOneShotRandomClip(in AudioSource source, in AudioClip[] clips, in float pitchMin, in float pitchMax)
		{
			source.pitch = Random.Range(pitchMin, pitchMax);
			PlayOneShotRandomClip(source, clips);
		}

		public static void PlayOneShotRandomClip(in AudioSource source, in AudioClip[] clips, in Vector2 pitch)
		{
			source.pitch = RandUtil.Range(pitch);
			PlayOneShotRandomClip(source, clips);
		}

		public static void PlayOneShotRandomClip(in AudioSource source, in AudioClip[] clips, in float pitchMin, in float pitchMax, in float volume)
		{
			source.volume = volume;
			source.pitch = Random.Range(pitchMin, pitchMax);
			PlayOneShotRandomClip(source, clips);
		}

		public static void PlayOneShotRandomClip(in AudioSource source, in AudioClip[] clips, in Vector2 pitch, in float volume)
		{
			source.volume = volume;
			source.pitch = RandUtil.Range(pitch);
			PlayOneShotRandomClip(source, clips);
		}
		

		[System.Serializable]
		public class AudioPiece
		{
			public AudioClip[] clips = new AudioClip[0];
			[Range(0, 1)] public float volume = 1.0f;
			[MinMaxSlider(0, 3, true)] public Vector2 pitch = new Vector2(0.9f, 1.2f);

			public void Play(in AudioSource source) => PlayOneShotRandomClip(source, clips, pitch, volume);
			public void Play(in AudioSourcePool source) => PlayOneShotRandomClip(source.GetSource(), clips, pitch, volume);
		}
	}
}