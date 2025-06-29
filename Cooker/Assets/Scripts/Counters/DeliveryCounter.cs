using System;
using Counters;
using UnityEngine;

public class DeliveryCounter : BaseCounter {
    
    public static DeliveryCounter Instance { get; private set; }
    private void Awake() => Instance = this;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) return;
        if (!player.KitchenObject.TryGetPlate(out Plate plate)) return;
        DeliveryManager.Instance.DeliverRecipe(plate);
        player.KitchenObject.DestroySelf();
    }
}