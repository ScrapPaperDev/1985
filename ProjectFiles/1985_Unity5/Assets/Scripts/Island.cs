using Disparity.Unity;
using Humble1985;
using UnityEngine;

namespace Unity1985{
public class Island : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float offset;
	private IMover mover;
	private IOffscreenable offscreener;

	private void Awake()
	{
		var t = new UnityTransformProvider(transform);
		mover = new Mover(-speed, t, new UnityTimeProvider());
		offscreener = new RespawnOffscreen(t, new UnityRandom(), offset, Sides.Down);
	}

	private void Update()
	{
		mover.Move();
		offscreener.CheckOffscreen();
	}
}
}