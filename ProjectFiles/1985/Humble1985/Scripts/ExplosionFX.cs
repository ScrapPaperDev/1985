using Disparity;
using System;

namespace Humble1985{

public class ExplosionFX
{
	public ExplosionFX(bool isPlayerExp, IDestroyer dest, Flipbook flip)
	{
		Action act = dest.Destroy;

		if(isPlayerExp)
			act += Game.playerData.LoseALife;

		flip.OnDone += act;
	}
}

}