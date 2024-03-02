using Disparity;

namespace Humble1985
{
	public class PlayerMover : IMover
	{
		[Data] public float speed;
		[Data] public float panelSize;

		public ITransformProvider player;
		public ITimeProvider time;
		public IInputProvider input;
		public FakeVector3 currentVelo;

		public PlayerMover(ITransformProvider t, ITimeProvider tt, IInputProvider i, float speed, float pan)
		{
			player = t;
			time = tt;
			input = i;
			this.speed = speed;
			panelSize = pan;
		}

		public void Move()
		{
			currentVelo.x = input.x * time.deltaTime * speed;
			currentVelo.y = input.y * time.deltaTime * speed;

			if (player.pos.x < Game.left + player.HalfWidth() && currentVelo.x < 0)
				currentVelo = new FakeVector3(0, currentVelo.y);

			if (player.pos.x > Game.right - player.HalfWidth() && currentVelo.x > 0)
				currentVelo = new FakeVector3(0, currentVelo.y);

			if (player.pos.y < Game.down + panelSize && currentVelo.y < 0)
				currentVelo = new FakeVector3(currentVelo.x, 0);
			if (player.pos.y > Game.up && currentVelo.y > 0)
				currentVelo = new FakeVector3(currentVelo.x, 0);
			if (currentVelo.y < 0)
				currentVelo = new FakeVector3(currentVelo.x, currentVelo.y / 2.0f);

			FakeVector3 finalVelo = currentVelo + player.pos;
			player.pos = finalVelo;
		}
	}
}
