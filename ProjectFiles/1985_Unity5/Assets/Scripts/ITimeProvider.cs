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


public class Flipbook
{

	private readonly float frameRate;
	private readonly int length;

	private Action<int> OnFrameAdvance;
	public event Action OnDone;

	private readonly bool loop;

	public Flipbook(float fr, int length, Action<int> frameAdvance, bool loop = true)
	{
		frameRate = fr;
		OnFrameAdvance = frameAdvance;
		this.length = length;
		this.loop = loop;

	}

	public IEnumerator Flip()
	{
		//TODO: make disparity coroutine manager so we can skip all the boilerplate
		var wait = new Disparity.WaitForSeconds(frameRate);
		do
		{
			for(int i = 0; i < length; i++)
			{	
				OnFrameAdvance(i);
				yield return wait;	
			}
		}
		while(loop);

		if(OnDone != null)
			OnDone();
	}

}








public static class Utils
{
	public static float HalfWidth(this Disparity.ITransformProvider t)
	{
		return t.scale.x / 2.0f;
	}
}

public static class Settings
{
	public static Func<int> TargetFrameRate;
}

public static class Environment
{
	public static float deltaTime;
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
		var dat = data.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
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
