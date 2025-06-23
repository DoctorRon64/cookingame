using Interfaces;
using KitchenObjects;
using UnityEngine;

namespace Counters {
    public abstract class BaseCounter : MonoBehaviour, IInteractable, IKitchenObjectParent {
        [field: SerializeField] public Transform KitchenObjectHoldPoint { get; private set; }
        protected internal KitchenObject KitchenObject { get; private set; }

        public virtual void Interact(Player player) { }
        public virtual void InteractAlt(Player player) { }
        public virtual void InteractAltHold(Player player) { }
        public virtual void InteractAltHoldCancel(Player player) { }

        public void SetKitchenObject(KitchenObject kitchenObject) => KitchenObject = kitchenObject;
        public void ClearKitchenObject() => KitchenObject = null;
        public bool HasKitchenObject() => KitchenObject != null;
    }
}