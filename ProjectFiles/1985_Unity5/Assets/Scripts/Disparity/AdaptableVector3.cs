using System.Collections;
using System;
using System.IO;

namespace Disparity
{

//id say replace with fake vector3s but include a converter and put it in the setter and getter so its all done from one palce.

//-- I think we should get rid of this and just focus on FakeVector3 implementation and instead abstract away the "adapters"
//-- Or rather just work with fake vectors in the frame work and create adapters for engine implementations

//TODO: so change fake vector 3 to be a plain ol struct. Then this one can be abstracted one.
public struct AdaptableVector3 : IVector3Provider
{
#region IVector3Provider implementation

	public void Set2(float x, float y)
	{
		throw new NotImplementedException();
	}

	public float x
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public float y
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public float z
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

#endregion

#region IAddable implementation

	public IVector3Provider AddTo(IVector3Provider other)
	{
		throw new NotImplementedException();
	}

#endregion

}

}
