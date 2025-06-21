using KitchenObjects;
using UnityEngine;

namespace Interfaces {
    public interface IKitchenObjectParent {
        public Transform KitchenObjectHoldPoint { get; }
        public void SetKitchenObject(KitchenObject kitchenObject);
        public void ClearKitchenObject();
        public bool HasKitchenObject();
    }
}