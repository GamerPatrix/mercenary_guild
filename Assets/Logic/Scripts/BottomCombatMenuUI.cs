using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace mercenary_guild
{
    public class BottomCombatMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject grid;
    [SerializeField] private ClickableButtonItem itemTemplate;
    [SerializeField] private LocalizedString backButtonText;
    private ClickableButtonItem backButton;
    private BottomCombatMenuUIManager manager;

    private List<ClickableButtonItem> activeItems = new List<ClickableButtonItem>();
        private List<LocalizedString> currentMenuItems = new List<LocalizedString>();
        private System.Action<int> onItemClickCallback;

        public void Initialize(BottomCombatMenuUIManager manager)
        {
            this.manager = manager;
        }

        public void UpdateDisplay(List<LocalizedString> localizedStrings, System.Action<int> onItemClick = null, bool showBackButton = false)
        {
            // Clear existing items
            foreach (var item in activeItems)
            {
                Destroy(item.gameObject);
            }
            activeItems.Clear();
            currentMenuItems = localizedStrings;
            onItemClickCallback = onItemClick;

            // Clear back button if not needed
            if (backButton != null && !showBackButton)
            {
                Destroy(backButton.gameObject);
                backButton = null;
            }

            // Show/hide back button
            if (backButton != null)
            {
                backButton.gameObject.SetActive(showBackButton);
            }
            
            // Create back button if needed
            if (showBackButton && backButton == null)
            {
                CreateBackButton();
            }

            // Create new items from the list
            for (int i = 0; i < localizedStrings.Count; i++)
            {
                var localizedString = localizedStrings[i];
                var itemObject = Instantiate(itemTemplate.gameObject, grid.transform);
                itemObject.SetActive(true);
                var itemComponent = itemObject.GetComponent<ClickableButtonItem>();

                if (itemComponent != null)
                {
                    itemComponent.Initialize(localizedString, i, this, OnItemClick);
                    activeItems.Add(itemComponent);
                }
            }

            
        }

        private void CreateBackButton()
        {
            if (itemTemplate == null)
            {
                Debug.LogError("itemTemplate is null in BottomCombatMenuUI - cannot create back button");
                return;
            }
            if (grid == null)
            {
                Debug.LogError("grid is null in BottomCombatMenuUI - cannot create back button");
                return;
            }

            var itemObject = Instantiate(itemTemplate.gameObject, grid.transform);
            itemObject.SetActive(true);
            backButton = itemObject.GetComponent<ClickableButtonItem>();
            if (backButton != null)
            {
                backButton.Initialize(backButtonText, -1, this, OnBackButtonClick);
                Debug.Log("Back button created successfully");
            }
            else
            {
                Debug.LogError("Failed to get ClickableButtonItem component from instantiated object");
            }
        }

        private void OnItemClick(int id)
        {
            onItemClickCallback?.Invoke(id);
        }

        private void OnBackButtonClick(int id)
        {
            manager.OnBackButtonClick();
        }

        public List<LocalizedString> GetCurrentMenuItems()
        {
            return currentMenuItems;
        }

        public enum MenuMode
        {
            Main,
            Attacks,
            Potions
        }
    }

}