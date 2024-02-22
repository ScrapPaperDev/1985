using System.Collections;
using System;

namespace Disparity
{
public class Coroutine
{

	private IScheduler scheduler;

	private bool running;

	private System.Collections.Generic.Stack<IEnumerator> currentCos;

	private IEnumerator currentCo;

	public Coroutine()
	{
		currentCos = new System.Collections.Generic.Stack<IEnumerator>();	
	}

	public Coroutine(IScheduler scheduler)
	{
		this.scheduler = scheduler;
		scheduler.OnUpdated += Update;
	}

	~Coroutine()
	{
		scheduler.OnUpdated -= Update;
	}

	public void RunCoroutine(IEnumerator co)
	{	
		currentCo = co;	
		if(currentCos.Count > 0 && currentCos.Peek() != co)
			currentCos.Push(co);
		running = true;
		while(co.MoveNext())
		{
			var cur = co.Current;

			if(cur == null)
			{				
				waitTillNextFrame = true;
				running = false;
				return;
			}
			else if(cur is float)
			{
				waitForTime = true;
				running = false;
				timeToWait = (float)cur;
				return;
			}
			else if(cur is IEnumerator)
			{
				RunCoroutine((IEnumerator)cur);
				return;
			}
		}
		running = false;
		if(currentCos.Count > 0)
			currentCos.Pop();
		currentCo = null;
	}

	public void ResumeCoroutine()
	{
		
	}

	private bool waitTillNextFrame;
	private bool waitForTime;
	private int framesSinceLastWait;
	private int frameCount;
	private float timer;
	private float timeToWait;

	public void Update()
	{
		if(currentCo == null)
			return;

		UnityEngine.Debug.Log("Update: " + frameCount);
		frameCount++;

		if(running)
			return;				

		if(waitTillNextFrame)
		{
			waitTillNextFrame = false;
			RunCoroutine(currentCo);
			return;
		}
		//This would be unscaled time.
		//For something like wait for seconds, do time * fps and count the frames
		else if(waitForTime)
		{
			timer += UnityEngine.Time.deltaTime;

			if(timer >= timeToWait)
			{
				waitForTime = false;
				RunCoroutine(currentCo);
				return;
			}
		}
	}
}



public interface IScheduler
{
	void Update();
	event Action OnUpdated;
}


}
