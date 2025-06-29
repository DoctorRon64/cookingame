using System;
using KitchenObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerTemplateUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI deliveryText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeAsset(RecipeAsset recipeAsset) {
        deliveryText.text = recipeAsset.LocalizedName;

        foreach (Transform child in iconContainer) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectAsset asset in recipeAsset.KitchenObjAssets) {
            Transform instantiate = Instantiate(iconTemplate, iconContainer);
            instantiate.gameObject.SetActive(true);
            instantiate.GetComponent<DeliveryManagerTemplateIconUI>().Icon.sprite = asset.Icon;
        }
    }
}