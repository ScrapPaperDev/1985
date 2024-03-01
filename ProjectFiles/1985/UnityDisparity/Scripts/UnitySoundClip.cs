using UnityEngine;

namespace Disparity.Unity
{

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

}
