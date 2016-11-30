using UnityEngine;

public class EnemyMove : MonoBehaviour {

	private GameObject player;
	private NavMeshAgent nav;
	private Animator anim;
	private EnemyHealth health;

	private void Start () {
		player = GameManager.Instance.Player;
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
		health = GetComponent<EnemyHealth>();
	}

	private void Update () {
		if (!GameManager.Instance.IsGameOver && health.IsAlive) {
			nav.SetDestination(player.transform.position);
		} else if (!health.IsAlive) {
			nav.enabled = false;
		} else {
			nav.enabled = false;
			anim.Play("idle");
		}
	}
}
 