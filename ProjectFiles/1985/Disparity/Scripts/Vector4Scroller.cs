namespace Disparity
{

	//id say replace with fake vector3s but include a converter and put it in the setter and getter so its all done from one palce.

	//-- I think we should get rid of this and just focus on FakeVector3 implementation and instead abstract away the "adapters"
	//-- Or rather just work with fake vectors in the frame work and create adapters for engine implementations

	public class Vector4Scroller
	{

		private ITimeProvider time;
		private float speed;
		private float o;
		private FakeVector4 vec;

		public Vector4Scroller(ITimeProvider time, float speed, FakeVector4 initialScale)
		{
			this.time = time;
			vec = initialScale;
			this.speed = speed;

		}

		public FakeVector4 Scroll()
		{
			o += time.deltaTime * speed;
			o = o % 1.0f;

			vec.w = o;

			return vec;

		}

	}

}
