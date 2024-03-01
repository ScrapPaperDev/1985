using UnityEngine;
using System;

namespace Disparity.Unity{

public class UnityGameObject : IGameObject
{
	public UnityGameObject(GameObject go)
	{
		obj = go;
	}

	public T GetGameObject<T>()where T: class
	{
		return obj as T;
	}

	public object obj	{ get; set; }
}

}
