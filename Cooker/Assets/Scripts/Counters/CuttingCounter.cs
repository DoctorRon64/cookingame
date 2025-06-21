using KitchenObjects;
using UnityEngine;

namespace Counters {
    public class CuttingCounter : BaseCounter {
        [SerializeField] private CuttingRecipeAsset[] recipesArray;

        public override void Interact(Player player) {
            if (!HasKitchenObject() && player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.KitchenObject.Asset)) {
                    player.KitchenObject.SetKitchenObjectParent(this);
                }
            }
            else if (HasKitchenObject() && !player.HasKitchenObject()) {
                KitchenObject.SetKitchenObjectParent(player);
            }
            else if (HasKitchenObject() && player.HasKitchenObject()) {
                Debug.LogWarning("CANT PLACE THAT OBJECT HERE");
            }
        }

        public override void InteractAlt(Player player) {
            if (!HasKitchenObject() || !HasRecipeWithInput(KitchenObject.Asset)) return;
            KitchenObjectAsset output = GetOutputForInput(KitchenObject.Asset);
            KitchenObject.DestorySelf();
            KitchenObject.SpawnKitchenObject(output, this);
        }
        
        private bool HasRecipeWithInput(KitchenObjectAsset asset) {
            foreach (CuttingRecipeAsset recipe in recipesArray) {
                if (recipe.Input == asset) {
                    return true;
                }
            }
            return false;
        }
        
        private KitchenObjectAsset GetOutputForInput(KitchenObjectAsset input) {
            foreach (CuttingRecipeAsset recipe in recipesArray) {
                if (recipe.Input == input) {
                    return recipe.Output;
                }
            }
            return null;
        }
    }
}