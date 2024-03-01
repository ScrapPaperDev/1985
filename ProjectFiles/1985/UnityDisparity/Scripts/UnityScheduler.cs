using UnityEngine;
using System;

namespace Disparity.Unity
{
	[DefaultExecutionOrder(-2)]
	public class UnityScheduler : MonoBehaviour, IScheduler, IUnityService<UnityScheduler>
	{
		public static UnityScheduler instance { get; private set; }

		public event Action<float> OnUpdated = delegate { };
		public UnityScheduler Instance { get { return instance; } set { instance = value; } }

		private void Awake()
		{
			instance = this;
		}

		private void OnDestroy()
		{
			instance = null;
		}

		private void Update()
		{
			OnUpdated(Time.deltaTime);
		}
	}
}