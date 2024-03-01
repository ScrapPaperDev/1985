using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humble1985;

namespace Unity1985{
public class Menu : MonoBehaviour
{
	public void StartGame(bool modern)
	{
		Game.modern = modern;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}
}
}