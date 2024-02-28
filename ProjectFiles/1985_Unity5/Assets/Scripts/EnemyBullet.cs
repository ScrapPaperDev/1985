using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1985{

public class EnemyBullet : MonoBehaviour
{
	[SerializeField] private float speed;

	[SerializeField] private GameObject explodePlayer;
	[SerializeField] private  AudioClip clip2;

	public bool isEnemy3Bullet;

	private Vector3 dirToPlayer;

	private void Start()
	{
		if(GameGlobals.player == null)
		{
			Destroy(gameObject);
			return;
		}

		dirToPlayer = GameGlobals.player.position - transform.position;
	}

	private void Update()
	{
		Vector3 velo = new Vector3(0, -speed * Time.deltaTime);

		if(!isEnemy3Bullet)
			transform.position += velo;
		else
			transform.Translate(dirToPlayer * Time.deltaTime * speed);



		if(transform.position.y < GameGlobals.down || transform.position.y > GameGlobals.up || transform.position.x < GameGlobals.left || transform.position.x > GameGlobals.right)
			Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			if(GameGlobals.SetAndCheckHealth(10))
			{
				Destroy(other.gameObject);
				Instantiate(explodePlayer, transform.position, Quaternion.identity);
				GameGlobals.PlaySound(clip2);
			}
			Destroy(gameObject);
		}
	}
}
}