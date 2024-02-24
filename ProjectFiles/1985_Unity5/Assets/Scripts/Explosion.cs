using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1985{
public class Explosion : MonoBehaviour
{
	public bool isPlayerExp;



	private void Awake()
	{
		if(isPlayerExp)
		{
			GetComponent<SpriteFlipbook>().flipBook.OnDone += () => 
			{				
				GameGlobals.LoseALife();
				Destroy(gameObject);
			};
		}
		else
		{
			GetComponent<SpriteFlipbook>().flipBook.OnDone += () => Destroy(gameObject);
		}
	}
}
}