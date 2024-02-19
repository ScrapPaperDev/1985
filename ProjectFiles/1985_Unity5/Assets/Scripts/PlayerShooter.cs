using System;
using Disparity;
using Unity1985;

namespace Humble1985{
public class PlayerShooter
{

	private float shootTimer;
	[Data] public float shootThresh;

	public ITimeProvider timerProvider;
	private IInstantiater bulletInstantiater;
	private IGameObject<Unity1985.Bullet> bulletPrototype;
	public IInputProvider input;

	public Action shootSingle;
	public Action shootDouble;
	public Action shootTripple;

	public void Shoot()
	{
		shootTimer += timerProvider.deltaTime;
		if(input.shoot)
		{
			if(shootTimer > shootThresh)
			{
				if(Unity1985.GameGlobals.score > 999)
					//bulletInstantiater.Instantiate(bulletPrototype, transform.pos);
					shootTripple();
				else if(Unity1985.GameGlobals.score > 399)
					shootDouble();
				else
					shootSingle();

				shootTimer = 0;
			}
		}
	}

	private void ShootSingle()
	{
		//bulletInstantiater.Instantiate(bulletPrototype,);
	}
}
}
