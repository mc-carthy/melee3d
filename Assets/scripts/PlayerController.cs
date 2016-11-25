using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private LayerMask layerMask;
    private CharacterController characterController;
    private Animator anim;
    private Vector3 currentLookTarget = Vector3.zero;
    private float turnSpeed = 10f;

    private void Start () {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update () {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection.normalized * moveSpeed);

        if (moveDirection == Vector3.zero) {
            anim.SetBool("isWalking", false);
        } else {
            anim.SetBool("isWalking", true);
        }

        if (Input.GetMouseButtonDown(0)) {
            anim.Play("doubleChop");
        } else if (Input.GetMouseButtonDown(1)) {
            anim.Play("spinAttack");
        }
    }

    private void FixedUpdate () {
        if (!GameManager.Instance.IsGameOver) {
            MouseLook();
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
}
