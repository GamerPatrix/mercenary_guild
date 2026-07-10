using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace mercenary_guild { 
    public class ClickabelItemInBotomCombatMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Button button;
        private LocalizedString localizedString;
        private int id;
        private BottomCombatMenuUI bottomCombatMenuUI;

        public void Initialize(LocalizedString localizedString, int id, BottomCombatMenuUI bottomCombatMenuUI)
        {
            this.localizedString = localizedString;
            this.id = id;
            this.bottomCombatMenuUI = bottomCombatMenuUI;

            // Setup button listener
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonPressed());
            }
            UpdateDisplay();
        }

        private void OnButtonPressed()
        {
            bottomCombatMenuUI.OnItemClick(id);
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
            UpdateDisplay();
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        private void OnLocaleChanged(Locale locale)
        {
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            nameText.text = localizedString.GetLocalizedString();
        }
    }
}