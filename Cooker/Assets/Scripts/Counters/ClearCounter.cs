using Interfaces;
using UnityEngine;

namespace Counters {
    public class ClearCounter : BaseCounter, INteractable {
        [SerializeField] private KitchenObjectAsset kitchenObjectAsset;

        public override void Interact() {
            
        }
    }
}