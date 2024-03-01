using UnityEngine;

namespace Disparity.Unity
{

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

}
