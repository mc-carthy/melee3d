using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour {

	[SerializeField]
	private Transform player;
	private NavMeshAgent nav;
	private Animator anim;

	private void Awake () {
		Assert.IsNotNull(player);
	}

	private void Start () {
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
	}

	private void Update () {
		nav.SetDestination(player.position);
	}
}
