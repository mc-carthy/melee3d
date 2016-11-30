using UnityEngine;
using UnityEngine.SceneManagement;
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

	[SerializeField]
	private GameObject arrow;
	public GameObject Arrow {
		get {
			return arrow;
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
	private GameObject[] powerupSpawnPoints;
	[SerializeField]
	private GameObject[] enemyPrefabs;
	[SerializeField]
	private GameObject[] powerupPrefabs;
	[SerializeField]
	private Text levelText;
	[SerializeField]
	private Text gameOverText;

	private GameObject newEnemy;
	private List<EnemyHealth> enemies = new List<EnemyHealth>();
	private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();
	private int currentLevel;
	private int finalLevel = 20;
	private int maxPowerups = 4;
	private int powerups = 0;
	private float generatedSpawnTime = 1f;
	private float currentSpawnTime = 0f;
	private float powerupGeneratedSpawnTime = 5f;
	private float powerupCurrentSpawnTime = 0f;


	private void Awake () {
		MakeSingleton();
	}

	private void Start () {
		currentLevel = 1;
		StartCoroutine(Spawn());
		StartCoroutine(PowerupSpawn());
		gameOverText.enabled = false;
	}

	private void Update () {
		currentSpawnTime += Time.deltaTime;
		powerupCurrentSpawnTime += Time.deltaTime;
	}

	public void PlayerHit (int currentHP) {
		if (currentHP > 0) {
			isGameOver = false;
		} else {
			isGameOver = true;
			StartCoroutine(EndGame("Defeat"));
		}
	}

	public void RegisterEnemy (EnemyHealth enemy) {
		enemies.Add(enemy);
	}

	public void KilledEnemy (EnemyHealth enemy) {
		killedEnemies.Add(enemy);
	}

	public void RegisterPowerup () {
		powerups++;
	}

	private IEnumerator Spawn () {
		if (currentSpawnTime > generatedSpawnTime) {
			currentSpawnTime = 0f;
			if (enemies.Count < currentLevel) {
				GameObject newSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
				GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
				GameObject newEnemy = Instantiate(enemyToSpawn) as GameObject;
				newEnemy.transform.position = newSpawnPoint.transform.position;
			} else if (killedEnemies.Count >= currentLevel && currentLevel <= finalLevel) {
				enemies.Clear();
				killedEnemies.Clear();
				currentLevel++;
				UpdateLevelText();
				yield return new WaitForSeconds(generatedSpawnTime * 3);
				StartCoroutine(Spawn());
			}

			if (killedEnemies.Count >= finalLevel) {
				StartCoroutine(EndGame("Victory"));
			}
		}
		yield return new WaitForSeconds(powerupGeneratedSpawnTime);
		StartCoroutine(Spawn());
	}

		private IEnumerator PowerupSpawn () {
		if (powerupCurrentSpawnTime > powerupGeneratedSpawnTime) {
			powerupCurrentSpawnTime = 0f;
			if (powerups < maxPowerups) {
				GameObject newSpawnPoint = spawnPoints[Random.Range(0, powerupSpawnPoints.Length)];
				GameObject newPowerup = Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Length)]) as GameObject;
				newPowerup.transform.position = newSpawnPoint.transform.position;
			}
		}
		yield return null;
		StartCoroutine(PowerupSpawn());
	}

	private IEnumerator EndGame (string outcome) {
		gameOverText.text = outcome;
		gameOverText.enabled = true;
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("menu");
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
