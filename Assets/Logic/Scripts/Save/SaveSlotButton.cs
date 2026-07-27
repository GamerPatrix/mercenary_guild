using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace mercenary_guild
{
//todo merge with ClickableButtonItem
    public class SaveSlotButton : MonoBehaviour
    {
        private string buttonText;
        private int slotIndex;
        private SaveSlotUI parent;
        private System.Action<int> onClickCallback;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private OnHoverEnable bottomLineScript;
        [SerializeField] private bool bottomLineOnHover = true;
        [SerializeField]
        private Button button;
       private void Awake()
        {
            
            if (button != null)
            {
                button.onClick.AddListener(HandleButtonClick);
            }
        }

        private void OnDestroy()
        {
            Button button = GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveListener(HandleButtonClick);
            }
        }

        public void Initialize(string text, int slotIndex, SaveSlotUI parent, System.Action<int> onClickCallback)
        {
            this.buttonText = text;
            this.slotIndex = slotIndex;
            this.parent = parent;
            this.onClickCallback = onClickCallback;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (nameText != null)
            {
                nameText.text = buttonText;
            }
            if (bottomLineScript != null)
            {
                bottomLineScript.enabled = bottomLineOnHover;
            }
        }

        public void HandleButtonClick()
        {
            onClickCallback?.Invoke(slotIndex);
        }
    }
}