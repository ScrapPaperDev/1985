using System;
using System.Collections;
using UnityEngine;

namespace Unity1985{
public class SpriteFlipbook : MonoBehaviour
{
	[SerializeField] private Texture2D[] sprite;
	[SerializeField] private bool loop;
	[SerializeField] private float frameRate = .2f;

	private ISpriteTextureUpdater<Texture2D> spriteUpdater;
	public Flipbook flipBook{ get; private set; }

	private void Awake()
	{		
		spriteUpdater = new UnitySpriteTexture(GetComponent<Renderer>());
		flipBook = new Flipbook(frameRate, sprite.Length, Flip, loop);
		StartCoroutine(flipBook.Flip());
	}

	private void OnValidate()
	{
		new UnitySpriteTexture(GetComponent<Renderer>()).UpdateSprite(sprite[0]);
	}

	private void Flip(int i)
	{
		spriteUpdater.UpdateSprite(sprite[i]);
	}

}

public class Flipbook
{

	private readonly float frameRate;
	private readonly int length;

	private Action<int> OnFrameAdvance;
	public event Action OnDone;

	private readonly bool loop;

	public Flipbook(float fr, int length, Action<int> frameAdvance, bool loop = true)
	{
		frameRate = fr;
		OnFrameAdvance = frameAdvance;
		this.length = length;
		this.loop = loop;

	}

	public IEnumerator Flip()
	{
		//TODO: make disparity coroutine manager so we can skip all the boilerplate
		var wait = new UnityEngine.WaitForSeconds(frameRate);
		do
		{
			for(int i = 0; i < length; i++)
			{	
				OnFrameAdvance(i);
				yield return wait;	
			}
		}
		while(loop);

		if(OnDone != null)
			OnDone();
	}

}

// aync task converter??
public interface IAsyncMethodProvider
{
	IEnumerator AyncMethod();
}

}