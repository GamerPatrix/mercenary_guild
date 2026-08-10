using UnityEngine;
using UnityEngine.UI;
public class InventoryUIManager : MonoBehaviour
{
   [SerializeField] private Button BackButton;
    DefMapUIManager defMapUIManager;

    public void SetDefMapUIManager(DefMapUIManager manager)
    {
        defMapUIManager = manager;
    }

    private void Start()
    {
        if (BackButton != null)
        {
            BackButton.onClick.AddListener(HandleBackButtonClick);
        }
    }   

    private void OnDestroy()
    {
        if (BackButton != null)
        {
            BackButton.onClick.RemoveListener(HandleBackButtonClick);
        }
    }

    private void HandleBackButtonClick()
    {
        if (defMapUIManager != null)
        {
            defMapUIManager.GoToMain();
        }
    }

    [SerializeField] private PlayerInventoryUI playerInventoryUI;

    public void DisplayInventoryItems()
    {
        if (playerInventoryUI != null)
        {
            playerInventoryUI.DisplayItems();
        }
    }

}
