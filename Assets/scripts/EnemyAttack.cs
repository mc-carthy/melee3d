using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	[SerializeField]
	private float range;
	[SerializeField]
	private float timeBetweenAttacks;

	private Animator anim;
	private BoxCollider[] weaponColliders;
	private GameObject player;
	private bool isPlayerInRange;

	private void Start () {
		weaponColliders = GetComponentsInChildren<BoxCollider>();
		player = GameManager.Instance.Player;
		anim = GetComponent<Animator>();
	}

	private void Update () {
		if (Vector3.Distance(transform.position, player.transform.position) < range) {
			isPlayerInRange = true;
		} else {
			isPlayerInRange = false;
		}
	}
}
