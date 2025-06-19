using System;
using UnityEngine;

public class Player : MonoBehaviour {
    //References
    public bool IsWalking { get; private set; }
    public Signal<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged { get; private set; }
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [Header("Movement Settings")] 
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 7f;

    [Header("Collider Settings")] 
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float interactDistance = 1f;

    private Vector3 lastMoveDirection = Vector3.zero;
    private ClearCounter selectedCounter;
    private static Player instance;
    public static Player Instance { get; private set; }

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Another Player is present!");
        }
        Instance = this;
        
        _ = InputManager.Instance;
        OnSelectedCounterChanged = new();
        InputManager.Instance.OnInteractButton.AddListener(HandleInteractions);
    }

    private void Update() {
        Move();
    }

    private void HandleInteractions(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Vector2 input = InputManager.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, 0f, input.y);

        if (moveDir != Vector3.zero) lastMoveDirection = moveDir;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (!Physics.Raycast(origin, lastMoveDirection, out RaycastHit hit, interactDistance)) return;
        if (!hit.collider.TryGetComponent(out Interactable interactable)) return;

        interactable.Interact();

        if (interactable is ClearCounter clearCounter) {
            ChangeSelectedCounter(clearCounter != selectedCounter ? clearCounter : null);
        }
        else {
            ChangeSelectedCounter(null);
        }
    }

    private void ChangeSelectedCounter(ClearCounter counter) {
        selectedCounter = counter;
        OnSelectedCounterChanged?.Invoke(counter, new() {
            selectedCounter = counter
        });
    }

    private void Move() {
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
            }
            else {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove) transform.position += moveDir * moveSpeed * Time.deltaTime;
        IsWalking = moveDir != Vector3.zero;

        if (!IsWalking) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}