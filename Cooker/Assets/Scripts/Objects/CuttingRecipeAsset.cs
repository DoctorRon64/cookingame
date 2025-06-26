using UnityEngine;

namespace KitchenObjects {
    [CreateAssetMenu()]
    public class CuttingRecipeAsset : DataAsset {
        public KitchenObjectAsset Input;
        public KitchenObjectAsset Output;
        public float CuttingProgressMax;
    }
}