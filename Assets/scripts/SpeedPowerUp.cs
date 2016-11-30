using UnityEngine;

public class SpeedPowerUp : MonoBehaviour {

	private GameObject player;
	private PlayerController controller;

	private void Start () {
		player = GameManager.Instance.Player;
		controller = player.GetComponent<PlayerController>();
		GameManager.Instance.RegisterPowerup();
	}

	private void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			controller.SpeedPowerUp();
			Destroy(gameObject);
		}
	}
}
