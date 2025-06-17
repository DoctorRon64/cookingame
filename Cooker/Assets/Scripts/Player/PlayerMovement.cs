using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    private Vector3 velocity;
    private bool isWalking;

    private void Update()
    {
        HandleInput();
        HandleRotation();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        Vector2 input = InputManager.Instance.GetMovementVectorNormalized();
        bool isSprinting = InputManager.Instance.GetSprintButton();
        
        float currentSpeed = moveSpeed;
        if (isSprinting) currentSpeed *= sprintMultiplier;
        velocity = new Vector3(input.x, 0f, input.y) * currentSpeed;
        isWalking = velocity != Vector3.zero;
    }

    private void HandleRotation()
    {
        if (!isWalking) return;

        Quaternion targetRotation = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MovePlayer() => transform.position += velocity * Time.fixedDeltaTime;
    public bool IsWalking() => isWalking;
}