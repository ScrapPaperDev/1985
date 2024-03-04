using System.Collections;
using System;
using System.Collections.Generic;

namespace Disparity
{
	/// No reason these cant be pooled and reused. Keep that in mind for later.
	public class Coroutine : IDisposable
	{
		private bool running;
		private IScheduler scheduler;
		private Stack<IEnumerator> stache;

		public bool InProgress { get { return stache.Count > 0 && running; } }
		public bool Suspended { get { return stache.Count > 0 && !running; } }
		public bool done { get; private set; }

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
			Dispose();
		}

		public void RunCoroutine(IEnumerator co)
		{
			running = true;

			while (co.MoveNext())
			{
				object cur = co.Current;

				if (cur == null)
				{
					waitTillNextFrame = true;
					SuspendCoroutine(co);
					return;
				}
				else if (cur is float)
				{
					waitForUnscaledTime = true;
					initialFrame = true;
					timeToWait = (float)cur;
					timer = 0;
					SuspendCoroutine(co);
					return;
				}
				else if (cur is int)
				{
					waitForFrames = true;
					framesToWait = (int)cur;
					SuspendCoroutine(co);
					return;
				}
				else if (cur is IEnumerator)
				{
					SuspendCoroutine(co);
					RunCoroutine((IEnumerator)cur);
					return;
				}
				else if (cur is Yield)
				{
					SuspendCoroutine(co);
					customYield = cur as Yield;
					waitForYield = true;
					return;
				}
			}

			if (stache.Count > 0)
			{
				RunCoroutine(stache.Pop());
				return;
			}

			running = false;
			Dispose();
		}

		private void SuspendCoroutine(IEnumerator current)
		{
			running = false;
			stache.Push(current);
		}

		private void ResumeCoroutine()
		{
			if (stache.Count == 0)
			{
				Logger.Log("All coroutines have been finished but is trying to resume?");
				return;
			}

			RunCoroutine(stache.Pop());
		}


		private void Tick(float delta)
		{
			//		UnityEngine.Debug.Log("tick: " + frameCount + " / " + delta);
			//		frameCount++;

			if (waitTillNextFrame)
			{
				waitTillNextFrame = false;
				ResumeCoroutine();
			}
			else if (waitForUnscaledTime)
			{
				if (initialFrame)
				{
					initialFrame = false;
					return;
				}

				timer += delta;

				if (timer >= timeToWait)
				{
					waitForUnscaledTime = false;
					ResumeCoroutine();
				}
			}
			else if (waitForFrames)
			{
				if (frameCount == framesToWait)
				{
					waitForFrames = false;
					ResumeCoroutine();
				}

				frameCount++;
			}
			else if (waitForYield)
			{
				if (customYield.WaitIsOver())
				{
					waitForYield = false;
					ResumeCoroutine();
				}
			}
		}

		public void Dispose()
		{
			done = true;
			if (scheduler != null)
				scheduler.OnUpdated -= Tick;
		}




	}

	public abstract class Yield
	{
		public abstract bool WaitIsOver();
	}

	//TODO: test in frame rate fluctuating settings and see if consistnent. May need to create a "delta" based on target and actual
	public class WaitForSeconds : Yield
	{
		private readonly int frameCount;
		private int framesEllapsed;

		public WaitForSeconds(float time)
		{
			int applicationFPS = Disparity.Settings.TargetFrameRate();
			frameCount = ((int)(time * (float)applicationFPS)) + 1;
		}

		public WaitForSeconds(float time, int applicationFPS)
		{
			frameCount = ((int)(time * (float)applicationFPS)) + 1;
		}


		public override bool WaitIsOver()
		{
			if (framesEllapsed == frameCount)
			{
				framesEllapsed = 0;
				return true;
			}


			framesEllapsed++;
			return false;
		}
	}


	public interface IScheduler
	{
		event Action<float> OnUpdated;
	}


	public interface ISequencer
	{
		void PlaySequence(IEnumerator sequence);
	}

	public class Timeline<T> : ISequencer
	{
		public List<T> sequences;

		public void PlaySequence(IEnumerator sequence)
		{
			//if this was unity, start coroutine

			//if not unity use our implementation
		}
	}


}
