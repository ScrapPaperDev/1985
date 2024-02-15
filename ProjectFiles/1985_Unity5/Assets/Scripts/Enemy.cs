using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _1985{
public class Enemy : MonoBehaviour
{

	public float speed;
	[SerializeField] private AudioClip clip;
	[SerializeField] private AudioClip clip2;
	[SerializeField] private GameObject explode;
	[SerializeField] private GameObject explodePlayer;

	[SerializeField] private float respawnOffset;

	[SerializeField] private bool isEnemy2;

	[SerializeField] private GameObject enemyBulletPrefab;

	private void Update()
	{
		float speedo = isEnemy2 ? speed / 2 : speed;

		transform.position += new Vector3(0, -(speedo * Time.deltaTime));

		if(transform.position.y < GameGlobals.down - respawnOffset)
			transform.position = new Vector3(Random.Range(GameGlobals.left + transform.HalfWidth(), GameGlobals.right - transform.HalfWidth()), GameGlobals.up + respawnOffset);


		if(isEnemy2)
		{
			if(Time.frameCount % 30 == 0)
			{
				if(UnityEngine.Random.Range(0, 100) < 30)
					Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
			}
		}
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
			else
			{
				GameGlobals.PlaySound(clip);
				Instantiate(explode, transform.position, Quaternion.identity);
				transform.position = new Vector3(Random.Range(GameGlobals.left + transform.HalfWidth(), GameGlobals.right - transform.HalfWidth()), GameGlobals.up + respawnOffset);
			}
		}
		else if(other.GetComponent<Bullet>() != null)
		{
			GameGlobals.PlaySound(clip);
			Instantiate(explode, transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			transform.position = new Vector3(Random.Range(GameGlobals.left + transform.HalfWidth(), GameGlobals.right - transform.HalfWidth()), GameGlobals.up + respawnOffset);
			int score = isEnemy2 ? 10 : 5;
			GameGlobals.SetScore(score);
		}
	}
}


}