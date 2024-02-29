using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

namespace Disparity.Unity{

public class AudioService : MonoBehaviour,IAudioPlayer,IUnityService<AudioService>
{
	public AudioService Instance
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public static AudioService instance;

	private AudioPlayer player;

	private AudioSource source;

	private void Awake()
	{
		player = new AudioPlayer(this);
		source = GetComponent<AudioSource>();
		instance = this;
		var a = gameObject.AddComponent<AudioSource>();
		a.loop = false;
		a.playOnAwake = false;
		a.spatialBlend = 0;
		source = a;
	}

	public void PlaySound(ISoundProvider clip)
	{
		source.PlayOneShot(clip.GetSound<AudioClip>());
	}
}

public class UnitySoundClip : ISoundProvider
{
	public AudioClip clip;

	public UnitySoundClip(AudioClip c)
	{
		clip = c;
	}


	public T GetSound<T>() where T : class
	{
		return clip as T;
	}
}


public class AudioPlayer
{
	private IAudioPlayer audioPlayer;

	public static AudioPlayer instance;

	public AudioPlayer(IAudioPlayer player)
	{
		if(instance != null)
			return;

		instance = this;
		audioPlayer = player;
	}

	public void PlaySound(ISoundProvider s)
	{
		audioPlayer.PlaySound(s);
	}
}

public interface IAudioPlayer
{
	void PlaySound(ISoundProvider clip);
}

public interface ISoundProvider
{
	T GetSound<T>()where T : class;
}

}