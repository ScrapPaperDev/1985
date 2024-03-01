using System.Collections;
using Disparity;

namespace Humble1985{
public class EnemySpawner
{
	public IEnumerator SpawnEnemies(EnemyWaveData enemy, IRandomProvider<float> range, float left, float right, float offscreen, float offset)
	{
		yield return new Disparity.WaitForSeconds(enemy.delay);
		var wait = new Disparity.WaitForSeconds(enemy.looptime);
		while(true)
		{
			enemy.enemy.Instantiate(new FakeVector3(range.RandomRange(left, right), offscreen - offset));
			yield return wait;
		}
	}
}
}