using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace mercenary_guild
{
    public class EnemyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private CombatCharacter comCharacter;


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
            nameText.text = comCharacter.GetDisplayName();       
        }
    }
}
