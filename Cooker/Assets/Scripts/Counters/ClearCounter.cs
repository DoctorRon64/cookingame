using Interfaces;
using KitchenObjects;
using UnityEngine;

namespace Counters {
    public class ClearCounter : BaseCounter {
        [SerializeField] private KitchenObjectAsset kitchenObjectAsset;

        public override void Interact(Player player) {
            if (!HasKitchenObject() && player.HasKitchenObject()) {
                player.KitchenObject.SetKitchenObjectParent(this);
            }
            else if (HasKitchenObject() && !player.HasKitchenObject()) {
                KitchenObject.SetKitchenObjectParent(player);
            }
            else if (HasKitchenObject() && player.HasKitchenObject()) {
                Debug.LogWarning("CANT PLACE THAT OBJECT HERE");
            }
        }

        public override void InteractAlt(Player player) {
        }

        public override void InteractAltHold(Player player) {
        }

        public override void InteractAltHoldCancel(Player player) {
        }
    }
}