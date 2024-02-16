using UnityEngine;
using System.Collections;

namespace _1985{

public class GameGlobals :MonoBehaviour
{
	private static GameGlobals instance;

	//	[SerializeField] private float _xBounds;
	//	[SerializeField] private float _yBounds;

	[SerializeField] private AudioSource source;

	[SerializeField] private GameObject enemy;
	[SerializeField] private GameObject enemy2;
	[SerializeField] private GameObject enemy3;
	[SerializeField] private GameObject enemy4;

	[SerializeField] private float instOffset;


	public static float up{ get; private set; }
	public static float down{ get; private set; }
	public static float left{ get; private set; }
	public static float right{ get; private set; }



	public static int score;
	public static int lives;
	public static int health;

	[SerializeField] private SpriteRenderer healthBar;

	[SerializeField] private TextMesh scoreText;

	public static Transform player;

	public static bool SetAndCheckHealth(int amount)
	{
		health -= amount;
		float i = ((float)health).Normalize(100.0f);
		instance.healthBar.transform.parent.localScale = new Vector3(i, instance.healthBar.transform.parent.localScale.y, 1);
		instance.healthBar.color = Color.Lerp(Color.red, Color.yellow, i);

		if(health < 0)
			health = 0;

		return health == 0;
	}

	public static void SetScore(int s)
	{
		score += s;
		instance.scoreText.text = score.ToString();
	}

	public GameObject playerPrefab;

	public static void LoseALife()
	{
		lives--;

		SetAndCheckHealth(-100);


		if(lives == 0)
		{
			//GameGlobals.ShowHighscoreTable();
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
			Instantiate(instance.playerPrefab, Vector3.zero, Quaternion.identity);
		}

	}

	private void Awake()
	{
		instance = this;
		health = 100;
		lives = 3;
		StartCoroutine(obj_controller_enemy());
		StartCoroutine(obj_controller_enemy2());
		StartCoroutine(obj_controller_enemy3());
		StartCoroutine(obj_controller_enemy4());

		player = FindObjectOfType<PlayerController>().transform;

		Vector3 topLeft = new Vector3(0, Screen.height, 0);

		Vector3 bottomRight = new Vector3(Screen.width, 0, 0);

		Vector3 topWorldPosition = Camera.main.ScreenToWorldPoint(topLeft);
		Vector3 bottomWorldPosition = Camera.main.ScreenToWorldPoint(bottomRight);

		up = topWorldPosition.y;
		down = bottomWorldPosition.y;
		left = topWorldPosition.x;
		right = bottomWorldPosition.x;

		Debug.Log(up);
		Debug.Log(down);
		Debug.Log(left);
		Debug.Log(right);
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private IEnumerator obj_controller_enemy()
	{
		while(true)
		{
			Instantiate(enemy, new Vector3(Random.Range(left, right), up - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(8.0f);
		}

	}

	private IEnumerator obj_controller_enemy2()
	{
		yield return new WaitForSeconds(12);
		while(true)
		{
			Instantiate(enemy2, new Vector3(Random.Range(left, right), up - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(UnityEngine.Random.Range(12.0f, 16.0f));
		}
	}

	private IEnumerator obj_controller_enemy3()
	{
		yield return new WaitForSeconds(25);
		while(true)
		{
			Instantiate(enemy3, new Vector3(Random.Range(left, right), up - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(UnityEngine.Random.Range(12.0f, 16.0f));
		}
	}

	private IEnumerator obj_controller_enemy4()
	{
		yield return new WaitForSeconds(60);
		while(true)
		{
			Instantiate(enemy4, new Vector3(Random.Range(left, right), down - instOffset), Quaternion.identity);
			yield return new WaitForSeconds(UnityEngine.Random.Range(12.0f, 16.0f));
		}
	}

	public static void PlaySound(AudioClip clip)
	{
//		if(instance.source.isPlaying)
//			instance.source.Stop();
//
//		instance.source.clip = clip;
		instance.source.PlayOneShot(clip);
	}

	public static void ShowHighscoreTable()
	{
		//TODO: do it
	}
}

}