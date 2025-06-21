using System;
using Interfaces;
using UnityEngine;

namespace Counters {
    public class ContainerCounter : BaseCounter, INteractable  {
        [SerializeField] private KitchenObjectAsset kitchenObjectAsset;
        public Signal<EventArgs> OnPlayerGrabbedObject { get; private set; } = new();
        
        public override void Interact() {
            Transform instance = Instantiate(kitchenObjectAsset.Prefab.transform);
            instance.GetComponent<KitchenObject>().SetKitchenObjectParent(Player.Instance);
            
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