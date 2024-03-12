using Disparity;

namespace Humble1985
{
	public class Game
	{
		internal static Game game;
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

		public static float up { get; private set; }
		public static float down { get; private set; }
		public static float left { get; private set; }
		public static float right { get; private set; }

		public static ITransformProvider player;

		public static bool modern;

		public Game(EnemyWaveData enemy1, EnemyWaveData enemy2, EnemyWaveData enemy3, EnemyWaveData enemy4, IScheduler scheduler, IRandomProvider<float> rando, IInstantiatable pla, IPresentable presentable, IWorldBoundaryProvider world, float offset)
		{
			game = this;
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

			new UIPresenter(playerData, presentable);

			this.offset = offset;
		}

		private void SetWorldBoundaries(IWorldBoundaryProvider world)
		{
			up = world.topWorldPosition.y;
			down = world.bottomWorldPosition.y;
			left = world.topWorldPosition.x;
			right = world.bottomWorldPosition.x;
			if (!modern)
			{
				left += 2;
				right -= 2;
			}
		}

		private void RespawnPlayer_OnLivesChanged(int obj)
		{
			if (obj > 0)
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


		public void UpdateEnemiesForGameMode(IInstantiatable e1, IInstantiatable e2, IInstantiatable e3, IInstantiatable e4)
		{
			enemy1.enemy = e1;
			enemy2.enemy = e2;
			enemy3.enemy = e3;
			enemy4.enemy = e4;
		}



	}

}