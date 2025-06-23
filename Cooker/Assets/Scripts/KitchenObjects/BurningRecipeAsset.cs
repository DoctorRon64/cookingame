using UnityEngine;

namespace KitchenObjects {
    [CreateAssetMenu()]
    public class BurningRecipeAsset : DataAsset {
        public KitchenObjectAsset Input;
        public KitchenObjectAsset Output;
        public float BurningTimerMax;
    }
}