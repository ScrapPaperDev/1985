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
			transform.position = new Vector3(position.x, position.y, position.z);
			return  position;
		}
	}
	public Disparity.IVector3Provider rot	{ get { return rotation; } }
	public Disparity.IVector3Provider scale	{ get { return scl; } }
}

public class UnityVector3 : IVector3Provider
{

	private Vector3 vec;

	public float x	{ get { return vec.x; } set { vec = new Vector3(value, vec.y, vec.z); } }
	public float y	{ get { return vec.y; } set { vec = new Vector3(vec.x, value, vec.z); } }
	public float z	{ get { return vec.z; } set { vec = new Vector3(vec.x, vec.y, value); } }


	public void AddTo(IVector3Provider other)
	{
		vec = new Vector3(vec.x + other.x, vec.y + other.y, vec.z + other.z);
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


}