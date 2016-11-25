using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	private int startingHealth = 20;
	[SerializeField]
	private float timeSinceLastHit = 0.5f;
	[SerializeField]
	private float dissapearSpeed = 2f;

	private AudioSource source;
	private Animator anim;
	private NavMeshAgent nav;
	private Rigidbody rb;
	private CapsuleCollider col;
	private ParticleSystem blood;

	private float timer = 0;
	private bool dissapearEnemy;
	private int currentHealth;

	private bool isAlive;
	public bool IsAlive {
		get {
			return isAlive;
		}
	}

	private void Start () {
		source = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		blood = GetComponentInChildren<ParticleSystem>();

		isAlive = true;
		currentHealth = startingHealth;
	}

	private void Update () {
		timer += Time.deltaTime;
		if (dissapearEnemy) {
			transform.Translate(Vector3.down * dissapearSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter (Collider other) {
		if (timer >= timeSinceLastHit && !GameManager.Instance.IsGameOver) {
			if (other.CompareTag("playerWeapon")) {
				TakeHit();
				timer = 0f;
			}
		}
	}

	private void TakeHit() {
		if (currentHealth > 0) {
			source.PlayOneShot(source.clip);
			currentHealth -= 10; // TODO - Remove hardcoded value
		} else {
			isAlive = false;
			KillEnemy();
		}
		anim.Play("hurt");
		blood.Play();
	}

	private void KillEnemy () {
		anim.SetTrigger("isDead");
		col.enabled = false;
		nav.enabled = false;
		rb.isKinematic = true;
		StartCoroutine(RemoveEnemy());
	}

	private IEnumerator RemoveEnemy () {
		yield return new WaitForSeconds(4f); // TODO - Remove hardcoded value
		dissapearEnemy = true;
		yield return new WaitForSeconds(2f); // TODO - Remove hardcoded value
		Destroy(gameObject);
	}
}
