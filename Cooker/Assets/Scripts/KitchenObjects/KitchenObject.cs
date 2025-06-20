using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [field: SerializeField] public KitchenObjectAsset Asset { get; private set; }
    public ClearCounter Counter { get; private set; }

    public void SetParentCounter(ClearCounter newCounter) {
        if (Counter != null) {
            Counter.ClearKitchenObject();
        }

        Counter = newCounter;
        
        if (newCounter.HasKitchenObject()) Debug.LogError("counter already has object");
        
        newCounter.SetKitchenObject(this);
        transform.SetParent(newCounter.CounterTopPoint);
        transform.localPosition = Vector3.zero;
    }
}