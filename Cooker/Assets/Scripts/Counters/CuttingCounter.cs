using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using KitchenObjects;
using UnityEngine;

namespace Counters {
    public class CuttingCounter : BaseCounter, IHasProgress {
        [SerializeField] private CuttingRecipeAsset[] recipesArray;
        [SerializeField] private float cancelCutDistance = 1.5f;
        public readonly Signal OnCutting = new Signal();
        public Signal<IHasProgress.ProgressNormalize> OnProgressChanged { get; } = new();
        
        private float cuttingDuration;
        private float cuttingTimer;
        private Coroutine cuttingCoroutine;

        public void OnDestroy() {
            OnProgressChanged.Clear();
        }

        public override void Interact(Player player) {
            if (!HasKitchenObject() && player.HasKitchenObject()) {
                if (!HasRecipeWithInput(player.KitchenObject.Asset)) return;
                player.KitchenObject.SetKitchenObjectParent(this);
                ResetCuttingState();

                CuttingRecipeAsset recipe = GetCuttingRecipeByInput(KitchenObject.Asset);
                OnProgressChanged?.Invoke(
                    new() { ProgressNormalizeFloat = cuttingDuration / recipe.CuttingProgressMax });
            }
            else if (HasKitchenObject() && !player.HasKitchenObject()) {
                KitchenObject.SetKitchenObjectParent(player);
            }
            else if (HasKitchenObject() && player.HasKitchenObject()) {
                Debug.LogWarning("CANT PLACE THAT OBJECT HERE");
            }
        }

        public override void InteractAlt(Player player) {
            /*if (!HasKitchenObject() || !HasRecipeWithInput(KitchenObject.Asset)) return;
            cuttingProgress++;

            CuttingRecipeAsset recipe = GetCuttingRecipeByInput(KitchenObject.Asset);
            if (cuttingProgress < recipe.cuttingProgressMax) return;

            KitchenObjectAsset output = GetOutputByInput(KitchenObject.Asset);
            KitchenObject.DestorySelf();
            KitchenObject.SpawnKitchenObject(output, this);*/
        }

        public override void InteractAltHold(Player player) {
            if (!HasKitchenObject() || !HasRecipeWithInput(KitchenObject.Asset)) return;

            if (cuttingCoroutine != null) return;
            CuttingRecipeAsset recipe = GetCuttingRecipeByInput(KitchenObject.Asset);
            cuttingCoroutine = StartCoroutine(CuttingProgressCoroutine(recipe.CuttingProgressMax, player));
        }

        public override void InteractAltHoldCancel(Player player) {
            ResetCuttingState();

            if (cuttingCoroutine == null) return;
            StopCoroutine(cuttingCoroutine);
            cuttingCoroutine = null;
        }

        private IEnumerator CuttingProgressCoroutine(float duration, Player player) {
            cuttingDuration = duration;
            cuttingTimer = 0f;

            CuttingRecipeAsset recipe = GetCuttingRecipeByInput(KitchenObject.Asset);

            int lastInt = -1;
            while (cuttingTimer < cuttingDuration) {
                cuttingTimer += Time.deltaTime;

                if (Vector3.Distance(player.transform.position, transform.position) > cancelCutDistance) {
                    CancelCutting();
                    yield break;
                }

                int currentWhole = Mathf.FloorToInt(cuttingTimer * 2f);
                if (currentWhole != lastInt) {
                    lastInt = currentWhole;
                    float stepValue = currentWhole * 0.5f;
                    Debug.Log($"Passed whole unit: {stepValue}"); // or call a method/event
                    OnCutting?.Invoke();
                }

                OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = cuttingTimer / recipe.CuttingProgressMax });

                yield return null;
            }

            OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = 0f });

            KitchenObjectAsset output = GetOutputByInput(KitchenObject.Asset);
            KitchenObject.DestorySelf();
            KitchenObject.SpawnKitchenObject(output, this);

            ResetCuttingState();
            cuttingCoroutine = null;
        }

        private void CancelCutting() {
            if (cuttingCoroutine != null) {
                StopCoroutine(cuttingCoroutine);
                cuttingCoroutine = null;
            }

            ResetCuttingState();
            OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = 0f });
        }

        private void ResetCuttingState() {
            if (KitchenObject != null && KitchenObject.Asset != null) {
                CuttingRecipeAsset recipe = GetCuttingRecipeByInput(KitchenObject.Asset);
                if (recipe != null) {
                    OnProgressChanged?.Invoke(new() { ProgressNormalizeFloat = 0f });
                }
            }

            cuttingTimer = 0f;
            cuttingDuration = 0f;
        }

        private bool HasRecipeWithInput(KitchenObjectAsset input) {
            CuttingRecipeAsset recipeAsset = GetCuttingRecipeByInput(input);
            return recipeAsset != null;
        }

        private KitchenObjectAsset GetOutputByInput(KitchenObjectAsset input) {
            CuttingRecipeAsset recipeAsset = GetCuttingRecipeByInput(input);
            if (recipeAsset != null) {
                return recipeAsset.Output;
            }
            else {
                return null;
            }
        }

        private CuttingRecipeAsset GetCuttingRecipeByInput(KitchenObjectAsset asset) {
            if (asset == null) return null;

            foreach (CuttingRecipeAsset recipe in recipesArray) {
                if (recipe.Input == asset) {
                    return recipe;
                }
            }

            return null;
        }
    }
}