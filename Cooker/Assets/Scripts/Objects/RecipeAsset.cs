using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu()]
public class RecipeAsset : DataAsset {
    public List<KitchenObjectAsset> KitchenObjAssets;
    
    public string LocalizedName => new LocalizedString("UIText", name).GetLocalizedString();
}