using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearCounter : MonoBehaviour, Interactable {
    [field: SerializeField] public Transform CounterTopPoint { get; private set; }
    [SerializeField] private KitchenObjectAsset kitchenObjectAsset;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool booljo = false;
    
    public KitchenObject KitchenObject { get; private set; }

    private void Awake() {
        InputManager.Instance.OnCrouchButton.AddListener(HandleInteraction);
    }

    private void HandleInteraction(InputAction.CallbackContext ctx) {
        Debug.Log("HandleInteraction triggered");
        if (booljo && KitchenObject != null) {
            KitchenObject.SetParentCounter(secondClearCounter);   
        }
    }

    public void Interact() {
        if (KitchenObject == null) {
            Transform instance = Instantiate(kitchenObjectAsset.Prefab.transform, CounterTopPoint.position, Quaternion.identity);
            instance.GetComponent<KitchenObject>().SetParentCounter(this);
        }
        else {
            Debug.Log(KitchenObject.Counter);
        }
    }

    public void SetKitchenObject(KitchenObject kitchenObject) => KitchenObject = kitchenObject;
    public void ClearKitchenObject() => KitchenObject = null;
    public bool HasKitchenObject() => KitchenObject != null;
}