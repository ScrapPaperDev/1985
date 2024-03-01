using UnityEngine;

namespace Disparity.Unity
{

	public class UnityCamWorldBoundaryProvider : IWorldBoundaryProvider
	{
		public FakeVector3 topLeft { get { return new FakeVector3(0, Screen.height); } }
		public FakeVector3 bottomRight { get { return new FakeVector3(Screen.width, 0); } }
		public FakeVector3 topWorldPosition { get { return Camera.main.ScreenToWorldPoint(topLeft.ToUnityV3()).ToFakeVector3(); } }
		public FakeVector3 bottomWorldPosition { get { return Camera.main.ScreenToWorldPoint(bottomRight.ToUnityV3()).ToFakeVector3(); } }
	}
}
