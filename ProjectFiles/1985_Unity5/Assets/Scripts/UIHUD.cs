using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Unity1985{

public class UIHUD : MonoBehaviour, IPresentable
{

	[SerializeField] private SpriteRenderer healthBar;

	[SerializeField] private TextMesh scoreText;

	[SerializeField] private SpriteRenderer[] liveIcons;

	[SerializeField] private GameObject highScoreTable;

	[SerializeField] private InputField[] scoreFields;

	private static int[] scores;
	private static string[] names;

	private void Awake()
	{
		highScoreTable.SetActive(false);

		if(scores == null)
			scores = new int[11];

	}



	public void UpdateHealthUI(int health)
	{
		float i = Mathf.Clamp((float)health / 100.0f, 0, 100f);
		healthBar.transform.parent.localScale = new Vector3(i, healthBar.transform.parent.localScale.y, 1);
		healthBar.color = Color.Lerp(Color.red, Color.yellow, i);
	}

	public void UpdateScoreLabel(int s)
	{
		scoreText.text = s.ToString();
	}

	private IEnumerator HighScoreTable()
	{
		while(highScoreTable.activeInHierarchy)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
				highScoreTable.SetActive(false);
			yield return null;
		}

		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	public void UpdateLifeIcons(int remainingLives)
	{

		for(int i = 2; i >= remainingLives; i--)
			liveIcons[i].enabled = false;

		if(remainingLives == 0)
		{
			ShowHighScoreTable(Game.playerData.score);
		}

	}

	public void ShowHighScoreTable(int currentScore)
	{
		highScoreTable.SetActive(true);

		scores[10] = currentScore;

		Array.Sort(scores, ((x, y) => y - x));

		int scoreIndex = 0;

		for(int i = 0; i < 10; i++)
			scoreFields[i].interactable = false;			

		for(int i = 0; i < 10; i++)
		{
			if(currentScore >= scores[i])
			{
				scoreFields[i].interactable = true;
				scoreIndex = i;
				break;
			}
		}

		for(int i = 0; i < 10; i++)
			scoreFields[i].text = scores[i].ToString();

		StartCoroutine(HighScoreTable());
	}

}

public interface IPresentable
{
	void UpdateHealthUI(int health);
	void UpdateScoreLabel(int s);
	void UpdateLifeIcons(int remaining);
}

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

}