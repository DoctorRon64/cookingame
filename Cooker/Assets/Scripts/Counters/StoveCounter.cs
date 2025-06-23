using System;
using Counters;
using Interfaces;
using KitchenObjects;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    [SerializeField] private FryingRecipeAsset[] fryingRecipes;
    [SerializeField] private BurningRecipeAsset[] burningRecipes;
    public Signal<IHasProgress.ProgressNormalize> OnProgressChanged { get; private set; } = new();
    public StateMachine<StoveCounter> StoveStateMachine { get; private set; }

    protected internal float FryingTimer;
    protected internal float BurningTimer;
    internal FryingRecipeAsset FryingRecipeAsset;
    internal BurningRecipeAsset BurningRecipeAsset;
    
    private void Awake() {
        StoveStateMachine = new(this);
        StoveStateMachine.Add<StoveIdleState>();
        StoveStateMachine.Add<StoveFryingState>();
        StoveStateMachine.Add<StoveFriedState>();
        StoveStateMachine.Add<StoveBurnedState>();
        StoveStateMachine.Switch<StoveIdleState>();
    }

    private void Update() {
        if (HasKitchenObject()) {
            StoveStateMachine?.OnUpdate();
        }
    }

    private void OnDestroy() {
        StoveStateMachine?.OnStateChanged.Clear();
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            if (!HasRecipeWithInput(player.KitchenObject.Asset)) return;
            player.KitchenObject.SetKitchenObjectParent(this);

            FryingRecipeAsset = GetFryingRecipeByInput(KitchenObject.Asset);
            StoveStateMachine.Switch<StoveFryingState>();
            FryingTimer = 0f;
            
            OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = FryingTimer / FryingRecipeAsset.FryingTimerMax });
        }
        else if (HasKitchenObject() && !player.HasKitchenObject()) {
            KitchenObject.SetKitchenObjectParent(player);
            StoveStateMachine.Switch<StoveIdleState>();
            OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = 0f });
        }
        else if (HasKitchenObject() && player.HasKitchenObject()) {
            Debug.LogWarning("CANT PLACE THAT OBJECT HERE");
        }
    }

    private bool HasRecipeWithInput(KitchenObjectAsset input) {
        FryingRecipeAsset recipeAsset = GetFryingRecipeByInput(input);
        return recipeAsset != null;
    }

    private KitchenObjectAsset GetOutputByInput(KitchenObjectAsset input) {
        FryingRecipeAsset recipeAsset = GetFryingRecipeByInput(input);
        if (recipeAsset != null) {
            return recipeAsset.Output;
        }
        else {
            return null;
        }
    }

    private FryingRecipeAsset GetFryingRecipeByInput(KitchenObjectAsset asset) {
        if (asset == null) return null;

        foreach (FryingRecipeAsset recipe in fryingRecipes) {
            if (recipe.Input == asset) {
                return recipe;
            }
        }

        return null;
    }

    internal BurningRecipeAsset GetBurningRecipeByInput(KitchenObjectAsset asset) {
        if (asset == null) return null;

        foreach (BurningRecipeAsset recipe in burningRecipes) {
            if (recipe.Input == asset) {
                return recipe;
            }
        }

        return null;
    }
}


public class StoveIdleState : BaseState<StoveCounter> {
}

public class StoveFryingState : BaseState<StoveCounter> {
    public override void OnUpdate() {
        Blackboard.FryingTimer += Time.deltaTime;
        Blackboard.OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = Blackboard.FryingTimer / Blackboard.FryingRecipeAsset.FryingTimerMax });
        
        if (Blackboard.FryingTimer >= Blackboard.FryingRecipeAsset.FryingTimerMax) {
            Blackboard.FryingTimer = 0;
            Blackboard.KitchenObject.DestorySelf();
            KitchenObject.SpawnKitchenObject(Blackboard.FryingRecipeAsset.Output, Blackboard);

            StateMachine.Switch<StoveFriedState>();
            Blackboard.BurningTimer = 0f;
            Blackboard.BurningRecipeAsset = Blackboard.GetBurningRecipeByInput(Blackboard.KitchenObject.Asset);
        }
    }
}

public class StoveFriedState : BaseState<StoveCounter> {
    public override void OnUpdate() {
        Blackboard.BurningTimer += Time.deltaTime;
        Blackboard.OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = Blackboard.BurningTimer / Blackboard.BurningRecipeAsset.BurningTimerMax });
        
        if (Blackboard.BurningTimer >= Blackboard.BurningRecipeAsset.BurningTimerMax) {
            Blackboard.BurningTimer = 0;
            Blackboard.KitchenObject.DestorySelf();
            KitchenObject.SpawnKitchenObject(Blackboard.BurningRecipeAsset.Output, Blackboard);

            StateMachine.Switch<StoveBurnedState>();
            Blackboard.BurningTimer = 0f;
            Blackboard.OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = .0f });
        }
    }
}

public class StoveBurnedState : BaseState<StoveCounter> {
    public override void OnEnter() {
        Debug.Log("Burned object");
    }
}