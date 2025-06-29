using System;
using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;

public class Plate : KitchenObject {
    public readonly Signal<KitchenObjectAsset> OnIngredientAdded = new();
    
    [SerializeField] private List<KitchenObjectAsset> whiteListObjs = new();
    public List<KitchenObjectAsset> KitchenObjAssets { get; private set; }

    private void Awake() {
        KitchenObjAssets = new();
    }

    public bool TryAddIngredient(KitchenObjectAsset asset) {
        if (!whiteListObjs.Contains(asset)) {
            return false;
        }

        if (KitchenObjAssets.Contains(asset)) {
            return false;
        }
        else {
            KitchenObjAssets.Add(asset);
            OnIngredientAdded?.Invoke(asset);
            return true;
        }
    }
}