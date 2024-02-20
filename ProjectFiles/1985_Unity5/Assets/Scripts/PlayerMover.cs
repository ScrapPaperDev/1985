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
	public FakeVector3 currentVelo;

	public void Movement()
	{
		
		currentVelo.x = input.x * time.deltaTime * speed;
		currentVelo.y = input.y * time.deltaTime * speed;

		if(player.pos.x < GameGlobals.left + player.HalfWidth() && currentVelo.x < 0)
			currentVelo.Set2(0, velo.y);

		if(player.pos.x > GameGlobals.right - player.HalfWidth() && currentVelo.x > 0)
			currentVelo.Set2(0, velo.y);

		if(player.pos.y < GameGlobals.down + panelSize && currentVelo.y < 0)
			currentVelo.Set2(currentVelo.x, 0);
		if(player.pos.y > GameGlobals.up && currentVelo.y > 0)
			currentVelo.Set2(currentVelo.x, 0);
		if(currentVelo.y < 0)
			currentVelo.Set2(currentVelo.x, currentVelo.y / 2.0f);

		var finalVelo = currentVelo.AddTo(player.pos);
		UnityEngine.Debug.Log(finalVelo.x);
		player.pos = finalVelo;
	}
}
}
