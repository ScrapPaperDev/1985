using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humble1985;
using Disparity.Unity;
using Disparity;

namespace Unity1985{
public class Enemy : MonoBehaviour
{

	
	[SerializeField] private AudioClip clip;
	[SerializeField] private AudioClip clip2;
	[SerializeField] private GameObject explode;
	[SerializeField] private GameObject explodePlayer;


	[SerializeField] private float speed;
	[SerializeField] private int enemyType;
	[SerializeField] private Sides side;
	[SerializeField] private float respawnOffset;
	[SerializeField] private GameObject enemyBulletPrefab;
	[SerializeField] private int points;

	private EnemyPlane plane;
	private UnityDestroyer<GameObject> dest;

	private void Awake()
	{		
		var ut = new UnityTimeProvider();
		var t = new UnityTransformProvider(transform);
		var tp = new UnityTransformProvider(transform);
		dest = new UnityDestroyer<GameObject>(null);

		plane = new EnemyPlane(tp, enemyType, new UnityTimeProvider(), speed, respawnOffset, new UnityRandom(), side, dest);

		plane.bullets = new UnityInstantiater<GameObject>(enemyBulletPrefab);
		plane.explosion = new UnityInstantiater<GameObject>(explode);
		plane.explosion2 = new UnityInstantiater<GameObject>(explodePlayer);
		
	}

	private void Update()
	{
		plane.Fly();			
	}

	private bool hit;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			dest.obj = other.gameObject;
			plane.HitPlayer();
		}
		else if(other.GetComponent<Bullet>() != null)
		{
			if(hit)
				return;

			hit = true;
			GameGlobals.PlaySound(clip);
			Instantiate(explode, transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			transform.position = new Vector3(Random.Range(GameGlobals.left + transform.HalfWidth(), GameGlobals.right - transform.HalfWidth()), GameGlobals.up + respawnOffset);
			int score = points;//isEnemy2 ? 10 : isEnemy3 ? 20 : isEnemy4 ? 40 : 5;
			GameGlobals.SetScore(score);
			Invoke("Unhit", .1f);
		}
	}

	private void Unhit()
	{
		hit = false;
	}
}


public sealed class EnemyPlane
{
	private float offset;
	private IMover mover;
	private IOffscreenable offscreener;
	private EnemyBehaviour behaviour;
	public IInstantiater bullets;
	public IInstantiater explosion;
	public IInstantiater explosion2;
	private RandomRespawner respawner;
	private IDestroyable destroyer;
	private ITransformProvider transform;

	public EnemyPlane(ITransformProvider t, int type, ITimeProvider time, float speed, float offset, IRandomProvider<float> rand, Sides side, IDestroyable dest)
	{		
		this.offset = offset;
		transform = t;

		float speedo = -(type == 1 ? speed / 2 : speed);
		if(type == 3)
			speedo = -speedo;

		this.mover = new Mover(speedo, t, time);

		switch(type)
		{
			case 1:
				behaviour = new Shooter(bullets, t, new UnityRandomInt());
				break;

			case 2:
				behaviour = new Shooter(bullets, t, new UnityRandomInt());
				break;
			default:
				behaviour = new EnemyBehaviour();
				break;
		}

		respawner = new RandomRespawner(t, rand, offset, side);
		offscreener = new RespawnOffscreen(t, rand, offset, side);
		destroyer = dest;
	}

	public void Fly()
	{
		mover.Move();
		offscreener.CheckOffscreen();
		behaviour.Behave();
	}

	public void HitPlayer()
	{
		if(GameGlobals.SetAndCheckHealth(30))
		{
			destroyer.Destroy();
			explosion2.Instantiate(transform.pos);
			//GameGlobals.PlaySound(clip2);
		}
		else
		{
			//GameGlobals.PlaySound(clip);
			explosion.Instantiate(transform.pos);
			respawner.Respawn();
		}
	}

}

public class EnemyBehaviour
{
	public virtual void Behave()
	{

	}
}

public class Shooter : EnemyBehaviour
{

	protected IInstantiater buller;
	protected ITransformProvider t;
	protected IRandomProvider<int> rand;

	private float shootChance;
	private int timeBetweenShots;

	private int frames;

	public Shooter(IInstantiater i, ITransformProvider t, IRandomProvider<int> r)
	{
		buller = i;
		this.t = t;
		rand = r;
		timeBetweenShots = 30;
	}

	public override void Behave()
	{
		if(frames % timeBetweenShots == 0)
		{
			if(rand.RandomRange(0, 100) < shootChance)
				buller.Instantiate(t.pos);
		}

		frames++;
		frames = frames % 100;
	}

}

}