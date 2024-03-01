using UnityEngine;
using System;

namespace Disparity.Unity{

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

}
