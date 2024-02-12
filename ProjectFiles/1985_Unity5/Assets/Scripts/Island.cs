using UnityEngine;

namespace _1985{
public class Island : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float size;

	private void Update()
	{
		transform.position += new Vector3(0, -(Time.deltaTime * speed));

		if(transform.position.y < -GameGlobals.yBounds - size)
			transform.position = new Vector3(Random.Range(-GameGlobals.xBounds, GameGlobals.xBounds), GameGlobals.yBounds + size);

	}
}
}