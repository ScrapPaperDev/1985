using UnityEngine;
using System;

namespace Disparity.Unity{

public static class UnityServiceFactory
{
	public static T CreateService<T>(string name = "") where T: UnityEngine.Object
	{
		string n = string.IsNullOrEmpty(name) ? typeof(T).Name : name;
		GameObject inst = new GameObject(n);
		Component comp = inst.AddComponent(typeof(T));
		UnityEngine.Object.DontDestroyOnLoad(inst);
		return comp as T;
	}

	public static bool SetService<T>(string name, IUnityService<T> singleton) where T: UnityEngine.Object
	{
		if(singleton.Instance != null)
			return false;
		
		return true;
	}
}

public interface IUnityService<T> where T : UnityEngine.Object
{
	T Instance{ get; set; }
}

}
