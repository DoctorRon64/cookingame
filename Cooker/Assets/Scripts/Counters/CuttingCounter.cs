using KitchenObjects;
using UnityEngine;

namespace Counters {
    public class CuttingCounter : BaseCounter {
        [SerializeField] private KitchenObjectAsset cutKitchenObjectAsset;
        
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
            if (!HasKitchenObject()) return;
            //cut the object
            
            KitchenObject.DestorySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectAsset, this);
        }
    }
}