using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour {

	[SerializeField]
	private Transform player;
	private NavMeshAgent nav;
	private Animator anim;
	private EnemyHealth health;

	private void Awake () {
		Assert.IsNotNull(player);
		health = GetComponent<EnemyHealth>();
	}

	private void Start () {
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
	}

	private void Update () {
		if (!GameManager.Instance.IsGameOver && health.IsAlive) {
			nav.SetDestination(player.position);
		} else if (!health.IsAlive) {
			nav.enabled = false;
		} else {
			nav.enabled = false;
			anim.Play("idle");
		}
	}
}
 