using System.Collections;
using System.Collections.Generic;
using Disparity;
using Disparity.Unity;
using UnityEngine;

namespace Unity1985{
public class EnemyBullet : MonoBehaviour
{
	[SerializeField] private float speed;

	[SerializeField] private GameObject explodePlayer;
	[SerializeField] private  AudioClip clip2;
	[SerializeField] private bool home;

	private EnemyBuller buller;
	private UnityDestroyer<GameObject> playerDestroyer;


	private void Start()
	{
		playerDestroyer = new UnityDestroyer<GameObject>(null);
		buller = new EnemyBuller(new UnityDestroyer<GameObject>(gameObject), new UnityTransformProvider(transform), speed, home, new UnityInstantiatable<GameObject>(explodePlayer), playerDestroyer, AudioService.instance, new UnitySoundClip(clip2));
	}

	private void Update()
	{
		buller.Move();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			playerDestroyer.obj = other.gameObject;
			buller.HitPlayer();
		}
			
	}
}


public class EnemyBuller
{
	public IDestroyer destroyer;
	private FakeVector3 dirToPlayer;
	private FakeVector3 velo;
	private ITransformProvider transform;

	private float speed;
	private bool homing;

	private IInstantiatable explode;
	private IDestroyer playerDestroyer;

	private IAudioPlayer player;
	private ISoundProvider cl;

	public EnemyBuller(IDestroyer d, ITransformProvider t, float speed, bool home, IInstantiatable explode, IDestroyer pd, IAudioPlayer pl, ISoundProvider clip)
	{
		if(Game.player == null)
		{
			d.Destroy();
			return;
		}

		player = pl;
		cl = clip;
		destroyer = d;
		homing = home;
		transform = t;
		this.explode = explode;
		playerDestroyer = pd;
		dirToPlayer = new FakeVector3(Game.player.pos.x - transform.pos.x, Game.player.pos.y - transform.pos.y);
		velo = new FakeVector3(transform.pos.x, transform.pos.y);
		this.speed = speed;

	}

	public void Move()
	{
		float actualSpeed = -speed * Time.deltaTime;

		if(!homing)
		{
			velo.y += actualSpeed;
			transform.pos = velo;
		}
		else
		{
			velo.AddTo(new FakeVector3(dirToPlayer.x * Time.deltaTime, dirToPlayer.y * Time.deltaTime));
			transform.pos = velo;
		}





		if(transform.pos.y < Game.down || transform.pos.y > Game.up || transform.pos.x < Game.left || transform.pos.x > Game.right)
			destroyer.Destroy();
	}

	public void HitPlayer()
	{
		Game.playerData.SetHealth(10);

		if(Game.playerData.IsDead())
		{
			playerDestroyer.Destroy();
			explode.Instantiate(transform.pos);
			player.PlaySound(cl);
		}
		destroyer.Destroy();
	}
}
}