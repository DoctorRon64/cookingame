using System;
using Counters;
using Interfaces;
using KitchenObjects;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
    // Events
    public class OnSelectedCounterChangedEventArgs : EventArgs { public IInteractable HighlightedCounter; }
    [field: SerializeField] public Signal<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged { get; private set; }

    // State
    public bool IsWalking { get; private set; }
    public static Player Instance { get; private set; }
    public KitchenObject KitchenObject { get; private set; }

    [field: SerializeField] public Transform KitchenObjectHoldPoint { get; private set; }
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float interactDistance = 1f;

    private Vector3 lastMoveDirection = Vector3.forward;
    private IInteractable selectedCounter;
    private IInteractable highlightedCounter;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Player instance detected!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        OnSelectedCounterChanged = new();

        _ = InputManager.Instance;
        InputManager.Instance.OnInteractButton.AddListener(HandleInteraction);
        InputManager.Instance.OnAttackButton.AddListener(HandleInteractionAlt);
    }

    private void Update() {
        Move();
        DetectCounterInFront();
    }

    private void Move() {
        Vector2 input = InputManager.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, 0f, input.y);

        if (moveDir != Vector3.zero) lastMoveDirection = moveDir;

        float moveDistance = moveSpeed * Time.deltaTime;
        Vector3 capsuleBottom = transform.position;
        Vector3 capsuleTop = transform.position + Vector3.up * playerHeight;

        bool canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDirX, moveDistance);
            if (canMove) {
                moveDir = moveDirX;
            }
            else {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDirZ, moveDistance);
                if (canMove) {
                    moveDir = moveDirZ;
                    canMove = true;
                }
            }
        }

        if (canMove) transform.position += moveDir * moveSpeed * Time.deltaTime;

        IsWalking = moveDir != Vector3.zero;

        if (!IsWalking) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void DetectCounterInFront() {
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        BaseCounter newCounter = null;
        if (Physics.Raycast(origin, lastMoveDirection, out RaycastHit hit, interactDistance)) {
            hit.collider.TryGetComponent(out newCounter);
        }

        if ((BaseCounter)highlightedCounter == newCounter) return;

        highlightedCounter = newCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            HighlightedCounter = highlightedCounter
        });
    }

    private void HandleInteraction(UnityEngine.InputSystem.InputAction.CallbackContext ctx) {
        highlightedCounter?.Interact(this);
        selectedCounter = highlightedCounter != selectedCounter ? highlightedCounter : null;

        OnSelectedCounterChanged?.Invoke(this, new() {
            HighlightedCounter = highlightedCounter
        });
    }

    private void HandleInteractionAlt() {
        highlightedCounter?.InteractAlt(this);
        
        //selectedCounter = highlightedCounter != selectedCounter ? highlightedCounter : null;
        /*OnSelectedCounterChanged?.Invoke(this, new() {
            HighlightedCounter = highlightedCounter
        });*/
    }

    public void SetKitchenObject(KitchenObject kitchenObject) => KitchenObject = kitchenObject;
    public void ClearKitchenObject() => KitchenObject = null;
    public bool HasKitchenObject() => KitchenObject != null;
}