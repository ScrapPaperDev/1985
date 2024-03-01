using Disparity;

namespace Humble1985{

public class EnemyWaveData
{
	public EnemyWaveData(IInstantiatable en, float d, float l)
	{
		enemy = en;
		delay = d;
		looptime = l;
	}
	public IInstantiatable enemy;
	public float delay;
	public float looptime;
}
}