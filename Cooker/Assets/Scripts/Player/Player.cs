using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 7f;

    [Header("Collider Settings")]
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.7f;

    private bool isWalking;

    private void Update() {
        Vector2 input = InputManager.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, 0f, input.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        Vector3 capsuleBottom = transform.position;
        Vector3 capsuleTop = transform.position + Vector3.up * playerHeight;

        bool canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                moveDir = moveDirX;
            } else {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        isWalking = moveDir != Vector3.zero;

        if (!isWalking) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking() => isWalking;
}