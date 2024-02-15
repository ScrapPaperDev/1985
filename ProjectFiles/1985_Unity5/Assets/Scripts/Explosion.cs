using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _1985{
public class Explosion : MonoBehaviour
{
	public bool isPlayerExp;



	private void Awake()
	{
		if(isPlayerExp)
		{
			GetComponent<SpriteFlipbook>().OnDone += () => 
			{				
				GameGlobals.LoseALife();
				Destroy(gameObject);
			};
		}
		else
		{
			GetComponent<SpriteFlipbook>().OnDone += () => Destroy(gameObject);
		}
	}
}
}