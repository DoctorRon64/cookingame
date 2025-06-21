using System;
using Interfaces;
using KitchenObjects;
using UnityEngine;

namespace Counters {
    public class ContainerCounter : BaseCounter, IInteractable  {
        [SerializeField] private KitchenObjectAsset kitchenObjectAsset;
        public Signal<EventArgs> OnPlayerGrabbedObject { get; private set; } = new();
        
        public override void Interact(Player player) {
            if (player.HasKitchenObject()) return;
            KitchenObject.SpawnKitchenObject(kitchenObjectAsset, player);
            OnPlayerGrabbedObject?.Invoke(EventArgs.Empty);
        }

        public override void InteractAlt(Player player) {
        }
    }
}