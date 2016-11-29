using UnityEngine;

public class HealthPowerUp : MonoBehaviour {

	private GameObject player;
	private PlayerHealth playerHealth;
	private int powerUpValue = 30;

	private void Start () {
		player = GameManager.Instance.Player;
		playerHealth = player.GetComponent<PlayerHealth>();
	}

	private void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
			playerHealth.PowerUpHealth(powerUpValue);
			Destroy(gameObject);
		}
	}
}
