using UnityEngine;

namespace Unity1985{
public class SpriteTexture : MonoBehaviour
{
	private Renderer rend;
	private MaterialPropertyBlock prop;

	[SerializeField] private Texture2D sprite;

	private void Awake()
	{
		OnValidate();
	}

	private void OnValidate()
	{
		if(rend == null)
			rend = GetComponent<Renderer>();

		prop = new MaterialPropertyBlock();
		rend.GetPropertyBlock(prop);
		prop.SetTexture("_MainTex", sprite);
		rend.SetPropertyBlock(prop);
	}
}


}