using Disparity;

namespace Humble1985{
public class HumbleBullet
{
	private Mover mover;
	private IOffscreenable offscreenHandler;

	public HumbleBullet(ITransformProvider t, ITimeProvider tt, IDestroyable d, float speed)
	{
		mover = new Mover(speed, t, tt);
		offscreenHandler = new OffscreenDestroyable(d, t);
	}

	public void Update()
	{
		mover.Move();
		offscreenHandler.CheckOffscreen();
	}
}
}