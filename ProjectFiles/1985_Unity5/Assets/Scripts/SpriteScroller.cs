using UnityEngine;

namespace _1985{
public class SpriteScroller : MonoBehaviour
{

	[SerializeField] private Renderer rend;

	private MaterialPropertyBlock prop;

	[SerializeField] private Vector4 vec;

	[SerializeField] private float speed;

	private float o;
	private int id;

	private void Start()
	{
		prop = new MaterialPropertyBlock();
		rend.GetPropertyBlock(prop);
		id = Shader.PropertyToID("_MainTex_ST");
	}

	private void OnValidate()
	{
		prop = new MaterialPropertyBlock();
		rend.GetPropertyBlock(prop);
		prop.SetVector("_MainTex_ST", vec);
		rend.SetPropertyBlock(prop);
	}

	private void Update()
	{
		o += Time.deltaTime * speed;
		o = o % 1.0f;

		var newVec = new Vector4(vec.x, vec.y, vec.z, o);

		prop.SetVector(id, newVec);
		rend.SetPropertyBlock(prop);
	}


}



}