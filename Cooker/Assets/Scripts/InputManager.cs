using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>, ISingleton {
    public Signal<UnityEngine.InputSystem.InputAction.CallbackContext> OnInteractButton{ get; private set; }

    private InputSystem_Actions playerInput;

    void ISingleton.OnInitialize() {
        playerInput = new();
        playerInput.Enable();
        playerInput.Player.Enable();
        OnInteractButton = new();

        playerInput.Player.Interact.performed += ctx => {
            Debug.Log("Interact performed!");
            OnInteractButton?.Invoke(ctx);
        };
    }

    void ISingleton.OnDestroy() {
        playerInput?.Disable();
        OnInteractButton?.Clear();
    }
    
    public Vector2 GetMovementVectorRaw() =>  playerInput.Player.Move.ReadValue<Vector2>();
    public Vector2 GetMovementVectorNormalized() => playerInput.Player.Move.ReadValue<Vector2>().normalized;
}