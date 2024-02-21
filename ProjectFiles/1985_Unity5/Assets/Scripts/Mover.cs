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
	private IDestroyable destroyable;
	private ITransformProvider t;

	public OffscreenDestroyable(IDestroyable destroyer, ITransformProvider t)
	{
		destroyable = destroyer;
		this.t = t;
	}

	public void CheckOffscreen()
	{
		if(t.pos.y > Unity1985.GameGlobals.up)
			destroyable.Destroy();
	}
}

public class RespawnOffscreen :Humble1985.IOffscreenable
{
	private ITransformProvider transform;
	private IRandomProvider<float> rand;

	private float offset;

	public RespawnOffscreen(ITransformProvider t, IRandomProvider<float> ran, float offset)
	{
		transform = t;
		rand = ran;
		this.offset = offset;
	}

	public void CheckOffscreen()
	{
		if(transform.pos.y < Unity1985.GameGlobals.down - transform.HalfWidth())
			transform.pos = new FakeVector3(rand.RandomRange(Unity1985.GameGlobals.left + transform.HalfWidth(), Unity1985.GameGlobals.right - transform.HalfWidth()), Unity1985.GameGlobals.up + transform.HalfWidth() + offset);
	}
}


}