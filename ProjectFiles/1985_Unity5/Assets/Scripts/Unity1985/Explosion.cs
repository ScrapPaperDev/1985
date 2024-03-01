using System;
using System.Collections;
using System.Collections.Generic;
using Disparity;
using Disparity.Unity;
using UnityEngine;
using Humble1985;

namespace Unity1985{
public class Explosion : MonoBehaviour
{
	public bool isPlayerExp;

	private void Awake()
	{
		new ExplosionFX(isPlayerExp, new UnityDestroyer<GameObject>(gameObject), GetComponent<SpriteFlipbook>().flipBook);
	}
}
}