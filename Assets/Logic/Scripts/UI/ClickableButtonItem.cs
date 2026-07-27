using UnityEngine;
using UnityEngine.Localization;
using TMPro;
using UnityEngine.Localization.Settings;
namespace mercenary_guild
{
    public class ClickableButtonItem : MonoBehaviour
    {
        private LocalizedString localizedString;
        private int id;
        private BottomCombatMenuUI parent;
        private System.Action<int> onClickCallback;
        [SerializeField] private TextMeshProUGUI nameText;


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

        public void Initialize(LocalizedString localizedString, int id, BottomCombatMenuUI parent, System.Action<int> onClickCallback)
        {
            this.localizedString = localizedString;
            this.id = id;
            this.parent = parent;
            this.onClickCallback = onClickCallback;
            UpdateDisplay();
        }

        public void OnItemClick()
        {
            onClickCallback?.Invoke(id);
        }
    }
}