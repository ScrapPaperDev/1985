using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _1985{
public class PlayerController : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private float speed;
	[SerializeField] private float panelSize;

	[SerializeField] private Bullet bullet;

	private float shootTimer;
	[SerializeField] private float shootThresh;

	private void Update()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		Vector3 velo = new Vector3(x, y, 0) * (Time.deltaTime * speed);

		if(player.position.x < GameGlobals.left + player.HalfWidth() && velo.x < 0)
			velo = new Vector3(0, velo.y, 0);

		if(player.position.x > GameGlobals.right - player.HalfWidth() && velo.x > 0)
			velo = new Vector3(0, velo.y, 0);

		if(player.position.y < GameGlobals.down + panelSize && velo.y < 0)
			velo = new Vector3(velo.x, 0, 0);

		if(player.position.y > GameGlobals.up && velo.y > 0)
			velo = new Vector3(velo.x, 0, 0);

		if(y < 0)
			velo = new Vector3(velo.x, velo.y / 2.0f, 0);

		player.position += velo;

		shootTimer += Time.deltaTime;


		if(Input.GetKey(KeyCode.Space))
		{
			if(shootTimer > shootThresh)
			{
				

				if(GameGlobals.score > 999)
				{
					Instantiate(bullet, transform.position + new Vector3(0, .25f), Quaternion.identity);
					Instantiate(bullet, transform.position + new Vector3(.5f, 0), Quaternion.identity);
					Instantiate(bullet, transform.position + new Vector3(-.5f, 0), Quaternion.identity);
				}
				else if(GameGlobals.score > 399)
				{
					Instantiate(bullet, transform.position + new Vector3(.5f, 0), Quaternion.identity);
					Instantiate(bullet, transform.position + new Vector3(-.5f, 0), Quaternion.identity);
				}
				else
				{
					Instantiate(bullet, transform.position, Quaternion.identity);
				}

				shootTimer = 0;


			}
		}
	}
}
}