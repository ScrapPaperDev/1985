﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1985{
public class Menu : MonoBehaviour
{



	public void StartGame(bool modern)
	{
		GameGlobals.modern = modern;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}




}
}