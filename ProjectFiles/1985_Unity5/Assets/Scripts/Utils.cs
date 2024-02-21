using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1985{

public static class Utils
{

	public static float HalfWidth(this Transform t)
	{
		return t.transform.localScale.x / 2.0f;
	}



}

}