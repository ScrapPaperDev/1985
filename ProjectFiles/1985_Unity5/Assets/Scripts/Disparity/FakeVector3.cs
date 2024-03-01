using System.Collections;
using System;
using System.IO;

namespace Disparity
{

//id say replace with fake vector3s but include a converter and put it in the setter and getter so its all done from one palce.

//-- I think we should get rid of this and just focus on FakeVector3 implementation and instead abstract away the "adapters"
//-- Or rather just work with fake vectors in the frame work and create adapters for engine implementations

public struct FakeVector3 :IVector3Provider
{

	public FakeVector3(float x, float y)
	{
		this.x = x;
		this.y = y;
		z = 0;
	}

	public IVector3Provider AddTo(IVector3Provider other)
	{
		x += other.x;
		y += other.y;
		z += other.z;

		return this;
	}

	public void Set2(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public float x { get; set; }

	public float y { get; set; }

	public float z{ get; set; }

	//	public static IVector3Provider operator +(FakeVector3 v1, FakeVector3 v2)
	//	{
	//		return v1.AddTo(v2);
	//	}

}

}
