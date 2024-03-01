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

		plane = new EnemyPlane(tp, enemyType, new UnityTimeProvider(), speed, respawnOffset, new UnityRandom(), side, dest, points, new UnityInstantiatable<GameObject>(enemyBulletPrefab), AudioService.instance, new UnitySoundClip(clip), new UnitySoundClip(clip2), new UnityInstantiater());

		plane.enemyExplosion = new UnityGameObject(explode);
		plane.playerExplostion = new UnityGameObject(explodePlayer);		
	}

	private void Update()
	{
		plane.Fly();			
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.GetComponent<PlayerController>() != null)
		{			
			dest.obj = other.gameObject;
			plane.HitPlayer();
		}
		else if(other.GetComponent<Bullet>() != null)
		{			
			dest.obj = other.gameObject;
			plane.HitBullet();
		}
	}
}


public sealed class EnemyPlane
{
	private float offset;
	private IMover mover;
	private IOffscreenable offscreener;
	private EnemyBehaviour behaviour;
	private IInstantiater instantiater;
	public IGameObject enemyExplosion;
	public IGameObject playerExplostion;
	private RandomRespawner respawner;
	private IDestroyer destroyer;
	private ITransformProvider transform;
	private bool hit;
	private int points;
	private int cooldown;
	private IAudioPlayer audioPlayer;
	private ISoundProvider sound1;
	private ISoundProvider sound2;

	public EnemyPlane(ITransformProvider t, int type, ITimeProvider time, float speed, float offset, IRandomProvider<float> rand, Sides side, IDestroyer dest, int points, IInstantiatable buls, IAudioPlayer audio, ISoundProvider clip1, ISoundProvider clip2, IInstantiater instantiater)
	{		
		this.offset = offset;
		transform = t;
		this.points = points;
		audioPlayer = audio;
		this.sound1 = clip1;
		this.sound2 = clip2;
		this.instantiater = instantiater;

		float speedo = -(type == 1 ? speed / 2 : speed);
		if(type == 3)
			speedo = -speedo;

		this.mover = new Mover(speedo, t, time);

		switch(type)
		{
			case 1:
				behaviour = new Shooter(buls, t, new UnityRandomInt());
				break;

			case 2:
				behaviour = new Shooter(buls, t, new UnityRandomInt());
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
		HitCooldown();
	}

	public void HitPlayer()
	{
		Game.playerData.SetHealth(30);

		if(Game.playerData.IsDead())
		{
			destroyer.Destroy();
			instantiater.Instantiate(playerExplostion, transform.pos);
			audioPlayer.PlaySound(sound2);
		}
		else
		{	
			instantiater.Instantiate(enemyExplosion, transform.pos);
			respawner.Respawn();
			audioPlayer.PlaySound(sound1);
		}
	}

	private void HitCooldown()
	{
		if(hit)
		{
			if(cooldown > 6)
			{
				cooldown = 0;
				hit = false;
				return;
			}
			cooldown++;
		}
	}

	public void HitBullet()
	{
		if(hit)
			return;

		hit = true;
		audioPlayer.PlaySound(sound1);
		instantiater.Instantiate(enemyExplosion, transform.pos);
		destroyer.Destroy();
		respawner.Respawn();
		int score = points;
		Game.playerData.SetScore(score);
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

	protected IInstantiatable buller;
	protected ITransformProvider t;
	protected IRandomProvider<int> rand;

	private int shootChance;
	private int timeBetweenShots;

	private int frames;

	public Shooter(IInstantiatable i, ITransformProvider t, IRandomProvider<int> r)
	{
		buller = i;
		this.t = t;
		rand = r;
		timeBetweenShots = 30;
		shootChance = 30;
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