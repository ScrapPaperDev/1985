using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humble1985;
using Disparity.Unity;

namespace Unity1985{
public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed;
	private HumbleBullet bullet;

	private void Awake()
	{
		bullet = new HumbleBullet(new UnityTransformProvider(transform), new UnityTimeProvider(), new UnityDestroyer<GameObject>(gameObject), speed);
	}

	private void Update()
	{
		bullet.Update();
	}
}
}