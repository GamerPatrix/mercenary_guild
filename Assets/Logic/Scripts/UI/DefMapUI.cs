using UnityEngine;
using UnityEngine.UI;

public class DefMapUIManager : MonoBehaviour
{
    
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject inventoryUI;


    void Awake()
    {
        mainUI.GetComponent<MapMainUI>().SetDefMapUIManager(this);
        inventoryUI.GetComponent<InventoryUIManager>().SetDefMapUIManager(this);
    }

    private void start()
    {
        if (mainUI != null)
        {
            mainUI.SetActive(true);
        }
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }
    }

    public void GoToInventory()
    {
        if (mainUI != null)
        {
            mainUI.SetActive(false);
        }

        if (inventoryUI != null)
        {
            inventoryUI.SetActive(true);
        }
    }

    public void GoToMain()
    {
        if (mainUI != null)
        {
            mainUI.SetActive(true);
        }

        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }
    }
}

