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

	private void Update()
	{
		transform.position += new Vector3(0, -(speed * Time.deltaTime));

		if(transform.position.y < -GameGlobals.yBounds - respawnOffset)
			transform.position = new Vector3(Random.Range(-GameGlobals.xBounds, GameGlobals.xBounds), GameGlobals.yBounds + respawnOffset);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			Instantiate(explodePlayer, transform.position, Quaternion.identity);
			GameGlobals.PlaySound(clip2);
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
		else if(other.GetComponent<Bullet>() != null)
		{
			GameGlobals.PlaySound(clip);
			Instantiate(explode, transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			transform.position = new Vector3(Random.Range(-GameGlobals.xBounds, GameGlobals.xBounds), GameGlobals.yBounds + respawnOffset);
			GameGlobals.score += 5;
		}
	}
}
}