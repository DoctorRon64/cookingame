using UnityEngine;

public class InputManager : SingletonInit<InputManager>, ISingletonInit {
    private InputSystem_Actions playerInput;
    

    void ISingletonInit.OnInitialize() {
        playerInput = new();
        playerInput.Enable();
    }

    ~InputManager() {
        playerInput?.Disable();
    }
    
    public Vector2 GetMovementVectorRaw() =>  playerInput.Player.Move.ReadValue<Vector2>();
    public Vector2 GetMovementVectorNormalized() => playerInput.Player.Move.ReadValue<Vector2>().normalized;
    public bool GetInteractButton() => playerInput.Player.Interact.WasPressedThisFrame();
}