using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class LocaleDropdownController : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }
    private IEnumerator Start()
    {
        // 1. Wait for the Localization System to initialize its tables and locales
        yield return LocalizationSettings.InitializationOperation;

        // 2. Clear any placeholder options currently in the dropdown
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentLocaleIndex = 0;

        var availableLocales = LocalizationSettings.AvailableLocales.Locales;

        // 3. Loop through all available languages and grab their native names
        for (int i = 0; i < availableLocales.Count; i++)
        {
            var locale = availableLocales[i];

            // This gets the readable name (e.g., "English" instead of just "en")
            options.Add(locale.Identifier.CultureInfo != null ?
                locale.Identifier.CultureInfo.NativeName :
                locale.name);

            // Track which index matches our current active language
            if (LocalizationSettings.SelectedLocale == locale)
            {
                currentLocaleIndex = i;
            }
        }

        // 4. Feed the language list into the dropdown and set the active one
        dropdown.AddOptions(options);
        dropdown.value = currentLocaleIndex;

        // 5. Listen for when the user clicks a new option
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        // Change the active locale to the one selected by the user
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    private void OnDestroy()
    {
        // Clean up our listener when the object is destroyed to prevent memory leaks
        if (dropdown != null)
        {
            dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }
    }
}