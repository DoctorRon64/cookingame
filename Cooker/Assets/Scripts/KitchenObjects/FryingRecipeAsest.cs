using UnityEngine;

namespace KitchenObjects {
    [CreateAssetMenu()]
    public class FryingRecipeAsset : DataAsset {
        public KitchenObjectAsset Input;
        public KitchenObjectAsset Output;
        public float FryingTimerMax;
    }
}