using System;

namespace Disparity
{
	public struct FakeVector3
	{
		public float x;
		public float y;
		public float z;

		public FakeVector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			z = 0;
		}

		public FakeVector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static FakeVector3 NullVector { get { return new FakeVector3(0, 0, 0); } }
		public static FakeVector3 FullVector { get { return new FakeVector3(1, 1, 1); } }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			FakeVector3 other = (FakeVector3)obj;
			return x == other.x && y == other.y && z == other.z;
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return $"({x}, {y}, {z})";
		}



		public static bool operator ==(FakeVector3 a, FakeVector3 b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(FakeVector3 a, FakeVector3 b)
		{
			return !a.Equals(b);
		}



		public static FakeVector3 operator +(FakeVector3 v1, FakeVector3 v2)
		{
			v1.x += v2.x;
			v1.y += v2.y;
			v1.z += v2.z;
			return v1;
		}

		//TODO: profile these cause new is not needed
		public static FakeVector3 operator -(FakeVector3 v1, FakeVector3 v2)
		{
			return new FakeVector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}

		public static FakeVector3 operator *(FakeVector3 v1, FakeVector3 v2)
		{
			return new FakeVector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
		}

		public static FakeVector3 operator /(FakeVector3 v1, FakeVector3 v2)
		{
			return new FakeVector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
		}

		public static FakeVector3 operator +(FakeVector3 a, float amount)
		{
			return new FakeVector3(a.x + amount, a.y + amount, a.z + amount);
		}

		public static FakeVector3 operator ++(FakeVector3 v)
		{
			return new FakeVector3(v.x + 1, v.y + 1, v.z + 1);
		}

		public static FakeVector3 operator --(FakeVector3 v)
		{
			return new FakeVector3(v.x - 1, v.y - 1, v.z - 1);
		}

		public static FakeVector3 operator -(FakeVector3 a, float amount)
		{
			return new FakeVector3(a.x - amount, a.y - amount, a.z - amount);
		}

		public static FakeVector3 operator *(FakeVector3 a, float amount)
		{
			return new FakeVector3(a.x * amount, a.y * amount, a.z * amount);
		}

		public static FakeVector3 operator /(FakeVector3 a, float amount)
		{
			if (amount == 0)
				throw new DivideByZeroException("amount was 0");
			return new FakeVector3(a.x / amount, a.y / amount, a.z / amount);
		}

	}

	/// <summary>
	/// Needs initialized from engine or test frameworks, as do any other types suffixed with "Adapter".
	/// </summary>
	public class Vector3Adapter<T>
	{
		private static IVector3Modifier vecMod;

		private static IVector3Converter<T> converter;

		public Vector3Adapter(IVector3Modifier m, IVector3Converter<T> engineVecType)
		{
			vecMod = m;
			converter = engineVecType;
		}

		public static float Magnitude(FakeVector3 v)
		{
			return vecMod.Magnitude(v);
		}
	}



	public interface IVector3Modifier
	{
		float Magnitude(FakeVector3 v);

		FakeVector3 Normalize(FakeVector3 v);
	}

	public class DisparityVector3Modifiers : IVector3Modifier
	{
		public float Magnitude(FakeVector3 v)
		{
			return (float)System.Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
		}

		public FakeVector3 Normalize(FakeVector3 v)
		{
			float magnitude = Magnitude(v);
			if (magnitude > 0)
				return new FakeVector3(v.x / magnitude, v.y / magnitude, v.z / magnitude);

			return FakeVector3.NullVector;
		}
	}

	public interface IVector3Converter<T>
	{
		FakeVector3 ToFake(T t);
		T FromFake(FakeVector3 fake);
	}

}