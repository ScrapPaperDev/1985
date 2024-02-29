using UnityEngine;
using System;
using Humble1985;
using Disparity.Unity;
using Disparity;

namespace Unity1985{
public class PlayerController : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Bullet bullet;
	[SerializeField] private float shootThresh;
	[SerializeField] private float speed;
	[SerializeField] private float panelSize;

	private PlayerShooter shooter;
	private IMover movement;



	private void Awake()
	{
		shooter = new Humble1985.PlayerShooter();

		shooter.shootThresh = shootThresh;
		shooter.input = new UnityInputProvider();
		shooter.timerProvider = new UnityTimeProvider();
		shooter.transform = new UnityTransformProvider(player);
		shooter.bulletInstantiater = new UnityInstantiater<Bullet>(bullet);
		shooter.Bind(shootThresh);

		movement = new PlayerMover(shooter.transform, shooter.timerProvider, shooter.input, speed, panelSize);
		Game.player = new UnityTransformProvider(transform);
	}

	private void OnDestroy()
	{
		Game.player = null;
	}

	private void Update()
	{
		movement.Move();
		shooter.Shoot();
	}
}

}