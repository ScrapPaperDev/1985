using UnityEngine;

namespace Disparity.Unity
{

	public class UnityTimeProvider : ITimeProvider
	{
		public float deltaTime { get { return Time.deltaTime; } }
	}
}
