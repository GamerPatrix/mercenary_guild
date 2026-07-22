using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace mercenary_guild
{
    public class BottomCombatMenuUI : MonoBehaviour
    {
       

        [SerializeField] GameObject grid;
        [SerializeField] private ClickableButtonItem itemTemplate;
        [SerializeField] private GameObject backButtonsContainer;
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

            // Show/hide back button
            if (backButtonsContainer != null)
            {
                backButtonsContainer.SetActive(showBackButton);
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

        private void OnItemClick(int id)
        {
            onItemClickCallback?.Invoke(id);
        }

        public void OnBackButtonClick()
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