using System.Collections;
using System;
using System.IO;

namespace Disparity
{

public interface ITimeProvider
{
	float deltaTime{ get; }
}

public interface IDestroyer
{
	void Destroy();
}

public interface IRandomProvider<T>
{
	T RandomRange(T min, T max);
}

public interface IInstantiatable
{
	void Instantiate(IVector3Provider pos);
}

public interface IInstantiater
{
	void Instantiate<T>(T obj, IVector3Provider pos) where T: class, IGameObject;
}

public interface IGameObject
{
	object obj{ get; }

	T GetGameObject<T>() where T:class;
}

public interface IInputProvider
{
	float x{ get; }
	float y{ get; }
	bool shoot{ get; }
	bool pause{ get; }
}

//id say replace with fake vector3s but include a converter and put it in the setter and getter so its all done from one palce.
public interface ITransformProvider
{
	IVector3Provider pos{ get; set; }
	IVector3Provider rot{ get; set; }
	IVector3Provider scale{ get; set; }
}

//-- I think we should get rid of this and just focus on FakeVector3 implementation and instead abstract away the "adapters"
//-- Or rather just work with fake vectors in the frame work and create adapters for engine implementations
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


public interface IBindable
{
	void Bind(params object[] o);
}

public interface IWorldBoundaryProvider
{
	FakeVector3 topLeft{ get; }
	FakeVector3 bottomRight{ get; }
	FakeVector3 topWorldPosition{ get; }
	FakeVector3 bottomWorldPosition{ get; }
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
		var dat = data.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach(object item in dat)
			yield return item;
	}
}


public class JSONData
{
	
}


public sealed class DataAttribute : Attribute
    {
}
}