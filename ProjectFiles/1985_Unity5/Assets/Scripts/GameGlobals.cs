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
	[SerializeField] private GameObject enemy;
	[SerializeField] private GameObject enemy2;
	[SerializeField] private GameObject enemy3;
	[SerializeField] private GameObject enemy4;

	public GameObject playerPrefab;

	private void Awake()
	{
		Application.targetFrameRate = 60;
		Disparity.Settings.TargetFrameRate = () => Application.targetFrameRate;
	}

	private void Start()
	{
		var en1 = new EnemyWaveData(new UnityInstantiatable<GameObject>(enemy), 1, 8);
		var en2 = new EnemyWaveData(new UnityInstantiatable<GameObject>(enemy2), 12, 10);
		var en3 = new EnemyWaveData(new UnityInstantiatable<GameObject>(enemy3), 24, 12);
		var en4 = new EnemyWaveData(new UnityInstantiatable<GameObject>(enemy4), 32, 16);

		new Game(en1, en2, en3, en4, UnityScheduler.instance, new UnityRandom(), new UnityInstantiatable<GameObject>(playerPrefab), GetComponent<IPresentable>(), new UnityCamWorldBoundaryProvider());

	}
}

public interface IWorldBoundaryProvider
{
	FakeVector3 topLeft{ get; }
	FakeVector3 bottomRight{ get; }
	FakeVector3 topWorldPosition{ get; }
	FakeVector3 bottomWorldPosition{ get; }
}

public class UnityCamWorldBoundaryProvider : IWorldBoundaryProvider
{
	public FakeVector3 topLeft	{ get { return new FakeVector3(0, Screen.height); } }
	public FakeVector3 bottomRight	{ get { return new FakeVector3(Screen.width, 0); } }
	public FakeVector3 topWorldPosition	{ get { return Camera.main.ScreenToWorldPoint(topLeft.ToUnityV3()).ToFakeVector3(); } }
	public FakeVector3 bottomWorldPosition	{ get { return Camera.main.ScreenToWorldPoint(bottomRight.ToUnityV3()).ToFakeVector3(); } }
}





public class PlayerDataModel
{
	public int score{ get; private set; }
	public int lives{ get; private set; }
	public int health{ get; private set; }

	public event Action<int> OnHealthChanged = delegate {};
	public event Action<int> OnScoreChanged = delegate {};
	public event Action<int> OnLivesChanged = delegate {};

	public PlayerDataModel()
	{
		health = 100;
		lives = 3;
		score = 0;
	}

	public bool IsDead()
	{		
		return health == 0;
	}

	public void SetHealth(int amount)
	{
		health -= amount;

		if(health < 0)
			health = 0;		

		OnHealthChanged(health);
	}

	public void SetScore(int s)
	{
		score += s;
		OnScoreChanged(score);
	}

	public void LoseALife()
	{
		lives--;
		SetHealth(-100);
		OnLivesChanged(lives);
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
	private IInstantiatable playerInst;

	private EnemySpawner spawner;
	public static PlayerDataModel playerData;

	public static float up{ get; private set; }
	public static float down{ get; private set; }
	public static float left{ get; private set; }
	public static float right{ get; private set; }

	public static Disparity.ITransformProvider player;

	public static bool modern;

	private UIPresenter presenter;


	public Game(EnemyWaveData enemy1, EnemyWaveData enemy2, EnemyWaveData enemy3, EnemyWaveData enemy4, IScheduler scheduler, IRandomProvider<float> rando, IInstantiatable pla, IPresentable presentable, IWorldBoundaryProvider world)
	{
		SetWorldBoundaries(world);
		this.enemy1 = enemy1;
		this.enemy2 = enemy2;
		this.enemy3 = enemy3;
		this.enemy4 = enemy4;
		sche = scheduler;
		rand = rando;
		spawner = new EnemySpawner();
		StartWaves();

		playerInst = pla;

		playerData = new PlayerDataModel();

		playerData.OnLivesChanged += RespawnPlayer_OnLivesChanged;

		presenter = new UIPresenter(playerData, presentable);
	}

	private void SetWorldBoundaries(IWorldBoundaryProvider world)
	{		
		up = world.topWorldPosition.y;
		down = world.bottomWorldPosition.y;
		left = world.topWorldPosition.x;
		right = world.bottomWorldPosition.x;
		Debug.Log(up);
		Debug.Log(down);
		Debug.Log(left);
		Debug.Log(right);
	}

	private void RespawnPlayer_OnLivesChanged(int obj)
	{
		if(obj > 0)
		{
			playerInst.Instantiate(new FakeVector3(0, 0));
		}

	}


	public void StartWaves()
	{
		var wave1 = spawner.SpawnEnemies(enemy1, rand, left, right, up, offset);
		var wave2 = spawner.SpawnEnemies(enemy2, rand, left, right, up, offset);
		var wave3 = spawner.SpawnEnemies(enemy3, rand, left, right, up, offset);
		var wave4 = spawner.SpawnEnemies(enemy4, rand, left, right, up, offset);

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
	public EnemyWaveData(IInstantiatable en, float d, float l)
	{
		enemy = en;
		delay = d;
		looptime = l;
	}
	public IInstantiatable enemy;
	public float delay;
	public float looptime;
}

}