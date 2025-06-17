using UnityEngine;

public class InputManager : SingletonInit<InputManager>, ISingletonInit {
    private InputSystem_Actions playerInput;
    
    void ISingletonInit.OnInitialize() {
        playerInput = new();
        playerInput.Enable();
    }
    
    public Vector2 GetMovementVector() {
        return playerInput.Player.Move.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetMovementVectorNormalized() {
        return playerInput.Player.Move.ReadValue<Vector2>().normalized;
    }

    public bool GetSprintButton() {
        return playerInput.Player.Sprint.ReadValue<float>() > 0;
    }
}