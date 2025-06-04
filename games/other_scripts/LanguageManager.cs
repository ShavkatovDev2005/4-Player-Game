using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System;

public enum LanguageType
{
    English,
    Turkish,
    Russian,
    Uzbek,
    Chinese,//Chinese (Simplified) (zh-CN)
    Spanish//Spanish (Spain) (es-ES)
}

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private Button languageButton;
    private int currentLanguageIndex; // Current language index

    void Awake()
    {
        // Load language index from PlayerPrefs
        if (PlayerPrefs.HasKey("LanguageIndex"))
        {
            currentLanguageIndex = PlayerPrefs.GetInt("LanguageIndex");
            // Setting the language when the application is launched
            LanguageType savedLanguage = (LanguageType)Enum.GetValues(typeof(LanguageType)).GetValue(currentLanguageIndex);
            setLanguage(savedLanguage);
        }

        languageButton.onClick.AddListener(changeLanguage);
    }

    void changeLanguage()
    {
        // Calculating the next language index
        currentLanguageIndex = (currentLanguageIndex + 1) % Enum.GetValues(typeof(LanguageType)).Length;

        // Take the next language
        LanguageType nextLanguage = (LanguageType)Enum.GetValues(typeof(LanguageType)).GetValue(currentLanguageIndex);
        // Change the language
        setLanguage(nextLanguage);

        // Save language index to PlayerPrefs
        PlayerPrefs.SetInt("LanguageIndex", currentLanguageIndex);
        PlayerPrefs.Save();
    }

    public void setLanguage(LanguageType languageType)
    {
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.LocaleName.Equals(languageType.ToString()))
            {
                LocalizationSettings.SelectedLocale = locale;
                return;
            }
        }
        Debug.LogError($"Locale for {languageType} not found.");
    }
}