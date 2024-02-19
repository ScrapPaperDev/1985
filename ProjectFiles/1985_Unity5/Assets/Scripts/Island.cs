using UnityEngine;

namespace Unity1985{
public class Island : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float size;

	private void Update()
	{
		transform.position += new Vector3(0, -(Time.deltaTime * speed));

		if(transform.position.y < GameGlobals.down - size)
			transform.position = new Vector3(Random.Range(GameGlobals.left + transform.HalfWidth(), GameGlobals.right - transform.HalfWidth()), GameGlobals.up + size);

	}
}
}