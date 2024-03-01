using UnityEngine;

namespace Unity1985{
public class SpriteTexture : MonoBehaviour
{
	[SerializeField] private Texture2D sprite;

	private void Awake()
	{		
		OnValidate();
	}

	private void OnValidate()
	{
		var rend = GetComponent<Renderer>();
		new UnitySpriteTexture(rend, sprite).UpdateSprite();
	}
}

public interface ISpriteTextureUpdater<T>
{
	void UpdateSprite(T t);
}


// Only need this if changing sprites is logical in the game. for now its just an editor time thing.
public class HumbleSpriteTexture
{
	
}

public class UnitySpriteTexture : ISpriteTextureUpdater<Texture2D>
{
	private Texture2D sprite;
	private Renderer rend;
	private MaterialPropertyBlock prop;

	public UnitySpriteTexture(Renderer rend)
	{
		this.rend = rend;
		prop = new MaterialPropertyBlock();
	}

	public UnitySpriteTexture(Renderer rend, Texture2D sprite)
	{
		this.rend = rend;
		prop = new MaterialPropertyBlock();
		this.sprite = sprite;
	}

	public void UpdateSprite()
	{		
		rend.GetPropertyBlock(prop);
		prop.SetTexture("_MainTex", sprite);
		rend.SetPropertyBlock(prop);
	}

	public void UpdateSprite(Texture2D spr)
	{
		sprite = spr;
		UpdateSprite();
	}

}

}