using UnityEngine;

namespace Disparity.Unity
{

    public static class UnityAdapters
{
	public static UnityEngine.Vector4 ToUnityVector4(this FakeVector4 v4)
	{
		return new Vector4(v4.x, v4.y, v4.z, v4.w);
	}

	public static FakeVector4 ToFakeVector4(this UnityEngine.Vector4 v4)
	{
		return new FakeVector4(v4.x, v4.y, v4.z, v4.w);
	}

	public static UnityEngine.Vector3 ToUnityVector3(this FakeVector3 v)
	{
		return new Vector3(v.x, v.y, v.z);
	}

	public static FakeVector3 ToFakeVector3(this UnityEngine.Vector3 v)
	{
		return new FakeVector3(v.x, v.y);
	}

	public static Vector3 ToUnityV3(this IVector3Provider vec)
	{
		return new Vector3(vec.x, vec.y, vec.z);
	}
}

}
