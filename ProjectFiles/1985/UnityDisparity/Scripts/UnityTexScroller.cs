using UnityEngine;

namespace Disparity.Unity
{

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

}
