using System;
using System.Collections;
using UnityEngine;
using Disparity;
using Disparity.Unity;

namespace Unity1985{
public class SpriteFlipbook : MonoBehaviour
{
	[SerializeField] private Texture2D[] sprite;
	[SerializeField] private bool loop;
	[SerializeField] private float frameRate = .2f;

	private ISpriteTextureUpdater<Texture2D> spriteUpdater;
	public Flipbook flipBook{ get; private set; }

	private Disparity.Coroutine co;

	private void Awake()
	{		
		spriteUpdater = new UnitySpriteTexture(GetComponent<Renderer>());
		flipBook = new Flipbook(frameRate, sprite.Length, Flip, loop);

	}

	private void Start()
	{
		co = new Disparity.Coroutine(UnityScheduler.instance, flipBook.Flip());
	}

	private void OnDestroy()
	{
		co.Dispose();
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
}