using Disparity;

namespace Humble1985{
public class Shooter : EnemyBehaviour
{
	protected IInstantiatable buller;
	protected ITransformProvider t;
	protected IRandomProvider<int> rand;

	private int shootChance;
	private int timeBetweenShots;

	private int frames;

	public Shooter(IInstantiatable i, ITransformProvider t, IRandomProvider<int> r)
	{
		buller = i;
		this.t = t;
		rand = r;
		timeBetweenShots = 30;
		shootChance = 30;
	}

	public override void Behave()
	{
		if(frames % timeBetweenShots == 0)
		{
			if(rand.RandomRange(0, 100) < shootChance)
				buller.Instantiate(t.pos);
		}

		frames++;
		frames = frames % 100;
	}
}
}