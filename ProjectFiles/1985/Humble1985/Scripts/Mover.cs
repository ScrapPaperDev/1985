using Disparity;

namespace Humble1985{

public interface IOffscreenable
{
	void CheckOffscreen();
}

public interface IMover
{
	void Move();
}

public class Mover : IMover
{
	private float speed;
	private ITransformProvider transform;
	private ITimeProvider time;
	private FakeVector3 vec;

	public Mover(float s, ITransformProvider t, ITimeProvider tim)
	{
		speed = s;
		transform = t;
		time = tim;
		vec.AddTo(t.pos);
	}

	public void Move()
	{
		//-- Make sure to update the vec with the engines latest transform.
		vec.Set2(transform.pos.x, transform.pos.y);

		vec.AddTo(new FakeVector3(0, speed * time.deltaTime));
		transform.pos = vec;
	}
}

public class OffscreenDestroyable :IOffscreenable
{
	private IDestroyer destroyable;
	private ITransformProvider t;

	public OffscreenDestroyable(IDestroyer destroyer, ITransformProvider t)
	{
		destroyable = destroyer;
		this.t = t;
	}

	public void CheckOffscreen()
	{
		if(t.pos.y > Game.up)
			destroyable.Destroy();
	}
}

public class RespawnOffscreen :Humble1985.IOffscreenable
{
	private ITransformProvider transform;

	//-- As in when this object is beyond this side, do something.
	private Sides side;

	private RandomRespawner respawner;

	public RespawnOffscreen(ITransformProvider t, IRandomProvider<float> ran, float offset, Sides side)
	{
		transform = t;
		this.side = side;
		respawner = new RandomRespawner(t, ran, offset, side);
	}

	public void CheckOffscreen()
	{
		bool offscreen = false;

		switch(side)
		{
			case Sides.Up:
				offscreen = transform.pos.y > Game.up + transform.HalfWidth();
				break;
			case Sides.Down:
				offscreen = transform.pos.y < Game.down - transform.HalfWidth();
				break;
		}

		if(offscreen)
			respawner.Respawn();

	}
}

public class RandomRespawner
{
	private ITransformProvider transform;
	private IRandomProvider<float> rand;

	private float offset;

	//-- As in when this object is beyond this side, do something.
	private Sides side;

	public RandomRespawner(ITransformProvider t, IRandomProvider<float> ran, float offset, Sides side)
	{
		transform = t;
		rand = ran;
		this.offset = offset;
		this.side = side;
	}

	public void Respawn()
	{
		float newSpawn = 0.0f;

		switch(side)
		{
			case Sides.Up:
				newSpawn = Game.down - transform.HalfWidth() - offset;
				break;
			case Sides.Down:
				newSpawn = Game.up + transform.HalfWidth() + offset;
				break;
		}
		transform.pos = new FakeVector3(rand.RandomRange(Game.left + transform.HalfWidth(), Game.right - transform.HalfWidth()), newSpawn);			
	}
}

public enum Sides
{
	Up,
	Down,
	Left,
	Right
}


}