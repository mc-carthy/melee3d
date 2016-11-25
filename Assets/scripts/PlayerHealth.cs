using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	[SerializeField]
	private int startingHealth = 100;
	[SerializeField]
	private float timeSinceLastHit = 2;
	[SerializeField]
	private Slider healthSlider;

	private CharacterController characterController;
	private Animator anim;
	private AudioSource source;

	private float timer;
	private int currentHealth;

	private void Awake () {
		Assert.IsNotNull(healthSlider);
	}

	private void Start () {
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
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
			healthSlider.value = (float)currentHealth / (float)startingHealth;
		} else {
			KillPlayer();
		}
		source.PlayOneShot(source.clip);
	}

	private void KillPlayer () {
		anim.Play("die");
		GameManager.Instance.PlayerHit(currentHealth);
		characterController.enabled = false;
	}

}
