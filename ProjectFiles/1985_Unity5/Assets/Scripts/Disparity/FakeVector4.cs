using System.Collections;
using System;
using System.IO;

namespace Disparity
{

//id say replace with fake vector3s but include a converter and put it in the setter and getter so its all done from one palce.

//-- I think we should get rid of this and just focus on FakeVector3 implementation and instead abstract away the "adapters"
//-- Or rather just work with fake vectors in the frame work and create adapters for engine implementations

public struct FakeVector4
{
	public float x;
	public float y;
	public float z;
	public float w;

	public FakeVector4(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

}

}
