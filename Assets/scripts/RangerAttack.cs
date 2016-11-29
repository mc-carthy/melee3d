using UnityEngine;
using System.Collections;

public class RangerAttack : MonoBehaviour {

	[SerializeField]
	private float range;
	[SerializeField]
	private float timeBetweenAttacks;

	private Animator anim;
	private GameObject player;
	private EnemyHealth health;
	private bool isPlayerInRange;
	private float lookRotationSpeed = 10f;

	private void Start () {
		anim = GetComponent<Animator>();
		player = GameManager.Instance.Player;
		health = GetComponent<EnemyHealth>();

		StartCoroutine(Attack());
	}

	private void Update () {
		if (Vector3.Distance(transform.position, player.transform.position) < range) {
			isPlayerInRange = true;
			RotateTowards(player.transform);
		} else {
			isPlayerInRange = false;
		}
		anim.SetBool("isPlayerInRange", isPlayerInRange);
	}

	private IEnumerator Attack () {
		if (isPlayerInRange && !GameManager.Instance.IsGameOver && health.IsAlive) {
			anim.Play("attack");
			yield return new WaitForSeconds(timeBetweenAttacks);
		}
		yield return null;
		StartCoroutine(Attack());
	}

	private void RotateTowards (Transform player) {
		Vector3 direction = (player.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
	}
}
