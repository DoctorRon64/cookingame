using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class ChangeTextByLanguageUI : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private string localizedKey;

    private void Start() {
        LanguageManager.Instance.OnLanguageChanged.AddListener(UpdateLanguageText);
    }

    private void UpdateLanguageText(string languageType) {
        countdownText.text = new LocalizedString("UIText", localizedKey).GetLocalizedString();  
    }
}
