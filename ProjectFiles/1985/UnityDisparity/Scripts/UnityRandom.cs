namespace Disparity.Unity
{

    public class UnityRandom : IRandomProvider<float>
{
	public float RandomRange(float min, float max)
	{
		return UnityEngine.Random.Range(min, max);
	}
}

}
