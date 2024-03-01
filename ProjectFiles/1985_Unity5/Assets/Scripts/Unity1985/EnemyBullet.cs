using System.Collections;
using System.Collections.Generic;
using Disparity;
using Disparity.Unity;
using UnityEngine;
using Humble1985;

namespace Unity1985{
public class EnemyBullet : MonoBehaviour
{
	[SerializeField] private float speed;

	[SerializeField] private GameObject explodePlayer;
	[SerializeField] private  AudioClip clip2;
	[SerializeField] private bool home;

	private EnemyBuller buller;
	private UnityDestroyer<GameObject> playerDestroyer;


	private void Start()
	{
		playerDestroyer = new UnityDestroyer<GameObject>(null);
		buller = new EnemyBuller(new UnityDestroyer<GameObject>(gameObject), new UnityTransformProvider(transform), speed, home, new UnityInstantiatable<GameObject>(explodePlayer), playerDestroyer, AudioService.instance, new UnitySoundClip(clip2));
	}

	private void Update()
	{
		buller.Move();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			playerDestroyer.obj = other.gameObject;
			buller.HitPlayer();
		}
			
	}
}
}