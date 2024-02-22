using System.Collections;
using System;
using System.IO;

namespace Disparity
{

public interface ITimeProvider
{
	float deltaTime{ get; }
}

public interface IDestroyable
{
	void Destroy();
}

public interface IRandomProvider<T>
{
	T RandomRange(T min, T max);
}

public interface IInstantiater
{
	void Instantiate(IVector3Provider pos);
}

public interface IGameObject<T>
{
	T obj{ get; }
}

public interface IInputProvider
{
	float x{ get; }
	float y{ get; }
	bool shoot{ get; }
	bool pause{ get; }
}

public interface ITransformProvider
{
	IVector3Provider pos{ get; set; }
	IVector3Provider rot{ get; set; }
	IVector3Provider scale{ get; set; }
}

public interface IVector3Provider : IAddable<IVector3Provider>
{
	float x{ get; set; }
	float y{ get; set; }
	float z{ get; set; }

	/// Equivalent to new Vector2(x,y)
	void Set2(float x, float y);
}

public interface IAddable<T>
{
	T AddTo(T other);
}

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

public static class Utils
{
	public static float HalfWidth(this Disparity.ITransformProvider t)
	{
		return t.scale.x / 2.0f;
	}
}

public interface IBindable
{
	void Bind(params object[] o);
}


public class SimpleSerializedData
{
	private string data;

	public SimpleSerializedData(string dataPath)
	{
		data = File.ReadAllText(dataPath);
	}

	public IEnumerable GetData()
	{
		var dat = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach(object item in dat)
			yield return item;
	}
}


public class JSONData
{
	
}


public sealed class DataAttribute : System.Attribute
{
}

















}
