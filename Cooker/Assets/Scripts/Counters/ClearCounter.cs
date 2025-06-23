using Interfaces;
using KitchenObjects;
using UnityEngine;

namespace Counters {
    public class ClearCounter : BaseCounter {
        [SerializeField] private KitchenObjectAsset kitchenObjectAsset;

        public override void Interact(Player player) {
            if (!HasKitchenObject() && player.HasKitchenObject()) {
                player.KitchenObject.SetKitchenObjectParent(this);
            } else if (HasKitchenObject() && !player.HasKitchenObject()) {
                KitchenObject.SetKitchenObjectParent(player);
            } else if (HasKitchenObject() && player.HasKitchenObject()) {
                if (player.KitchenObject.TryGetPlate(out Plate playerHoldsPlate)) {
                    if (playerHoldsPlate.TryAddIngredient(KitchenObject.Asset)) {
                        KitchenObject.DestroySelf();
                    }
                } else {
                    if (KitchenObject.TryGetPlate(out Plate counterHoldsPlate)) {
                        if (counterHoldsPlate.TryAddIngredient(player.KitchenObject.Asset)) {
                            player.KitchenObject.DestroySelf();
                        }
                    } else {
                        Debug.LogWarning("CANT PLACE THAT OBJECT HERE");
                    }
                }
            }
        }
    }
}