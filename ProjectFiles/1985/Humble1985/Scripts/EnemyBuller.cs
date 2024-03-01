using Disparity;

namespace Humble1985
{
	public class EnemyBuller
	{
		public IDestroyer destroyer;
		private FakeVector3 dirToPlayer;
		private FakeVector3 velo;
		private ITransformProvider transform;

		private float speed;
		private bool homing;

		private IInstantiatable explode;
		private IDestroyer playerDestroyer;

		private ISoundPlayer player;
		private ISoundProvider cl;

		private ITimeProvider time;

		public EnemyBuller(IDestroyer d, ITransformProvider t, float speed, bool home, IInstantiatable explode, IDestroyer pd, ISoundPlayer pl, ISoundProvider clip, ITimeProvider time)
		{
			if (Game.player == null)
			{
				d.Destroy();
				return;
			}

			this.time = time;
			player = pl;
			cl = clip;
			destroyer = d;
			homing = home;
			transform = t;
			this.explode = explode;
			playerDestroyer = pd;
			dirToPlayer = new FakeVector3(Game.player.pos.x - transform.pos.x, Game.player.pos.y - transform.pos.y);
			velo = new FakeVector3(transform.pos.x, transform.pos.y);
			this.speed = speed;

		}

		public void Move()
		{
			float actualSpeed = -speed * time.deltaTime;

			if (!homing)
			{
				velo.y += actualSpeed;
				transform.pos = velo;
			}
			else
			{
				velo.AddTo(new FakeVector3(dirToPlayer.x * time.deltaTime, dirToPlayer.y * time.deltaTime));
				transform.pos = velo;
			}

			if (transform.pos.y < Game.down || transform.pos.y > Game.up || transform.pos.x < Game.left || transform.pos.x > Game.right)
				destroyer.Destroy();
		}

		public void HitPlayer()
		{
			Game.playerData.SetHealth(10);

			if (Game.playerData.IsDead())
			{
				playerDestroyer.Destroy();
				explode.Instantiate(transform.pos);
				player.PlaySound(cl);
			}
			destroyer.Destroy();
		}
	}
}