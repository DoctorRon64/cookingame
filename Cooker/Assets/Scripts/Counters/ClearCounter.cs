using Interfaces;
using UnityEngine;

namespace Counters {
    public class ClearCounter : BaseCounter {
        [SerializeField] private KitchenObjectAsset kitchenObjectAsset;

        public override void Interact(Player player) {
            if (!HasKitchenObject()) {
                //There is no KitchenObject here
                if (player.HasKitchenObject()) {
                    //player is carrying something
                    player.KitchenObject.SetKitchenObjectParent(this);
                }
                else {
                    //player has nothing
                }
            }
            else {
                //there is a kitchenObjecthere
                if (player.HasKitchenObject()) {
                    //player carry somethig
                }
                else {
                    //player is nog carrying anything
                    KitchenObject.SetKitchenObjectParent(player);
                }
            }
        }
    }
}