using System;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearCounter : MonoBehaviour, INteractable, IKitchenObjectParent {
    [field: SerializeField] public Transform KitchenObjectHoldPoint { get; private set; }
    [SerializeField] private KitchenObjectAsset kitchenObjectAsset;

    public KitchenObject KitchenObject { get; private set; }

    public void Interact() {
        if (KitchenObject == null) {
            Transform instance = Instantiate(kitchenObjectAsset.Prefab.transform, KitchenObjectHoldPoint.position, Quaternion.identity);
            instance.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else {
            KitchenObject.SetKitchenObjectParent(Player.Instance);
        }
    }

    public void SetKitchenObject(KitchenObject kitchenObject) => KitchenObject = kitchenObject;
    public void ClearKitchenObject() => KitchenObject = null;
    public bool HasKitchenObject() => KitchenObject != null;
}