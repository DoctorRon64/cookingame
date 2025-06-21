using System;
using Interfaces;
using UnityEngine;

namespace Counters {
    public class ContainerCounter : BaseCounter, IInteractable  {
        [SerializeField] private KitchenObjectAsset kitchenObjectAsset;
        public Signal<EventArgs> OnPlayerGrabbedObject { get; private set; } = new();
        
        public override void Interact(Player player) {
            if (player.HasKitchenObject()) return;
            Transform instance = Instantiate(kitchenObjectAsset.Prefab.transform);
            instance.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            
            OnPlayerGrabbedObject?.Invoke(EventArgs.Empty);




            //TODO clear comments
            /*if (!HasKitchenObject()) {
                Transform instance = Instantiate(kitchenObjectAsset.Prefab.transform);
                instance.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }
            else {
                KitchenObject.SetKitchenObjectParent(Player.Instance);
            }*/
        }
    }
}