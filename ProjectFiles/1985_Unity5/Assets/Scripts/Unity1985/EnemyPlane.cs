using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Humble1985;
using Disparity.Unity;
using Disparity;

namespace Unity1985{

public sealed class EnemyPlane
{
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
	private ISoundPlayer audioPlayer;
	private ISoundProvider sound1;
	private ISoundProvider sound2;

	public EnemyPlane(ITransformProvider t, int type, ITimeProvider time, float speed, float offset, IRandomProvider<float> rand, Sides side, IDestroyer dest, int points, IInstantiatable buls, ISoundPlayer audio, ISoundProvider clip1, ISoundProvider clip2, IInstantiater instantiater)
	{		
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

}