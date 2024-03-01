using UnityEngine;
using System;

namespace Disparity.Unity{
[DefaultExecutionOrder(-1024)]
public class UnityServiceProvider : MonoBehaviour
{
	//TODO: abstract out to a generic, not specifically... unless these are actually part of the framework... yeah they sorta are i guess
	//[RuntimeInitializeOnLoadMethod()]
	private void Awake()
	{
		var thisT = transform;

		var s = UnityServiceFactory.CreateService<UnityScheduler>("Scheduler");
		s.transform.SetParent(thisT);

		var a = UnityServiceFactory.CreateService<AudioService>("Audio Service");
		a.transform.SetParent(thisT);
	}
}

}