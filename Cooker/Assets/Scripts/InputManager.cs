using System;
using UnityEngine;

public class InputManager : SingletonInit<InputManager>, ISingletonInit {
    public Signal<UnityEngine.InputSystem.InputAction.CallbackContext> OnInteractButton{ get; private set; }

    private InputSystem_Actions playerInput;

    void ISingletonInit.OnInitialize() {
        playerInput = new();
        playerInput.Enable();
        OnInteractButton = new();

        playerInput.Player.Interact.started += ctx => OnInteractButton?.Invoke(ctx);
    }

    ~InputManager() {
        playerInput?.Disable();
        OnInteractButton.Clear();
    }
    
    public Vector2 GetMovementVectorRaw() =>  playerInput.Player.Move.ReadValue<Vector2>();
    public Vector2 GetMovementVectorNormalized() => playerInput.Player.Move.ReadValue<Vector2>().normalized;
}