namespace Disparity
{

public interface ITimeProvider
{
	float deltaTime{ get; }
}


public interface IInstantiater
{
	void Instantiate<T,U>(IGameObject<T> objToIns, IVector3Provider dat);
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
	IVector3Provider pos{ get; }
	IVector3Provider rot{ get; }
	IVector3Provider scale{ get; }

}

public interface IVector3Provider
{
	float x{ get; set; }
	float y{ get; set; }
	float z{ get; set; }

	/// equivalent to vector += other
	void AddTo(IVector3Provider other);

	/// Equivalent to new Vector2(x,y)
	void Set2(float x, float y);
}


public sealed class DataAttribute : System.Attribute
{
}

}
