using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


	public Transform player;
	public float speed;

	public float xBounds;
	public float yBounds;


	private IEnumerator Start()
	{
		for(;;)
		{
			Debug.Log("BEFORE YIELD");
			yield return null;
			Debug.Log("AFTER YIELD");
		}
	}

	private void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");


		Vector3 velo = new Vector3(x, y, 0) * (Time.deltaTime * speed);


		if(player.transform.position.x < -xBounds && velo.x < 0)
			velo = new Vector3(0, velo.y, 0);
		else if(player.transform.position.x > xBounds && velo.x > 0)
			velo = new Vector3(0, velo.y, 0);

		if(player.transform.position.y < -yBounds && velo.y < 0)
			velo = new Vector3(velo.x, 0, 0);
		else if(player.transform.position.y > yBounds && velo.y > 0)
			velo = new Vector3(velo.x, 0, 0);

		player.transform.position += velo;

		Debug.Log("UPDATING");
	}
}