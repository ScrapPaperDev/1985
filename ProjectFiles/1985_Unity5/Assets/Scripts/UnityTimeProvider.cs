using UnityEngine;
using System;

namespace Disparity.Unity{

public class UnityTimeProvider : ITimeProvider
{
	public float deltaTime	{ get { return Time.deltaTime; } }
}


public class UnityTransformProvider : ITransformProvider
{
	public UnityTransformProvider(Transform t)
	{
		transform = t;
		position = new UnityVector3();
		rotation = new UnityVector3();
		scl = new UnityVector3();
	}

	public Transform transform;


	public UnityVector3 position;
	public UnityVector3 rotation;
	public UnityVector3 scl;

	public Disparity.IVector3Provider pos
	{
		get
		{	
			position.x = transform.position.x;
			position.y = transform.position.y;
			position.z = transform.position.z;
			return  position;
		}
		set
		{			
			position.x = value.x;
			position.y = value.y;
			position.z = value.z;
			transform.position = new Vector3(position.x, position.y, position.z);
		}
	}
	public Disparity.IVector3Provider rot	{ get { return rotation; } set { } }
	public Disparity.IVector3Provider scale	{ get { return scl; } set { } }
}

public class UnityVector3 : IVector3Provider
{

	private Vector3 vec;

	public float x	{ get { return vec.x; } set { vec = new Vector3(value, vec.y, vec.z); } }
	public float y	{ get { return vec.y; } set { vec = new Vector3(vec.x, value, vec.z); } }
	public float z	{ get { return vec.z; } set { vec = new Vector3(vec.x, vec.y, value); } }


	public IVector3Provider AddTo(IVector3Provider other)
	{
		vec = new Vector3(vec.x + other.x, vec.y + other.y, vec.z + other.z);
		return this;
	}

	public void Set2(float x, float y)
	{
		vec = new Vector3(x, y);
	}
}


public class UnityInputProvider : IInputProvider
{
	public float x	{ get { return Input.GetAxisRaw("Horizontal"); } }
	public float y	{ get { return Input.GetAxisRaw("Vertical"); } }
	public bool shoot { get { return Input.GetKey(KeyCode.Space); } }
	public bool pause	{ get { throw new NotImplementedException(); } }
	
}

public class UnityInstantiater<T> : IInstantiater where T: UnityEngine.Object
{
	private T t;

	public UnityInstantiater(T t)
	{
		this.t = t;
	}

	public void Instantiate(IVector3Provider pos)
	{
		UnityEngine.Object.Instantiate(t, pos.ToUnityV3(), Quaternion.identity);
	}
}


public static class UnityHelpers
{
	public static Vector3 ToUnityV3(this IVector3Provider vec)
	{
		return new Vector3(vec.x, vec.y, vec.z);
	}
}


}
