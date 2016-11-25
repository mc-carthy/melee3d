using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

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

	private void Awake () {
		MakeSingleton();
	}

	public void PlayerHit (int currentHP) {
		if (currentHP > 0) {
			isGameOver = false;
		} else {
			isGameOver = true;
		}
	}

	private void MakeSingleton() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
}
