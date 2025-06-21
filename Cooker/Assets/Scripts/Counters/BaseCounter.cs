using Interfaces;
using KitchenObjects;
using UnityEngine;

namespace Counters {
    public abstract class BaseCounter : MonoBehaviour, IInteractable, IKitchenObjectParent {
        [field: SerializeField] public Transform KitchenObjectHoldPoint { get; private set; }
        protected KitchenObject KitchenObject { get; private set; }
        
        public abstract void Interact(Player player);
        public abstract void InteractAlt(Player player);

        public void SetKitchenObject(KitchenObject kitchenObject) => KitchenObject = kitchenObject;
        public void ClearKitchenObject() => KitchenObject = null;
        public bool HasKitchenObject() => KitchenObject != null;
    }
}