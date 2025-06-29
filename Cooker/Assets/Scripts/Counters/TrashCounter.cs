using Counters;
using UnityEngine;

public class TrashCounter : BaseCounter {
    public static readonly SignalSender OnAnyObjectTrashed = new();
    
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) return;
        player.KitchenObject.DestroySelf();
        OnAnyObjectTrashed?.Invoke(this);
    }
    
    private void OnDestroy() {
        OnAnyObjectTrashed.Clear();
    }
}
