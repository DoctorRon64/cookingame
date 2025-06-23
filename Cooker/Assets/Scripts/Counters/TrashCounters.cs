using Counters;
using UnityEngine;

public class TrashCounters : BaseCounter
{
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) return;
        player.KitchenObject.DestroySelf();
    }
}
