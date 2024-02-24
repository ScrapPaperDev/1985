using UnityEngine;
using System;
using Humble1985;
using Disparity.Unity;
using System.Collections;
using Disparity;
using Coroutine = Disparity.Coroutine;

namespace Unity1985{

public class CoroutineTester : MonoBehaviour, IScheduler
{
	public event Action<float> OnUpdated;
	public float deltaTime	{ get { return Time.deltaTime; } }

	private Disparity.Coroutine co;

	[ContextMenu("Test yield null")]
	private void TestYieldNull()
	{
		new Coroutine(this, TestYieldNullCo("Disparity: "));
		StartCoroutine(TestYieldNullCo("Unity: "));
	}

	public IEnumerator TestYieldNullCo(string caller)
	{
		Debug.Log(caller + "Co Started on" + Time.frameCount);
		yield return null;
		Debug.Log(caller + "Co yielded frame on" + Time.frameCount);
		yield return null;
		Debug.Log(caller + "Co done! on" + Time.frameCount);
	}


	[ContextMenu("Test Multiple nulls")]
	private void TestMultiNulls()
	{
		new Coroutine(this, TestMultiNullsCo("Disparity: "));
		StartCoroutine(TestMultiNullsCo("Unity: "));
	}

	public IEnumerator TestMultiNullsCo(string caller)
	{		
		UnityEngine.Debug.Log(caller + " Test Co Started on " + Time.frameCount);
		yield return null;
		UnityEngine.Debug.Log(caller + " Test co yielded frame on " + Time.frameCount);
		yield return null;
		yield return null;
		UnityEngine.Debug.Log(caller + " Test co yielded 2 frames on " + Time.frameCount);

		for(int i = 0; i < 12; i++)
			yield return null;

		UnityEngine.Debug.Log(caller + " Test co yielded 12 frames on " + Time.frameCount);
		UnityEngine.Debug.Log(caller + " Test co done on " + Time.frameCount);
	}

	[ContextMenu("Test Co Eternal")]
	private void TestTheCoE()
	{
		new Coroutine(this, TestCoEternal("Disparity: "));
		StartCoroutine(TestCoEternal("Unity "));
	}

	public IEnumerator TestCoEternal(string caller)
	{
		int frameCount = 0;

		while(true)
		{
			Debug.Log(caller + ": " + frameCount);
			frameCount++;
			yield return null;
		}

	}

	private void Start()
	{
		//TestTheCofloat();
	}

	[ContextMenu("Test Co float")]
	private void TestTheCofloat()
	{
		new Coroutine(this, TestUnscaledTime("Disparity "));
		StartCoroutine(TestUnscaledTime2("Unity "));
		starto = true;
	}

	public IEnumerator TestUnscaledTime(string caller)
	{
		System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
		timer.Start();
		UnityEngine.Debug.Log(caller + "Test Co Started " + Time.frameCount);
		yield return .5f;
		UnityEngine.Debug.Log(caller + "Test co yielded .5f seconds on " + Time.frameCount);
		yield return 1.5f;
		UnityEngine.Debug.Log(caller + "Test co finished on " + Time.frameCount);
		timer.Stop();
		Debug.Log("Disparity elapsed: " + timer.Elapsed.TotalSeconds);
	}

	// Custom yield instruction actually implements IEnumerator so it screws up our implementation of it so dont yield to it in ours.
	public IEnumerator TestUnscaledTime2(string caller)
	{
		System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
		timer.Start();
		UnityEngine.Debug.Log(caller + "Test Co Started " + Time.frameCount);
		yield return new WaitForSecondsRealtime(.5f);
		UnityEngine.Debug.Log(caller + "Test co yielded .5f seconds on " + Time.frameCount);
		yield return new WaitForSecondsRealtime(1.5f);
		UnityEngine.Debug.Log(caller + "Test co finished on " + Time.frameCount);
		timer.Stop();
		Debug.Log("UnityTimee elapsed: " + timer.Elapsed.TotalSeconds + " / " + Time.frameCount);
	}


	[ContextMenu("Test Co Waitforseconds")]
	private void TestTheCoSeconds()
	{
		Application.targetFrameRate = 60;
		Disparity.Settings.TargetFrameRate = () => Application.targetFrameRate;
		new Coroutine(this, TestWaitForSeconds("Disparity "));
		StartCoroutine(TestWaitForSeconds("Unity "));
	}

	public IEnumerator TestWaitForSeconds(string caller)
	{
		Debug.Log(caller + "Test Co Started " + Time.frameCount);
		yield return new UnityEngine.WaitForSeconds(.5f);
		yield return new Disparity.WaitForSeconds(.5f, Disparity.Settings.TargetFrameRate());
		Debug.Log(caller + "Test co yielded .5f seconds on " + Time.frameCount);
		yield return new UnityEngine.WaitForSeconds(1.5f);
		yield return new Disparity.WaitForSeconds(1.5f, Disparity.Settings.TargetFrameRate());
		Debug.Log(caller + "Test co finished on " + Time.frameCount);
	}


	[ContextMenu("Test Co Nested")]
	private void TestNested()
	{
		Application.targetFrameRate = 60;
		Disparity.Settings.TargetFrameRate = () => Application.targetFrameRate;
		new Coroutine(this, TestNested("Disparity "));
		StartCoroutine(TestNested("Unity "));
	}

	public IEnumerator TestNested(string caller)
	{
		Debug.Log(caller + "Nest Test Co Started " + Time.frameCount);
		yield return TestMultiNullsCo((caller));
		Debug.Log(caller + "Nest Test co finished first nested on " + Time.frameCount);
		yield return TestWaitForSeconds((caller));
		Debug.Log(caller + "Nest Test co finished second nested on " + Time.frameCount);
		yield return null;
		Debug.Log(caller + "Nest Test co finished on " + Time.frameCount);
	}



	//	// Custom yield instruction actually implements IEnumerator so it screws up our implementation of it so dont yield to it in ours.
	//	public IEnumerator TestUnscaledTime2(string caller)
	//	{
	//		System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
	//		timer.Start();
	//		UnityEngine.Debug.Log(caller + "Test Co Started " + Time.frameCount);
	//		yield return new WaitForSecondsRealtime(.5f);
	//		UnityEngine.Debug.Log(caller + "Test co yielded .5f seconds on " + Time.frameCount);
	//		yield return new WaitForSecondsRealtime(1.5f);
	//		UnityEngine.Debug.Log(caller + "Test co finished on " + Time.frameCount);
	//		timer.Stop();
	//		Debug.Log("UnityTimee elapsed: " + timer.Elapsed.TotalSeconds + " / " + Time.frameCount);
	//	}



	//	[ContextMenu("Test ness oc")]
	//	private void Testness()
	//	{
	//		co = new Disparity.Coroutine();
	//		co.RunCoroutine(TestNessCo());
	//	}

	private float tiemrTest;
	private bool starto;

	public void Update()
	{
		if(OnUpdated != null)
			OnUpdated(deltaTime);	

		if(starto)
		{
			tiemrTest += Time.deltaTime;

			if(tiemrTest >= 2.0f)
			{
				starto = false;
				Debug.Log("DELTA TIME DONE! " + Time.frameCount);
			}
		}
	}

	//	public IEnumerator TestNessCo()
	//	{
	//		UnityEngine.Debug.Log("Nest Co Started");
	//		UnityEngine.Debug.Log("ness co Done!");
	//		UnityEngine.Debug.Log("Test co done!");
	//	}









}

}