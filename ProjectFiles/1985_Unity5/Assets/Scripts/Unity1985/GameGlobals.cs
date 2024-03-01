using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Disparity;
using Disparity.Unity;
using Humble1985;

namespace Unity1985{

public class GameGlobals :MonoBehaviour
{
	[SerializeField] private GameObject enemy;
	[SerializeField] private GameObject enemy2;
	[SerializeField] private GameObject enemy3;
	[SerializeField] private GameObject enemy4;

	[SerializeField] private float offscreenOffset;

	public GameObject playerPrefab;

	private void Awake()
	{
		Application.targetFrameRate = 60;
		Disparity.Settings.TargetFrameRate = () => Application.targetFrameRate;
	}

	private void Start()
	{
		var en1 = new EnemyWaveData(new UnityInstantiatable<GameObject>(enemy), 1, 8);
		var en2 = new EnemyWaveData(new UnityInstantiatable<GameObject>(enemy2), 12, 10);
		var en3 = new EnemyWaveData(new UnityInstantiatable<GameObject>(enemy3), 24, 12);
		var en4 = new EnemyWaveData(new UnityInstantiatable<GameObject>(enemy4), 32, 16);

		new Game(en1, en2, en3, en4, UnityScheduler.instance, new UnityRandom(), new UnityInstantiatable<GameObject>(playerPrefab), GetComponent<IPresentable>(), new UnityCamWorldBoundaryProvider(), offscreenOffset);

	}
}

}
