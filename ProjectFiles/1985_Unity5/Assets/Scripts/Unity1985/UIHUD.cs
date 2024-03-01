using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Humble1985;

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


		for(int i = 0; i < 10; i++)
			scoreFields[i].interactable = false;			

		for(int i = 0; i < 10; i++)
		{
			if(currentScore >= scores[i])
			{
				scoreFields[i].interactable = true;
				break;
			}
		}

		for(int i = 0; i < 10; i++)
			scoreFields[i].text = scores[i].ToString();

		StartCoroutine(HighScoreTable());
	}
}
}