using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
    private float regularMoveSpeed = 10f;
    [SerializeField]
    private float fastMoveSpeed = 15f;
    [SerializeField]
    private float speedPowerUpDuration = 10f;
    [SerializeField]
    private LayerMask layerMask;
    private CharacterController characterController;
    private Animator anim;
    private BoxCollider[] weaponColliders;
    private Vector3 currentLookTarget = Vector3.zero;
    private float moveSpeed;
    private float turnSpeed = 10f;
    private GameObject fireTrail;
    private ParticleSystem fireTrailParticles;

    private void Start () {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        fireTrail = GameObject.FindGameObjectWithTag("Fire") as GameObject;
        fireTrailParticles = fireTrail.GetComponent<ParticleSystem>();
        fireTrail.SetActive(false);
        moveSpeed = regularMoveSpeed;
    }

    private void Update () {
        if (!GameManager.Instance.IsGameOver) {
            Move();
            Attack();
        }

    }

    private void FixedUpdate () {
        if (!GameManager.Instance.IsGameOver) {
            MouseLook();
        }
    }

    public void PlayerBeginAttack () {
		foreach (BoxCollider weapon in weaponColliders) {
			weapon.enabled = true;
		}
	}

	public void PlayerEndAttack () {
		foreach (BoxCollider weapon in weaponColliders) {
			weapon.enabled = false;
		}
	}

    public void SpeedPowerUp () {
        StartCoroutine(FireTrail());
    }

    private void Move() {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection.normalized * moveSpeed);

        if (moveDirection == Vector3.zero) {
            anim.SetBool("isWalking", false);
        } else {
            anim.SetBool("isWalking", true);
        }
    }

    private void Attack() {
        if (Input.GetMouseButtonDown(0)) {
            anim.Play("doubleChop");
        } else if (Input.GetMouseButtonDown(1)) {
            anim.Play("spinAttack");
        }
    }

    private void MouseLook() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);

        if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore)) {
            if (hit.point != currentLookTarget) {
                currentLookTarget = hit.point;
            }
            Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion rotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * turnSpeed);
        }
    }

    private IEnumerator FireTrail () {
        fireTrail.SetActive(true);
        moveSpeed = fastMoveSpeed;

        yield return new WaitForSeconds(speedPowerUpDuration);

        var em = fireTrailParticles.emission;
        em.enabled = false;
        moveSpeed = regularMoveSpeed;

        yield return new WaitForSeconds(2.1f); // Figure used as is greater than the max lifetime of the particles
        em.enabled = true;

        fireTrail.SetActive(false);
    }
}
