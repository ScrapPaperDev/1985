using System;
using Disparity;
using Unity1985;

namespace Humble1985{
public class PlayerShooter : IBindable
{

	private float shootTimer;
	[Data] public float shootThresh;

	public ITimeProvider timerProvider;
	public IInstantiater bulletInstantiater;
	public IInputProvider input;
	public ITransformProvider transform;

	public void Bind(params object[] o)
	{
		shootThresh = (float)o[0];

	}

	public void Shoot()
	{
		shootTimer += timerProvider.deltaTime;
		if(input.shoot)
		{
			if(shootTimer > shootThresh)
			{
				if(Unity1985.Game.playerData.score > 999)
					ShootTripple();
				else if(Unity1985.Game.playerData.score > 399)
					ShootDouble();
				else
					ShootSingle();

				shootTimer = 0;
			}
		}
	}

	private void ShootSingle()
	{
		bulletInstantiater.Instantiate(transform.pos);
	}

	private void ShootTripple()
	{
		bulletInstantiater.Instantiate(transform.pos.AddTo(new FakeVector3(0, .25f)));
		bulletInstantiater.Instantiate(transform.pos.AddTo(new FakeVector3(.5f, 0)));
		bulletInstantiater.Instantiate(transform.pos.AddTo(new FakeVector3(-.5f, 0)));
	}

	private void ShootDouble()
	{
		bulletInstantiater.Instantiate(transform.pos.AddTo(new FakeVector3(.5f, 0)));
		bulletInstantiater.Instantiate(transform.pos.AddTo(new FakeVector3(-.5f, 0)));
	}
}
}
