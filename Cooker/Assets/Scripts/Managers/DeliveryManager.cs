using System;
using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;

public class DeliveryManager : MonoSingleton<DeliveryManager> {
    [SerializeField] private RecipeAssetsLib recipeLibrary;
    [SerializeField] private float recipeTimerMax = 5f;

    public readonly Signal OnRecipeSpawned = new();
    public readonly Signal OnRecipeSuccess = new();
    public readonly Signal OnRecipeFailed = new();
    public readonly Signal<RecipeAsset> OnRecipeDelivered = new();
    
    public List<RecipeAsset> WaitingRecipes { get; private set; }
    private float recipeTimer;

    protected override void Awake() {
        base.Awake();
        WaitingRecipes = new();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        
        OnRecipeSpawned.Clear();
        OnRecipeSuccess.Clear();
        OnRecipeFailed.Clear();
        OnRecipeDelivered.Clear();
    }

    private void Update() {
        recipeTimer -= Time.deltaTime;
        if (!(recipeTimer <= 0)) return;
        recipeTimer = recipeTimerMax;

        if (!(WaitingRecipes.Count < recipeTimerMax)) return;
        RecipeAsset waitingRecipe = recipeLibrary.Recipes[UnityEngine.Random.Range(0, recipeLibrary.Recipes.Count)];
        WaitingRecipes.Add(waitingRecipe);
        OnRecipeSpawned?.Invoke();
    }
    
    public void DeliverRecipe(Plate plate) {
        for (int i = 0; i < WaitingRecipes.Count; i++) {
            RecipeAsset waitingRecipe = WaitingRecipes[i];
            
            if (waitingRecipe.KitchenObjAssets.Count != plate.KitchenObjAssets.Count) continue;
            
            bool plateMatch = true;
            foreach (KitchenObjectAsset recipeKitchenObj in waitingRecipe.KitchenObjAssets) {
                bool ingredientFound = false;
                
                foreach (KitchenObjectAsset plateKitchenObj in plate.KitchenObjAssets) {
                    if (plateKitchenObj != recipeKitchenObj) continue;
                    ingredientFound = true;
                    break;
                }

                if (!ingredientFound) {
                    plateMatch = false;
                }
            }

            if (!plateMatch) continue;
            WaitingRecipes.RemoveAt(i);
            OnRecipeDelivered?.Invoke(waitingRecipe);
            OnRecipeSuccess?.Invoke();
            
            return;
        }
        
        OnRecipeFailed?.Invoke();
    }
}