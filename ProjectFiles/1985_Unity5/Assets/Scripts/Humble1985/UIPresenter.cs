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

}