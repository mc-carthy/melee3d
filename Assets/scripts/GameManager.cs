using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	[SerializeField]
	private GameObject player;
	public GameObject Player {
		get {
			return player;
		}
	}

	private bool isGameOver;
	public bool IsGameOver {
		get {
			return isGameOver;
		}
	}

	[SerializeField]
	private GameObject[] spawnPoints;
	[SerializeField]
	private GameObject[] enemyPrefabs;
	[SerializeField]
	private Text levelText;

	private GameObject newEnemy;
	private List<EnemyHealth> enemies = new List<EnemyHealth>();
	private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();
	private int currentLevel;
	private float generatedSpawnTime = 1f;
	private float currentSpawnTime = 0f;


	private void Awake () {
		MakeSingleton();
	}

	private void Start () {
		currentLevel = 1;
		StartCoroutine(Spawn());
	}

	private void Update () {
		currentSpawnTime += Time.deltaTime;
	}

	public void PlayerHit (int currentHP) {
		if (currentHP > 0) {
			isGameOver = false;
		} else {
			isGameOver = true;
		}
	}

	public void RegisterEnemy (EnemyHealth enemy) {
		enemies.Add(enemy);
	}

	public void KilledEnemy (EnemyHealth enemy) {
		killedEnemies.Add(enemy);
	}

	private IEnumerator Spawn () {
		if (currentSpawnTime > generatedSpawnTime) {
			currentSpawnTime = 0f;
			if (enemies.Count < currentLevel) {
				GameObject newSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
				GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
				GameObject newEnemy = Instantiate(enemyToSpawn) as GameObject;
				newEnemy.transform.position = newSpawnPoint.transform.position;
			} else if (killedEnemies.Count >= currentLevel) {
				enemies.Clear();
				killedEnemies.Clear();
				currentLevel++;
				UpdateLevelText();
				yield return new WaitForSeconds(generatedSpawnTime * 3);
				StartCoroutine(Spawn());
			}
		}
		yield return null;
		StartCoroutine(Spawn());
	}

	private void UpdateLevelText () {
		levelText.text = "Level " + currentLevel.ToString();
	}

	private void MakeSingleton () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
}
