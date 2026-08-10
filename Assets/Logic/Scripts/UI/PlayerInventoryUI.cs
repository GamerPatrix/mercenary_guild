using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using mercenary_guild;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private GameObject itemTemplate;

    private List<GameObject> itemUIObjects = new List<GameObject>();

    public void DisplayItems()
    {
        foreach (GameObject obj in itemUIObjects)
        {
            Destroy(obj);
        }
        itemUIObjects.Clear();

        List<Inventory.ItemCounted> items = PlayerManager.instance.inventory.Items;

        // Create UI for each item
        foreach (Inventory.ItemCounted itemCounted in items)
        {

            GameObject itemObject = Instantiate(itemTemplate, itemsContainer);
            itemObject.SetActive(true); 
            ItemUI itemUI = itemObject.GetComponent<ItemUI>();
            if (itemUI != null)
            {
                itemUI.SetItem(itemCounted.ItemSO, itemCounted.Actualcount);
            }
            else
            {
                Debug.LogError("ItemUI component not found on template!");
            }

            itemUIObjects.Add(itemObject);
        }
    }
}
