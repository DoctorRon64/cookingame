using System;
using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;

public class Plate : KitchenObject {
    public Signal<KitchenObjectAsset> OnIngredientAdded = new();
    
    [SerializeField] private List<KitchenObjectAsset> whiteListObjs = new();
    private List<KitchenObjectAsset> kitchenObjAssets;

    private void Awake() {
        kitchenObjAssets = new();
    }

    public bool TryAddIngredient(KitchenObjectAsset asset) {
        if (!whiteListObjs.Contains(asset)) {
            return false;
        }

        if (kitchenObjAssets.Contains(asset)) {
            return false;
        }
        else {
            kitchenObjAssets.Add(asset);
            OnIngredientAdded?.Invoke(asset);
            return true;
        }
    }
}