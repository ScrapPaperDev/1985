using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Disparity;
using Disparity.Unity;

namespace Unity1985{

public class GameGlobals :MonoBehaviour
{
	private static GameGlobals instance;


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

	public static Disparity.ITransformProvider player;

	public static bool SetAndCheckHealth(int amount)
	{
		health -= amount;
		float i = Mathf.Clamp((float)health / 100.0f, 0, 100f);

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

	private void Start()
	{
		var en1 = new EnemyWaveData(new UnityInstantiater<GameObject>(enemy), 1, 8);
		var en2 = new EnemyWaveData(new UnityInstantiater<GameObject>(enemy2), 12, 10);
		var en3 = new EnemyWaveData(new UnityInstantiater<GameObject>(enemy3), 24, 12);
		var en4 = new EnemyWaveData(new UnityInstantiater<GameObject>(enemy4), 32, 16);

		new Game(en1, en2, en3, en4, UnityScheduler.instance, new UnityRandom());
	}

	private void OnDestroy()
	{
		instance = null;
	}

}


public class Game
{
	private EnemyWaveData enemy1;
	private EnemyWaveData enemy2;
	private EnemyWaveData enemy3;
	private EnemyWaveData enemy4;
	private float offset;
	private IRandomProvider<float> rand;
	private IScheduler sche;

	private EnemySpawner spawner;


	public Game(EnemyWaveData enemy1, EnemyWaveData enemy2, EnemyWaveData enemy3, EnemyWaveData enemy4, IScheduler scheduler, IRandomProvider<float> rando)
	{
		this.enemy1 = enemy1;
		this.enemy2 = enemy2;
		this.enemy3 = enemy3;
		this.enemy4 = enemy4;
		sche = scheduler;
		rand = rando;
		spawner = new EnemySpawner();
		StartWaves();
	}


	public void StartWaves()
	{
		var wave1 = spawner.SpawnEnemies(enemy1, rand, GameGlobals.left, GameGlobals.right, GameGlobals.up, offset);
		var wave2 = spawner.SpawnEnemies(enemy2, rand, GameGlobals.left, GameGlobals.right, GameGlobals.up, offset);
		var wave3 = spawner.SpawnEnemies(enemy3, rand, GameGlobals.left, GameGlobals.right, GameGlobals.up, offset);
		var wave4 = spawner.SpawnEnemies(enemy4, rand, GameGlobals.left, GameGlobals.right, GameGlobals.up, offset);

		new Disparity.Coroutine(sche, wave1);
		new Disparity.Coroutine(sche, wave2);
		new Disparity.Coroutine(sche, wave3);
		new Disparity.Coroutine(sche, wave4);


	}

}


public class EnemySpawner
{

	public IEnumerator SpawnEnemies(EnemyWaveData enemy, IRandomProvider<float> range, float left, float right, float offscreen, float offset)
	{
		yield return new Disparity.WaitForSeconds(enemy.delay);
		var wait = new Disparity.WaitForSeconds(enemy.looptime);
		while(true)
		{
			enemy.enemy.Instantiate(new FakeVector3(range.RandomRange(left, right), offscreen - offset));
			yield return wait;
		}
	}
}

public class EnemyWaveData
{
	public EnemyWaveData(IInstantiater en, float d, float l)
	{
		enemy = en;
		delay = d;
		looptime = l;
	}
	public IInstantiater enemy;
	public float delay;
	public float looptime;
}

}