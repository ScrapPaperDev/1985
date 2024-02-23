using System.Collections;
using System;
using System.Collections.Generic;

namespace Disparity
{
public class Coroutine
{
	private bool running;
	private IScheduler scheduler;
	private Stack<IEnumerator> stache;

	public bool InProgress{ get { return stache.Count > 0; } }
	public bool Suspended{ get { return stache.Count > 0 && !running; } }

	private bool waitTillNextFrame;
	private bool initialFrame;

	private bool waitForFrames;
	private int framesToWait;
	private int frameCount;

	private bool waitForUnscaledTime;
	private float timer;
	private float timeToWait;

	private bool waitForYield;
	private Yield customYield;

	public Coroutine()
	{
		stache = new Stack<IEnumerator>();	
	}

	public Coroutine(IScheduler scheduler)
	{
		stache = new Stack<IEnumerator>();	
		this.scheduler = scheduler;
		scheduler.OnUpdated += Tick;
	}

	public Coroutine(IScheduler scheduler, IEnumerator routine)
	{
		stache = new Stack<IEnumerator>();	
		this.scheduler = scheduler;
		scheduler.OnUpdated += Tick;
		RunCoroutine(routine);
	}

	~Coroutine()
	{
		scheduler.OnUpdated -= Tick;
	}

	public void RunCoroutine(IEnumerator co)
	{	
		running = true;
		while(co.MoveNext())
		{
			object cur = co.Current;

			if(cur == null)
			{				
				waitTillNextFrame = true;
				SuspendCoroutine(co);
				return;
			}
			else if(cur is float)
			{
				waitForUnscaledTime = true;
				initialFrame = true;
				timeToWait = (float)cur;
				timer = 0;
				SuspendCoroutine(co);
				return;
			}
			else if(cur is int)
			{
				waitForFrames = true;
				framesToWait = (int)cur;
				SuspendCoroutine(co);
				return;
			}
			else if(cur is IEnumerator)
			{
				SuspendCoroutine(co);
				RunCoroutine((IEnumerator)cur);
				return;
			}
			else if(cur is Yield)
			{
				SuspendCoroutine(co);
				customYield = cur as Yield;
				return;
			}
		}
		running = false;
	}

	private void SuspendCoroutine(IEnumerator current)
	{
		running = false;
		stache.Push(current);
	}

	private void ResumeCoroutine()
	{
		if(stache.Count == 0)
		{
			UnityEngine.Debug.LogError("All coroutines have been finished but is trying to resume?");
			return;
		}

		RunCoroutine(stache.Pop());
	}


	private void Tick(float delta)
	{
//		UnityEngine.Debug.Log("tick: " + frameCount + " / " + delta);
//		frameCount++;

		if(waitTillNextFrame)
		{
			waitTillNextFrame = false;
			ResumeCoroutine();
		}
		else if(waitForUnscaledTime)
		{
			if(initialFrame)
			{
				initialFrame = false;
				return;
			}

			timer += delta;

			if(timer >= timeToWait)
			{
				waitForUnscaledTime = false;
				ResumeCoroutine();
			}
		}
		else if(waitForFrames)
		{
			if(frameCount == framesToWait)
			{
				waitForFrames = false;
				ResumeCoroutine();
			}
		}
		else if(waitForYield)
		{
			if(customYield.WaitIsOver())
			{
				waitForYield = false;
				ResumeCoroutine();
			}
		}
	}

	//TODO: move to extensions
	public static bool TryPeek<T>(Stack<T> col, out T obj)
	{
		if(col == null || col.Count == 0)
		{
			obj = default(T);
			return false;
		}
		else
		{
			obj = col.Peek();
			return true;
		}
	}
}

public abstract class Yield
{
	public abstract bool WaitIsOver();
}

public class WaitForSeconds : Yield
{
	private readonly int frameCount;
	private int framesEllapsed;
	public WaitForSeconds(float time, int applicationFPS)
	{
		frameCount = (int)(time * applicationFPS);
	}


	public override bool WaitIsOver()
	{
		if(framesEllapsed == frameCount)
			return true;

		framesEllapsed++;
		return false;
	}
}


public interface IScheduler : ITimeProvider
{
	void Update();
	event Action<float> OnUpdated;
}


}
