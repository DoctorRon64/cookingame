using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeAsset : DataAsset {
    public List<KitchenObjectAsset> KitchenObjAssets;
    public string RecipeName => name;
}