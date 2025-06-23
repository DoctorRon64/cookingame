using Counters;
using UnityEngine;

public class TrashCounters : BaseCounter
{
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) return;
        player.KitchenObject.DestorySelf();
    }

    public override void InteractAlt(Player player) {
    }

    public override void InteractAltHold(Player player) {
    }

    public override void InteractAltHoldCancel(Player player) {
    }
}
