using UnityEngine;
using Disparity;
using System;
using Disparity.Unity;

namespace Unity1985{
public class SpriteScroller : MonoBehaviour
{
	[SerializeField] private Renderer rend;
	[SerializeField] private float speed;
	[SerializeField] private Vector4 scale;

	private Vector4Scroller vecScr;
	private TextureScroller texScroll;

	private void Start()
	{
		vecScr = new Vector4Scroller(new UnityTimeProvider(), speed, scale.ToFakeVector4());
		texScroll = new UnityTexScroller(rend);
	}

	private void OnValidate()
	{
		texScroll = new UnityTexScroller(rend);
		texScroll.Preview(scale.ToFakeVector4());
	}



	private void Update()
	{
		FakeVector4 newVec = vecScr.Scroll();
		texScroll.UpdateScroll(newVec);
	}


}

public abstract class TextureScroller
{
	public abstract void UpdateScroll(FakeVector4 vec);
	public abstract void Preview(FakeVector4 vec);

}

public class UnityTexScroller : TextureScroller
{
	private Renderer rend;
	private MaterialPropertyBlock prop;
	private int id;

	public UnityTexScroller(Renderer rend)
	{
		this.rend = rend;
		prop = new MaterialPropertyBlock();
		rend.GetPropertyBlock(prop);
		id = Shader.PropertyToID("_MainTex_ST");
	}

	public override void UpdateScroll(FakeVector4 vec)
	{
		Vector4 uv = UnityAdapters.ToUnityVector4(vec);

		prop.SetVector(id, uv);
		rend.SetPropertyBlock(prop);
	}

	public override void Preview(FakeVector4 vec)
	{
		Vector4 uv = UnityAdapters.ToUnityVector4(vec);
		prop = new MaterialPropertyBlock();
		rend.GetPropertyBlock(prop);
		prop.SetVector("_MainTex_ST", uv);
		rend.SetPropertyBlock(prop);
	}
			
}


public class Vector4Scroller
{

	private ITimeProvider time;
	private float speed;
	private float o;
	private FakeVector4 vec;

	public Vector4Scroller(ITimeProvider time, float speed, FakeVector4 initialScale)
	{
		this.time = time;
		vec = initialScale;
		this.speed = speed;

	}

	public FakeVector4 Scroll()
	{
		o += time.deltaTime * speed;
		o = o % 1.0f;

		vec.w = o;

		return vec;

	}


}

public struct FakeVector4
{
	public float x;
	public float y;
	public float z;
	public float w;

	public FakeVector4(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}


}

public static class UnityAdapters
{
	public static UnityEngine.Vector4 ToUnityVector4(this FakeVector4 v4)
	{
		return new Vector4(v4.x, v4.y, v4.z, v4.w);
	}

	public static FakeVector4 ToFakeVector4(this UnityEngine.Vector4 v4)
	{
		return new FakeVector4(v4.x, v4.y, v4.z, v4.w);
	}

	public static UnityEngine.Vector3 ToUnityVector3(this FakeVector3 v)
	{
		return new Vector3(v.x, v.y, v.z);
	}

	public static FakeVector3 ToFakeVector3(this UnityEngine.Vector3 v)
	{
		return new FakeVector3(v.x, v.y);
	}
}



}