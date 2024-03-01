namespace Disparity.Unity
{

    public class UnityDestroyer<T> : IDestroyer where T : UnityEngine.Object
{
	public T obj;
	public UnityDestroyer(T go)
	{
		obj = go;
	}

	public void Destroy()
	{
		UnityEngine.Object.Destroy(obj);
	}
}

}
