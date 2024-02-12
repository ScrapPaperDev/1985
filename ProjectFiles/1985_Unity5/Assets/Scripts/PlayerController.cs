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

		if(player.transform.position.x < -GameGlobals.xBounds && velo.x < 0)
			velo = new Vector3(0, velo.y, 0);
		else if(player.transform.position.x > GameGlobals.xBounds && velo.x > 0)
			velo = new Vector3(0, velo.y, 0);

		if(player.transform.position.y < -GameGlobals.yBounds + panelSize && velo.y < 0)
			velo = new Vector3(velo.x, 0, 0);
		else if(player.transform.position.y > GameGlobals.yBounds && velo.y > 0)
			velo = new Vector3(velo.x, 0, 0);

		player.transform.position += velo;

		shootTimer += Time.deltaTime;


		if(Input.GetKey(KeyCode.Space))
		{
			if(shootTimer > shootThresh)
			{
				Instantiate(bullet,transform.position,Quaternion.identity);
				shootTimer = 0;
			}
		}
	}
}
}