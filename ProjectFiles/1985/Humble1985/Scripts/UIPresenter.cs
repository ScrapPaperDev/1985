using System;

namespace Humble1985
{
	public class UIPresenter
	{
		public IPresentable presentable;

		public PlayerDataModel playerInfo;

		public UIPresenter(PlayerDataModel model, IPresentable p)
		{
			model.OnHealthChanged += UpdateHealthUI_OnHealthChanged;
			model.OnScoreChanged += SetScore_OnScoreChanged;
			model.OnLivesChanged += PresentLifeIcons_OnLoseALife;
			presentable = p;
		}

		public void UpdateHealthUI_OnHealthChanged(int health)
		{
			presentable.UpdateHealthUI(health);

		}

		public void SetScore_OnScoreChanged(int s)
		{
			presentable.UpdateScoreLabel(s);
		}

		public void PresentLifeIcons_OnLoseALife(int remaining)
		{
			presentable.UpdateLifeIcons(remaining);
		}
	}

	public interface IPresentable
	{
		void UpdateHealthUI(int health);
		void UpdateScoreLabel(int s);
		void UpdateLifeIcons(int remaining);
	}

	public class PlayerDataModel
	{
		public int score { get; private set; }
		public int lives { get; private set; }
		public int health { get; private set; }

		public event Action<int> OnHealthChanged = delegate { };
		public event Action<int> OnScoreChanged = delegate { };
		public event Action<int> OnLivesChanged = delegate { };

		public PlayerDataModel()
		{
			health = 100;
			lives = 3;
			score = 0;
		}

		public bool IsDead()
		{
			return health == 0;
		}

		public void SetHealth(int amount)
		{
			health -= amount;

			if (health < 0)
				health = 0;

			OnHealthChanged(health);
		}

		public void SetScore(int s)
		{
			score += s;
			OnScoreChanged(score);
		}

		public void LoseALife()
		{
			lives--;
			SetHealth(-100);
			OnLivesChanged(lives);
		}
	}

}