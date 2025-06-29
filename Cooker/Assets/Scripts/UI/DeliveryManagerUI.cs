using System;
using TreeEditor;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeDelivered.AddListener(OnRecipeDelivered);
        DeliveryManager.Instance.OnRecipeSpawned.AddListener(UpdateVisual);
        LanguageManager.Instance.OnLanguageChanged.AddListener(UpdateVisualWithLanguage);
    }

    private void OnRecipeDelivered(RecipeAsset recipe) => UpdateVisual();

    private void UpdateVisualWithLanguage(string language) {
        Debug.Log($"Updating visual with language: {language}");
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeAsset recipeAsset in DeliveryManager.Instance.WaitingRecipes) {
            Transform recipe = Instantiate(recipeTemplate, container);
            recipe.gameObject.SetActive(true);
            recipe.GetComponent<DeliveryManagerTemplateUI>().SetRecipeAsset(recipeAsset);
        }
    }
}