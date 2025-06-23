using System;
using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjStruct {
        public KitchenObjectAsset Asset;
        public GameObject Object;
    }
    
    [SerializeField] private Plate plate;
    [SerializeField] private List<KitchenObjStruct> kitchenObjectsList;
    
    private void Start() {
        plate.OnIngredientAdded.AddListener(OnIngredientAdded);
        
        foreach (KitchenObjStruct objStruct in kitchenObjectsList) {
            objStruct.Object.SetActive(false);
        }
    }

    private void OnIngredientAdded(KitchenObjectAsset obj) {
        foreach (KitchenObjStruct objStruct in kitchenObjectsList) {
            if (objStruct.Asset == obj) { 
                objStruct.Object.SetActive(true);
            }
        }
    }
}
