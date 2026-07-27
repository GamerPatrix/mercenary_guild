using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using TMPro;
using UnityEngine.Localization.Settings;
using System.Reflection;
namespace mercenary_guild
{
    public class ClickableButtonItem : MonoBehaviour
    {
        private LocalizedString localizedString;
        private int id;
        private BottomCombatMenuUI parent;
        private System.Action<int> onClickCallback;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image image;
        [SerializeField] private OnHoverEnable bottomLineScript;
        [SerializeField] private bool BottomLineOnHower = true;

        //todo i dont need the localeChanged event when I have only an image
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
            if (bottomLineScript != null)
                bottomLineScript.enabled = BottomLineOnHower;
        }

        public void Initialize(LocalizedString localizedString, int id, BottomCombatMenuUI parent, System.Action<int> onClickCallback)
        {
            this.localizedString = localizedString;
            this.id = id;
            this.parent = parent;
            this.onClickCallback = onClickCallback;
            UpdateDisplay();
        }

        public void Initialize(Sprite sprite, int id, BottomCombatMenuUI parent, System.Action<int> onClickCallback)
        {
            image.sprite = sprite;
            this.id = id;
            this.parent = parent;
            this.onClickCallback = onClickCallback;
            UpdateDisplay();
        }

        public void Initialize(LocalizedString localizedString, Sprite sprite, int id, BottomCombatMenuUI parent, System.Action<int> onClickCallback)
        {
            this.localizedString = localizedString;
            image.sprite = sprite;
            this.id = id;
            this.parent = parent;
            this.onClickCallback = onClickCallback;
            UpdateDisplay();
        }

        public void Initialize(string newString, int id, BottomCombatMenuUI parent, System.Action<int> onClickCallback)
        {
            nameText.text = newString;
            this.id = id;
            this.parent = parent;
            this.onClickCallback = onClickCallback;
        }



        public void OnItemClick()
        {
            onClickCallback?.Invoke(id);
        }
    }
}