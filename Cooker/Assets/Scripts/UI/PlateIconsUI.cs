using System;
using KitchenObjects;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
    [SerializeField] private Plate plate;
    [SerializeField] private Transform icon;

    private void Awake() {
        icon.gameObject.SetActive(false);
    }

    private void Start() {
        plate.OnIngredientAdded.AddListener(OnIngredientAdded);
    }

    private void OnIngredientAdded(KitchenObjectAsset asset) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in transform) {
            if (child == icon) continue;
            Destroy(child.gameObject);
        }
        
        foreach (KitchenObjectAsset asset in plate.KitchenObjAssets) {
            Transform instance = Instantiate(icon, transform.position, Quaternion.identity, transform);
            instance.gameObject.SetActive(true);
            instance.GetComponent<PlateIconsImgUI>().SetKitchenObjectAsset(asset);
        }
    }
}
