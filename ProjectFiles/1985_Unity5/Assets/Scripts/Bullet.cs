using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1985{
public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed;

	private void Update()
	{
		transform.position += new Vector3(0, speed * Time.deltaTime);

		if(transform.position.y > GameGlobals.up)
			Destroy(gameObject);
	}
}
}