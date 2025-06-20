using UnityEngine;

[CreateAssetMenu(menuName = "Asset/")]
public class KitchenObjectAsset : DataAsset {
    [field:SerializeField] public GameObject Prefab { get; private set; }
    [field:SerializeField] public Sprite Icon { get; private set; }
    [SerializeField] private new string name;
}