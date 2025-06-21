using Interfaces;
using UnityEngine;

namespace Counters {
    public abstract class BaseCounter : MonoBehaviour, INteractable, IKitchenObjectParent {
        public abstract void Interact();
        
        [field: SerializeField] public Transform KitchenObjectHoldPoint { get; private set; }
        protected KitchenObject KitchenObject { get; private set; }
        
        public void SetKitchenObject(KitchenObject kitchenObject) => KitchenObject = kitchenObject;
        public void ClearKitchenObject() => KitchenObject = null;
        public bool HasKitchenObject() => KitchenObject != null;
    }
}