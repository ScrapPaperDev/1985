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
		buller = new EnemyBuller(new UnityDestroyer<GameObject>(gameObject), new UnityTransformProvider(transform), speed, home, new UnityInstantiater<GameObject>(explodePlayer), playerDestroyer);
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

	private IInstantiater explode;
	private IDestroyer playerDestroyer;

	public EnemyBuller(IDestroyer d, ITransformProvider t, float speed, bool home, IInstantiater explode, IDestroyer pd)
	{
		if(GameGlobals.player == null)
		{
			d.Destroy();
			return;
		}

		destroyer = d;
		homing = home;
		transform = t;
		this.explode = explode;
		playerDestroyer = pd;
		dirToPlayer = new FakeVector3(GameGlobals.player.pos.x - transform.pos.x, GameGlobals.player.pos.y - transform.pos.y);
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





		if(transform.pos.y < GameGlobals.down || transform.pos.y > GameGlobals.up || transform.pos.x < GameGlobals.left || transform.pos.x > GameGlobals.right)
			destroyer.Destroy();
	}

	public void HitPlayer()
	{
		if(GameGlobals.SetAndCheckHealth(10))
		{
			playerDestroyer.Destroy();
			explode.Instantiate(transform.pos);
			//GameGlobals.PlaySound(clip2);
		}
		destroyer.Destroy();
	}
}
}