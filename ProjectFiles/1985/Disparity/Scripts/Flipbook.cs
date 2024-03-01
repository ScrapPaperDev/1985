using System.Collections;
using System;

namespace Disparity
{

	public class Flipbook
	{

		private readonly float frameRate;
		private readonly int length;

		private Action<int> OnFrameAdvance;
		public event Action OnDone;

		private readonly bool loop;

		public Flipbook(float fr, int length, Action<int> frameAdvance, bool loop = true)
		{
			frameRate = fr;
			OnFrameAdvance = frameAdvance;
			this.length = length;
			this.loop = loop;

		}

		public IEnumerator Flip()
		{
			//TODO: make disparity coroutine manager so we can skip all the boilerplate
			var wait = new WaitForSeconds(frameRate);
			do
			{
				for (int i = 0; i < length; i++)
				{
					OnFrameAdvance(i);
					yield return wait;
				}
			}
			while (loop);

			if (OnDone != null)
				OnDone();
		}

	}

}
