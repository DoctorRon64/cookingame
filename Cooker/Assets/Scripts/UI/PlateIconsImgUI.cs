using KitchenObjects;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsImgUI : MonoBehaviour {
    [SerializeField] private Image img;

    public void SetKitchenObjectAsset(KitchenObjectAsset asset) {
        img.sprite = asset.Icon;
    }
}