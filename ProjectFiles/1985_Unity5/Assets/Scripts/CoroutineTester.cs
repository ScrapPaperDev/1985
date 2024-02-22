using UnityEngine;
using System;
using Humble1985;
using Disparity.Unity;
using System.Collections;

namespace Unity1985{

public class CoroutineTester : MonoBehaviour
{

	private Disparity.Coroutine co;

	[ContextMenu("Test Co")]
	private void TestTheCo()
	{
		co = new Disparity.Coroutine();
		co.RunCoroutine(TestCo());
		StartCoroutine(TestCo());
	}

	[ContextMenu("Test Co2")]
	private void TestTheCo2()
	{
		co = new Disparity.Coroutine();
		co.RunCoroutine(TestCo2());
	}

	[ContextMenu("Test Co eternal")]
	private void TestTheCoE()
	{
		co = new Disparity.Coroutine();
		co.RunCoroutine(TestCoEternal());
	}

	[ContextMenu("Test Co float")]
	private void TestTheCofloat()
	{
		co = new Disparity.Coroutine();
		co.RunCoroutine(TestCoFloat("Run "));
		StartCoroutine(TestCoFloat("Start "));
	}

	[ContextMenu("Test ness oc")]
	private void Testness()
	{
		co = new Disparity.Coroutine();
		co.RunCoroutine(TestNessCo());
	}

	private void Update()
	{
		if(co != null)
			co.Update();

	}

	public IEnumerator TestNessCo()
	{
		UnityEngine.Debug.Log("Nest Co Started");
		yield return TestCo();
		UnityEngine.Debug.Log("ness co Done!");
		UnityEngine.Debug.Log("Test co done!");
	}

	public IEnumerator TestCo()
	{
		UnityEngine.Debug.Log("Test Co Started");
		yield return null;
		UnityEngine.Debug.Log("Test co yielded frame");
		yield return null;
		UnityEngine.Debug.Log("Test co done!");
	}

	public IEnumerator TestCoFloat(string caller)
	{
		UnityEngine.Debug.Log(caller + "Test Co Started");
		yield return .5f;
		yield return new WaitForSecondsRealtime(.5f);
		UnityEngine.Debug.Log(caller + "Test co yielded .5f seconds");
		yield return 1.5f;
		yield return new WaitForSecondsRealtime(1.5f);
		UnityEngine.Debug.Log(caller + "Test co done!");
	}

	public IEnumerator TestCoEternal()
	{
		int frameCount = 0;

		while(true)
		{
			Debug.Log("Co: " + frameCount);
			frameCount++;
			yield return null;
		}

	}

	public IEnumerator TestCo2()
	{
		UnityEngine.Debug.Log("Test Co Started");
		yield return null;
		UnityEngine.Debug.Log("Test co yielded frame");
		yield return null;
		yield return null;
		UnityEngine.Debug.Log("Test co yielded 2 frame");

		for(int i = 0; i < 12; i++)
			yield return null;

		UnityEngine.Debug.Log("Test co yielded 12 frame");
		UnityEngine.Debug.Log("Test co done!");
	}

}

}