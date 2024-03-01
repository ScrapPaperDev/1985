using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

namespace Disparity.Unity{

public class AudioService : MonoBehaviour,ISoundPlayer,IUnityService<AudioService>
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

	private AudioSource source;

	private void Awake()
	{
		new AudioPlayer(this);
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


}