using Disparity;
using Unity1985;

namespace Humble1985{
public class PlayerMover
{
	[Data] public float speed;
	[Data] public float panelSize;

	public ITransformProvider player;
	public ITimeProvider time;
	public IVector3Provider velo;
	public IInputProvider input;

	public void Movement()
	{
		velo.x = input.x * time.deltaTime * speed;
		velo.y = input.y * time.deltaTime * speed;

		if(player.pos.x < GameGlobals.left + player.HalfWidth() && velo.x < 0)
			velo.Set2(0, velo.y);

		if(player.pos.x > GameGlobals.right - player.HalfWidth() && velo.x > 0)
			velo.Set2(0, velo.y);

		if(player.pos.y < GameGlobals.down + panelSize && velo.y < 0)
			velo.Set2(velo.x, 0);
		if(player.pos.y > GameGlobals.up && velo.y > 0)
			velo.Set2(velo.x, 0);
		if(velo.y < 0)
			velo.Set2(velo.x, velo.y / 2.0f);

		player.pos.AddTo(velo);
	}
}
}
