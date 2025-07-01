using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoSingleton<LanguageManager> {
    public enum SupportedLanguage
    {
        English,
        Spanish,
        Dutch
    }
    
    public readonly Signal<string> OnLanguageChanged = new();
    
    [SerializeField] private SupportedLanguage selectedLanguage;
    
    private void OnValidate()
    {
        switch (selectedLanguage)
        {
            case SupportedLanguage.English:
                ChangeLanguage("en");
                break;
            case SupportedLanguage.Spanish:
                ChangeLanguage("es");
                break;
            case SupportedLanguage.Dutch:
                ChangeLanguage("nl");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void ChangeLanguage(string languageCode)
    {
        Locale locale = LocalizationSettings.AvailableLocales.Locales.Find(
            locale => locale.Identifier.Code == languageCode);

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            OnLanguageChanged?.Invoke(languageCode);
        }
        else
        {
            Debug.LogWarning($"Locale with code '{languageCode}' not found.");
        }
    }
}