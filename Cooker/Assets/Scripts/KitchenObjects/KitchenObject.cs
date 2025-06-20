using Interfaces;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [field: SerializeField] public KitchenObjectAsset Asset { get; private set; }
    public IKitchenObjectParent KitchenObjectParent { get; private set; }

    public void SetKitchenObjectParent(IKitchenObjectParent newParent) {
        KitchenObjectParent?.ClearKitchenObject();
        KitchenObjectParent = newParent;
        
        if (newParent.HasKitchenObject()) Debug.LogError("counter already has object");
        
        newParent.SetKitchenObject(this);
        transform.SetParent(newParent.KitchenObjectHoldPoint);
        transform.localPosition = Vector3.zero;
    }
}