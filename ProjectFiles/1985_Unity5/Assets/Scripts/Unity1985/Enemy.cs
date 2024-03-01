using System.Collections;
using System.Collections.Generic;
using Disparity;
using Disparity.Unity;
using Humble1985;
using UnityEngine;

namespace Unity1985{
public class Enemy : MonoBehaviour
{
	[SerializeField] private AudioClip clip;
	[SerializeField] private AudioClip clip2;
	[SerializeField] private GameObject explode;
	[SerializeField] private GameObject explodePlayer;

	[SerializeField] private float speed;
	[SerializeField] private int enemyType;
	[SerializeField] private Sides side;
	[SerializeField] private float respawnOffset;
	[SerializeField] private GameObject enemyBulletPrefab;
	[SerializeField] private int points;

	private EnemyPlane plane;
	private UnityDestroyer<GameObject> dest;

	private void Awake()
	{		
		var tp = new UnityTransformProvider(transform);
		dest = new UnityDestroyer<GameObject>(null);

		plane = new EnemyPlane(tp, enemyType, new UnityTimeProvider(), speed, respawnOffset, new UnityRandom(), side, dest, points, new UnityInstantiatable<GameObject>(enemyBulletPrefab), AudioService.instance, new UnitySoundClip(clip), new UnitySoundClip(clip2), new UnityInstantiater());

		plane.enemyExplosion = new UnityGameObject(explode);
		plane.playerExplostion = new UnityGameObject(explodePlayer);		
	}

	private void Update()
	{
		plane.Fly();			
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.GetComponent<PlayerController>() != null)
		{			
			dest.obj = other.gameObject;
			plane.HitPlayer();
		}
		else if(other.GetComponent<Bullet>() != null)
		{			
			dest.obj = other.gameObject;
			plane.HitBullet();
		}
	}
}
}