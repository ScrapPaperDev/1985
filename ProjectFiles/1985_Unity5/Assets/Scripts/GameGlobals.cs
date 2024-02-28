using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

namespace Unity1985{

public class GameGlobals :MonoBehaviour
{
	private static GameGlobals instance;

	//	[SerializeField] private float _xBounds;
	//	[SerializeField] private float _yBounds;

	[SerializeField] private AudioSource source;

	[SerializeField] private GameObject enemy;
	[SerializeField] private GameObject enemy2;
	[SerializeField] private GameObject enemy3;
	[SerializeField] private GameObject enemy4;

	[SerializeField] private float instOffset;


	public static float up{ get; private set; }
	public static float down{ get; private set; }
	public static float left{ get; private set; }
	public static float right{ get; private set; }


	public static bool modern;


	public static int score;
	public static int lives;
	public static int health;

	[SerializeField] private SpriteRenderer healthBar;

	[SerializeField] private TextMesh scoreText;

	[SerializeField] private SpriteRenderer[] liveIcons;

	public static Transform player;

	public static bool SetAndCheckHealth(int amount)
	{
		health -= amount;
		float i = (float)health / 100.0f;
		instance.healthBar.transform.parent.localScale = new Vector3(i, instance.healthBar.transform.parent.localScale.y, 1);
		instance.healthBar.color = Color.Lerp(Color.red, Color.yellow, i);

		if(health < 0)
			health = 0;

		return health == 0;
	}

	public static void SetScore(int s)
	{
		score += s;
		instance.scoreText.text = score.ToString();
	}

	public GameObject playerPrefab;

	public GameObject highScoreTable;

	private static int[] scores;
	private static string[] names;

	public InputField[] scoreFields;

	public static void LoseALife()
	{
		lives--;

		for(int i = 2; i >= lives; i--)
			instance.liveIcons[i].enabled = false;

		SetAndCheckHealth(-100);


		if(lives == 0)
		{
			//GameGlobals.ShowHighscoreTable();
			instance.highScoreTable.SetActive(true);

			scores[10] = score;

			Array.Sort(scores, ((x, y) => y - x));


			int scoreIndex = 0;

			for(int i = 0; i < 10; i++)
				instance.scoreFields[i].interactable = false;			

			for(int i = 0; i < 10; i++)
			{
				if(score >= scores[i])
				{
					instance.scoreFields[i].interactable = true;
					scoreIndex = i;
					break;
				}
			}

			for(int i = 0; i < 10; i++)
				instance.scoreFields[i].text = scores[i].ToString();


			instance.StartCoroutine(instance.HighScoreTable());
		}
		else
		{
			Instantiate(instance.playerPrefab, Vector3.zero, Quaternion.identity);
		}

	}

	private IEnumerator HighScoreTable()
	{
		while(highScoreTable.activeInHierarchy)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
				highScoreTable.SetActive(false);
			yield return null;
		}

		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	private void OnApplicationQuit()
	{

	}

	private void Awake()
	{
		Application.targetFrameRate = 60;
		Disparity.Settings.TargetFrameRate = () => Application.targetFrameRate;

		instance = this;
		health = 100;
		lives = 3;
		score = 0;
		instance.highScoreTable.SetActive(false);

		if(scores == null)
			scores = new int[11];

		StartCoroutine(obj_controller_enemy());
		StartCoroutine(obj_controller_enemy2());
		StartCoroutine(obj_controller_enemy3());
		StartCoroutine(obj_controller_enemy4());

		player = FindObjectOfType<PlayerController>().transform;

		Vector3 topLeft = new Vector3(0, Screen.height, 0);

		Vector3 bottomRight = new Vector3(Screen.width, 0, 0);

		Vector3 topWorldPosition = Camera.main.ScreenToWorldPoint(topLeft);
		Vector3 bottomWorldPosition = Camera.main.ScreenToWorldPoint(bottomRight);

		up = topWorldPosition.y;
		down = bottomWorldPosition.y;
		left = topWorldPosition.x;
		right = bottomWorldPosition.x;

		Debug.Log(up);
		Debug.Log(down);
		Debug.Log(left);
		Debug.Log(right);
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private IEnumerator obj_controller_enemy()
	{
		yield return new WaitForSeconds(.5f);
		while(true)
		{
			Instantiate(enemy, new Vector3(Random.Range(left, right), up - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(8.0f);
		}

	}

	private IEnumerator obj_controller_enemy2()
	{
		yield return new WaitForSeconds(12);
		while(true)
		{
			Instantiate(enemy2, new Vector3(Random.Range(left, right), up - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(UnityEngine.Random.Range(12.0f, 16.0f));
		}
	}

	private IEnumerator obj_controller_enemy3()
	{
		yield return new WaitForSeconds(25);
		while(true)
		{
			Instantiate(enemy3, new Vector3(Random.Range(left, right), up - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(UnityEngine.Random.Range(12.0f, 16.0f));
		}
	}

	private IEnumerator obj_controller_enemy4()
	{
		yield return new WaitForSeconds(60);
		while(true)
		{
			Instantiate(enemy4, new Vector3(Random.Range(left, right), down - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(UnityEngine.Random.Range(12.0f, 16.0f));
		}
	}

	public static void PlaySound(AudioClip clip)
	{
//		if(instance.source.isPlaying)
//			instance.source.Stop();
//
//		instance.source.clip = clip;
		instance.source.PlayOneShot(clip);
	}


}

}