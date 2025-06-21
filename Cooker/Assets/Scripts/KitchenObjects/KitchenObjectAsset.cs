using UnityEngine;

namespace KitchenObjects {
    [CreateAssetMenu(menuName = "Asset/KitchenObject" , fileName = nameof(KitchenObjectAsset))]
    public class KitchenObjectAsset : DataAsset {
        [field:SerializeField] public GameObject Prefab { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        public string Name => name;
    }
}