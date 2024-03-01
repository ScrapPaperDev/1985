using UnityEngine;
using System;

namespace Disparity.Unity{

public class UnityRandomInt : IRandomProvider<int>
{
	public int RandomRange(int min, int max)
	{
		return UnityEngine.Random.Range(min, max);
	}
}

}
