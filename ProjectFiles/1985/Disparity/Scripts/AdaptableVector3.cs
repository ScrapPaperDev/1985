using System;

namespace Disparity
{
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
