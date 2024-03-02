using System.Collections;
using System;
using System.IO;

namespace Disparity
{

	public interface ITimeProvider
	{
		float deltaTime { get; }
	}

	//change to destroyer and the other to destroyable
	public interface IDestroyableTemp
	{
		void Destroy<T>(T obj) where T : class, IGameObject;
	}

	public interface IDestroyer
	{
		void Destroy();
	}

	public interface IRandomProvider<T>
	{
		T RandomRange(T min, T max);
	}

	//TODO: change name to ICreatable, since instantiation is more of an implementation of creation
	// wehere alot of times we will be drawing from a pool or something
	public interface IInstantiatable
	{
		void Instantiate(FakeVector3 pos);
	}

	public interface IInstantiater
	{
		void Instantiate<T>(T obj, FakeVector3 pos) where T : class, IGameObject;
	}

	public interface IGameObject
	{
		object obj { get; }

		T GetGameObject<T>() where T : class;
	}

	public interface IInputProvider
	{
		float x { get; }
		float y { get; }
		bool shoot { get; }
		bool pause { get; }
	}

	//id say replace with fake vector3s but include a converter and put it in the setter and getter so its all done from one palce.
	public interface ITransformProvider
	{
		FakeVector3 pos { get; set; }
		FakeVector3 rot { get; set; }
		FakeVector3 scale { get; set; }
	}




	public interface IBindable
	{
		void Bind(params object[] o);
	}

	public interface IWorldBoundaryProvider
	{
		FakeVector3 topLeft { get; }
		FakeVector3 bottomRight { get; }
		FakeVector3 topWorldPosition { get; }
		FakeVector3 bottomWorldPosition { get; }
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
			foreach (object item in dat)
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