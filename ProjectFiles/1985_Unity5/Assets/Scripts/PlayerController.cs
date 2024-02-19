using UnityEngine;
using System;
using Humble1985;
using Disparity.Unity;

namespace Unity1985{
public class PlayerController : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Bullet bullet;
	[SerializeField] private float shootThresh;
	[SerializeField] private float speed;
	[SerializeField] private float panelSize;

	private PlayerShooter shooter;
	private PlayerMover movement;

	private void Awake()
	{
		shooter = new Humble1985.PlayerShooter();

		shooter.shootThresh = shootThresh;
		shooter.input = new UnityInputProvider();
		shooter.shootSingle = ShootSingle;
		shooter.shootDouble = ShootDouble;
		shooter.shootTripple = ShootTripple;
		shooter.timerProvider = new UnityTimeProvider();


		movement = new PlayerMover();

		movement.input = shooter.input;
		movement.time = shooter.timerProvider;
		movement.player = new UnityTransformProvider(player);
		movement.velo = new UnityVector3();
		movement.speed = speed;
		movement.panelSize = panelSize;
	}

	private void Update()
	{
		movement.Movement();
		shooter.Shoot();
	}


	void ShootTripple()
	{
		Instantiate(bullet, transform.position + new Vector3(0, .25f), Quaternion.identity);
		Instantiate(bullet, transform.position + new Vector3(.5f, 0), Quaternion.identity);
		Instantiate(bullet, transform.position + new Vector3(-.5f, 0), Quaternion.identity);
	}

	void ShootDouble()
	{
		Instantiate(bullet, transform.position + new Vector3(.5f, 0), Quaternion.identity);
		Instantiate(bullet, transform.position + new Vector3(-.5f, 0), Quaternion.identity);
	}

	void ShootSingle()
	{
		Instantiate(bullet, transform.position, Quaternion.identity);
	}
}
}