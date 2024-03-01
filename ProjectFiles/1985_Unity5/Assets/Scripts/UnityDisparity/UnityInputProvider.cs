using UnityEngine;
using System;

namespace Disparity.Unity{

public class UnityInputProvider : IInputProvider
{
	public float x	{ get { return Input.GetAxisRaw("Horizontal"); } }
	public float y	{ get { return Input.GetAxisRaw("Vertical"); } }
	public bool shoot { get { return Input.GetKey(KeyCode.Space); } }
	public bool pause	{ get { throw new NotImplementedException(); } }
	
}

}
