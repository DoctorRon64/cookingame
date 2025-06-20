﻿using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>, ISingleton {
    public Signal<UnityEngine.InputSystem.InputAction.CallbackContext> OnInteractButton { get; private set; }
    public Signal OnAttackButton { get; private set; }
    public Signal OnCrouchButton { get; private set; }

    private InputSystem_Actions playerInput;

    void ISingleton.OnInitialize() {
        playerInput = new();
        playerInput.Enable();
        playerInput.Player.Enable();
        
        //declare Events
        OnInteractButton = new();
        OnAttackButton = new();
        OnCrouchButton = new();
        
        //Invoke Events
        playerInput.Player.Interact.performed += ctx => OnInteractButton?.Invoke(ctx);
        playerInput.Player.Crouch.performed += ctx => OnCrouchButton?.Invoke();
        playerInput.Player.Attack.performed += ctx => OnAttackButton?.Invoke();
    }

    void ISingleton.OnDestroy() {
        playerInput?.Disable();
        OnInteractButton?.Clear();
        OnAttackButton?.Clear();
    }

    public Vector2 GetMovementVectorRaw() => playerInput.Player.Move.ReadValue<Vector2>();
    public Vector2 GetMovementVectorNormalized() => playerInput.Player.Move.ReadValue<Vector2>().normalized;
}