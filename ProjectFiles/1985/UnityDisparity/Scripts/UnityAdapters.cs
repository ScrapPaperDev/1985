using UnityEngine;

namespace Disparity.Unity
{

	public static class UnityAdapters
	{

		private static readonly IVector3Converter<Vector3> converter;


		static UnityAdapters()
		{
			converter = new UnityVector3Converter();
			new Vector3Adapter<Vector3>(null, converter);
		}

		public static Vector4 ToUnityVector4(this FakeVector4 v4)
		{
			return new Vector4(v4.x, v4.y, v4.z, v4.w);
		}

		public static FakeVector4 ToFakeVector4(this Vector4 v4)
		{
			return new FakeVector4(v4.x, v4.y, v4.z, v4.w);
		}

		public static Vector3 ToUnityVector3(this FakeVector3 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		public static FakeVector3 ToFakeVector3(this Vector3 v)
		{
			return new FakeVector3(v.x, v.y);
		}

		public static Vector3 ToUnityV3(this FakeVector3 vec)
		{
			return converter.FromFake(vec);
		}
	}

	public class UnityVectorModifier : IVector3Modifier
	{
		public float Magnitude(FakeVector3 v)
		{
			Vector3 uv = v.ToUnityV3();
			return uv.magnitude;
		}

		public FakeVector3 Normalize(FakeVector3 v)
		{
			return v.ToUnityV3().normalized.ToFakeVector3();
		}
	}

	public class UnityVector3Converter : IVector3Converter<Vector3>
	{
		public Vector3 FromFake(FakeVector3 fake)
		{
			return new Vector3(fake.x, fake.y, fake.z);
		}

		public FakeVector3 ToFake(Vector3 t)
		{
			return new FakeVector3(t.x, t.y, t.z);
		}
	}

}
