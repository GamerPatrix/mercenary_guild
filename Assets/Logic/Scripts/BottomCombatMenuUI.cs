using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace mercenary_guild
{
    public class BottomCombatMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject grid;
        [SerializeField] private ClickabelItemInBotomCombatMenu itemTemplate;
        private BottomCombatMenuUIManager manager;

        private List<ClickabelItemInBotomCombatMenu> activeItems = new List<ClickabelItemInBotomCombatMenu>();

        public void Initialize(BottomCombatMenuUIManager manager)
        {
            this.manager = manager;
        }

        public void UpdateDisplay(List<LocalizedString> localizedStrings)
        {
            // Clear existing items
            foreach (var item in activeItems)
            {
                Destroy(item.gameObject);
            }
            activeItems.Clear();

            // Create new items from the list
            for (int i = 0; i < localizedStrings.Count; i++)
            {
                var localizedString = localizedStrings[i];
                var itemObject = Instantiate(itemTemplate.gameObject, grid.transform);
                itemObject.SetActive(true);
                var itemComponent = itemObject.GetComponent<ClickabelItemInBotomCombatMenu>();

                if (itemComponent != null)
                {
                    itemComponent.Initialize(localizedString, i, this);
                    activeItems.Add(itemComponent);
                }
            }
        }

        public void OnItemClick(int id)
        {
            // Handle item click with the provided ID
            Debug.Log($"Item with ID {id} was pressed");
        }
    }
}