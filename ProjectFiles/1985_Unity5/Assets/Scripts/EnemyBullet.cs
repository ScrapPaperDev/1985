using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _1985{

public class EnemyBullet : MonoBehaviour
{
	[SerializeField] private float speed;

	[SerializeField] private GameObject explodePlayer;
	[SerializeField] private  AudioClip clip2;

	private void Update()
	{
		transform.position += new Vector3(0, -speed * Time.deltaTime);

		if(transform.position.y < GameGlobals.down)
			Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			if(GameGlobals.SetAndCheckHealth(30))
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