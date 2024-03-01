using System.Collections;
using System;
using System.IO;

namespace Disparity
{

//id say replace with fake vector3s but include a converter and put it in the setter and getter so its all done from one palce.

//-- I think we should get rid of this and just focus on FakeVector3 implementation and instead abstract away the "adapters"
//-- Or rather just work with fake vectors in the frame work and create adapters for engine implementations

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
		var wait = new Disparity.WaitForSeconds(frameRate);
		do
		{
			for(int i = 0; i < length; i++)
			{	
				OnFrameAdvance(i);
				yield return wait;	
			}
		}
		while(loop);

		if(OnDone != null)
			OnDone();
	}

}

}
