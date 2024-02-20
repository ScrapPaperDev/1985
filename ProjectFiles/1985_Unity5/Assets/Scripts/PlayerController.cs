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
		shooter.timerProvider = new UnityTimeProvider();
		shooter.transform = new UnityTransformProvider(player);
		shooter.bulletInstantiater = new UnityInstantiater<Bullet>(bullet);
		shooter.Bind(shootThresh);


		movement = new PlayerMover();

		movement.input = shooter.input;
		movement.time = shooter.timerProvider;
		movement.player = shooter.transform;
		movement.velo = new UnityVector3();
		movement.speed = speed;
		movement.panelSize = panelSize;

	}

	private void Update()
	{
		movement.Movement();
		shooter.Shoot();
	}
}
}