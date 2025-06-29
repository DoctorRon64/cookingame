using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : SingletonMono<LanguageManager> {
    
    public readonly Signal<string> OnLanguageChanged = new();
    
    [ContextMenu(" set spanish languages")]
    public void SetSpanish() {
        ChangeLanguage("es");
    }
    
    [ContextMenu(" set english languages")]
    public void SetEnglish() {
       ChangeLanguage("en");
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