using System;
using System.Collections;
using UnityEngine;

namespace Unity1985{
public class SpriteFlipbook : MonoBehaviour
{
	private Renderer rend;
	private MaterialPropertyBlock prop;

	[SerializeField] private Texture2D[] sprite;

	private int id;

	[SerializeField] private bool loop;
	[SerializeField] private float frameRate = .2f;

	public event Action OnDone;

	private void Awake()
	{
		OnValidate();
		id = Shader.PropertyToID("_MainTex");
	}

	private IEnumerator Start()
	{
		var wait = new WaitForSeconds(frameRate);
		do
		{
			for(int i = 0; i < sprite.Length; i++)
			{				
				
				prop.SetTexture(id, sprite[i]);
				rend.SetPropertyBlock(prop);
				yield return wait;	
			}
		}
		while(loop);

		if(OnDone != null)
			OnDone();
	}

	private void OnValidate()
	{
		if(rend == null)
			rend = GetComponent<Renderer>();

		prop = new MaterialPropertyBlock();
		rend.GetPropertyBlock(prop);
		prop.SetTexture("_MainTex", sprite[0]);
		rend.SetPropertyBlock(prop);
	}
}

}