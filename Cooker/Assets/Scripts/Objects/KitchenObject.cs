using Interfaces;
using UnityEngine;

namespace KitchenObjects {
    public class KitchenObject : MonoBehaviour {
        [field: SerializeField] public KitchenObjectAsset Asset { get; private set; }
        private IKitchenObjectParent KitchenObjectParent { get; set; }

        public void SetKitchenObjectParent(IKitchenObjectParent newParent) {
            KitchenObjectParent?.ClearKitchenObject();
            KitchenObjectParent = newParent;
        
            if (newParent.HasKitchenObject()) Debug.LogError("counter already has object");
        
            newParent.SetKitchenObject(this);
            transform.SetParent(newParent.KitchenObjectHoldPoint);
            transform.localPosition = Vector3.zero;
        }

        public void DestroySelf() {
            KitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public static KitchenObject SpawnKitchenObject(KitchenObjectAsset asset, IKitchenObjectParent parent) {
            Transform instance = Instantiate(asset.Prefab.transform);
            KitchenObject kitchenObject = instance.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(parent);
            return kitchenObject;
        }

        public bool TryGetPlate(out Plate plate) {
            if (this is Plate) {
                plate = this as Plate;
                return true;
            } else {
                plate = null;
                return false;
            }
        }
    }
}