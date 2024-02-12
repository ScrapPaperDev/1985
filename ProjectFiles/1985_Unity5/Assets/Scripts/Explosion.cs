using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _1985{
public class Explosion : MonoBehaviour
{
	public bool isPlayerExp;

	private IEnumerator Start()
	{
		if(isPlayerExp)
		{
			yield return new WaitForSeconds(2);
			GameGlobals.ShowHighscoreTable();
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
			Destroy(gameObject, 1.0f);
		}

	}
}
}