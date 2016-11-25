using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	[SerializeField]
	private int startingHealth = 100;
	[SerializeField]
	private float timeSinceLastHit = 2;

	private CharacterController characterController;
	private Animator anim;

	private float timer;
	private int currentHealth;

	private void Start () {
		anim = GetComponent<Animator>();
		characterController = GetComponent<CharacterController>();
		currentHealth = startingHealth;
	}

	private void Update () {
		timer += Time.deltaTime;
	}

	private void OnTriggerEnter (Collider other) {
		if (timer >= timeSinceLastHit && !GameManager.Instance.IsGameOver) {
			if (other.CompareTag("weapon")) {
				TakeHit();
				timer = 0;
			}
		}
	}

	private void TakeHit () {
		if (currentHealth > 0) {
			GameManager.Instance.PlayerHit(currentHealth);
			anim.Play("hurt");
			currentHealth -= 10; // TODO - Remove this hardcoded value
		} else {
			KillPlayer();
		}
	}

	private void KillPlayer () {
		anim.Play("die");
		GameManager.Instance.PlayerHit(currentHealth);
		characterController.enabled = false;
	}

}
