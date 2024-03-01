namespace Disparity
{

	//id say replace with fake vector3s but include a converter and put it in the setter and getter so its all done from one palce.

	//-- I think we should get rid of this and just focus on FakeVector3 implementation and instead abstract away the "adapters"
	//-- Or rather just work with fake vectors in the frame work and create adapters for engine implementations

	public static class Utils
	{
		public static float HalfWidth(this ITransformProvider t)
		{
			return t.scale.x / 2.0f;
		}
	}

}
