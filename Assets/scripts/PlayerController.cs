using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
    private float moveSpeed = 10f;
    private CharacterController characterController;

    private void Start () {
        characterController = GetComponent<CharacterController>();
    }

    private void Update () {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection.normalized * moveSpeed);
    }
}
