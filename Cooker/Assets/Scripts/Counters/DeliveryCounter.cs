using Counters;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) return;
        if (player.KitchenObject.TryGetPlate(out Plate plate)) {
            player.KitchenObject.DestroySelf();
        }
    }
}
