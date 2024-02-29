using System;
using System.Collections;
using System.Collections.Generic;
using Disparity;
using Disparity.Unity;
using UnityEngine;

namespace Unity1985{
public class Explosion : MonoBehaviour
{
	public bool isPlayerExp;

	private void Awake()
	{
		new ExplosionFX(isPlayerExp, new UnityDestroyer<GameObject>(gameObject), GetComponent<SpriteFlipbook>().flipBook);
	}
}

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