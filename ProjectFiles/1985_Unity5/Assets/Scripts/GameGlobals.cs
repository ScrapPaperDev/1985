using UnityEngine;
using System.Collections;

namespace _1985{

public class GameGlobals :MonoBehaviour
{
	private static GameGlobals instance;

	[SerializeField] private float _xBounds;
	[SerializeField] private float _yBounds;

	[SerializeField] private AudioSource source;

	[SerializeField] private GameObject enemy;

	[SerializeField] private float instOffset;

	public static float xBounds{ get { return instance._xBounds; } }
	public static float yBounds{ get { return instance._yBounds; } }

	public static int score;

	private void Awake()
	{
		instance = this;
		StartCoroutine(obj_controller_enemy());
	}

	private IEnumerator obj_controller_enemy()
	{
		while(true)
		{
			Instantiate(enemy, new Vector3(Random.Range(-_xBounds, _xBounds), _yBounds - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(8.0f);
		}

	}

	public static void PlaySound(AudioClip clip)
	{
		if(instance.source.isPlaying)
			instance.source.Stop();

		instance.source.clip = clip;
		instance.source.Play();
	}

	public static void ShowHighscoreTable()
	{
		//TODO: do it
	}
}

}