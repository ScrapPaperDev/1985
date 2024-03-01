using UnityEngine;
using Disparity;
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
}