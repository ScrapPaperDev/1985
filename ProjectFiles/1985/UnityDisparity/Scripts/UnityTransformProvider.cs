using UnityEngine;

namespace Disparity.Unity
{
	public class UnityTransformProvider : ITransformProvider
	{
		public UnityTransformProvider(Transform t)
		{
			transform = t;
			position = new FakeVector3();
			rotation = new FakeVector3();
			scl = new FakeVector3();
		}

		public Transform transform;

		public FakeVector3 position;
		public FakeVector3 rotation;
		public FakeVector3 scl;

		public FakeVector3 pos
		{
			get
			{
				position.x = transform.position.x;
				position.y = transform.position.y;
				position.z = transform.position.z;
				return position;
			}
			set
			{
				position.x = value.x;
				position.y = value.y;
				position.z = value.z;
				transform.position = new Vector3(position.x, position.y, position.z);
			}
		}
		public FakeVector3 rot { get { return rotation; } set { } }
		public FakeVector3 scale { get { return scl; } set { } }
	}

}
