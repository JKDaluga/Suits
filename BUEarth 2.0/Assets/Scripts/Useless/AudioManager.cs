﻿using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
	// Audio sources
	// Music
	[SerializeField] AudioSource musicMainAudioSource;
	List<AudioSource> sfxAudioSources;

	[SerializeField] GameObject sfxAudioSourcePrefab;
	[SerializeField] GameObject sfxSourcesParent;
	[SerializeField] int defaultNumSfxSources;
	[SerializeField] int maxNumSfxSources = 10;


	// Use this for initialization
	void Awake()
	{
		sfxAudioSources = new List<AudioSource>();

		for (int i = 0; i < defaultNumSfxSources; i++)
		{
			InstantiateAudioSource();
		}

	}

	/*
	 * Play Sound methods
	 */

	public static void PlaySound(AudioClip clip, bool bypassEffects, int priority, float volume, float pitch, float stereoPan, bool delay, float delayTime)
	{
		if (clip == null)
		{
			Debug.LogError("PlaySound(): null AudioClip");
		}

		AudioSource source = GetFreeSFXAudioSource();
		source.bypassEffects = bypassEffects;
		source.clip = clip;
		source.priority = priority;
		source.volume = volume;
		source.pitch = pitch;
		source.panStereo = stereoPan;
		if(delay)
		{
			source.PlayDelayed(delayTime);
		}
		else
		{
			source.Play();
		}
	}

	public static void PlaySound(AudioClip clip, int priority, float volume, float pitch, bool delay, float delayTime)
	{
		PlaySound(clip, false, priority, volume, pitch, 0, delay, delayTime);
	}

	public static void PlaySound(AudioClip clip, int priority, float volume, float pitch)
	{
		PlaySound(clip, false, priority, volume, pitch, 0, false, 0);
	}

	public static void PlaySound(AudioClip clip, bool bypassEffects, int priority, float volume, float minPitch, float maxPitch, float stereoPan)
	{
		PlaySound(clip, bypassEffects, priority, volume, Random.Range(minPitch, maxPitch), stereoPan, false, 0);
	}

	public static void PlaySound(AudioClip clip, bool delay, float delayTime)
	{
		PlaySound(clip, false, 128, 1, 1, 0, delay, delayTime);
	}

	public static void PlaySound(AudioClip clip)
	{
		PlaySound(clip, false, 128, 1, 1, 0, false, 0);
	}

	public static void PlaySound(AudioClip clip, int priority, float volume, float minPitch, float maxPitch)
	{
		PlaySound(clip, false, priority, volume, Random.Range(minPitch, maxPitch), 0, false, 0);
	}

	public static void PlaySound(AudioClip clip, float minPitch, float maxPitch)
	{
		PlaySound(clip, false, 128, 1, Random.Range(minPitch, maxPitch), 0, false, 0);
	}

	/*
	 * Helper methods
	 */

	static AudioSource GetFreeSFXAudioSource()
	{
		// Check the current list of sfx audio sources to see if any are free (not playing)
		for (int i = 0; i < inst.sfxAudioSources.Count; i++)
		{
			if (!inst.sfxAudioSources[i].isPlaying)
			{
				return inst.sfxAudioSources[i];
			}
		}

		// If none are free and the max number of sfx audio sources allowed has been reached, return the last one in the index
		// This shouldn't happen but is a precauation against accidently spawning tons of unwanted sources
		if (inst.sfxAudioSources.Count + 1 > inst.maxNumSfxSources)
		{
			Debug.LogError("Exceeded max count for free sfx audio sources");
			return inst.sfxAudioSources[inst.sfxAudioSources.Count - 1];
		}

		// If none are free, instantiate a new audio source
		return InstantiateAudioSource();
	}

	static AudioSource InstantiateAudioSource()
	{
		AudioSource newSource = ((GameObject)Instantiate(inst.sfxAudioSourcePrefab, inst.sfxSourcesParent.transform)).GetComponent<AudioSource>();
		inst.sfxAudioSources.Add(newSource);
		return newSource;
	}
}
