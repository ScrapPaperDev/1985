using Disparity;
using Unity1985;

namespace Humble1985{
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

		if(player.pos.x < GameGlobals.left + player.HalfWidth() && currentVelo.x < 0)
			currentVelo.Set2(0, currentVelo.y);

		if(player.pos.x > GameGlobals.right - player.HalfWidth() && currentVelo.x > 0)
			currentVelo.Set2(0, currentVelo.y);

		if(player.pos.y < GameGlobals.down + panelSize && currentVelo.y < 0)
			currentVelo.Set2(currentVelo.x, 0);
		if(player.pos.y > GameGlobals.up && currentVelo.y > 0)
			currentVelo.Set2(currentVelo.x, 0);
		if(currentVelo.y < 0)
			currentVelo.Set2(currentVelo.x, currentVelo.y / 2.0f);

		var finalVelo = currentVelo.AddTo(player.pos);
		player.pos = finalVelo;
	}
}
}
