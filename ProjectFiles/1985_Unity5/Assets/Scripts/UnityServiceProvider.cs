using UnityEngine;
using System;

namespace Disparity.Unity{

public class UnityServiceProvider : MonoBehaviour
{
	[RuntimeInitializeOnLoadMethod()]
	private static void Init()
	{
		var thisT = FindObjectOfType<UnityServiceProvider>().transform;

		var s = UnityServiceFactory.CreateService<UnityScheduler>("Scheduler");
		s.transform.SetParent(thisT);

		var a = UnityServiceFactory.CreateService<AudioService>("Audio Service");
		a.transform.SetParent(thisT);
	}
}

}