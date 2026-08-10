using UnityEngine;
using UnityEngine.UI;
public class MapMainUI : MonoBehaviour
{
    [SerializeField] private Button button;
    DefMapUIManager defMapUIManager;

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(HandleButtonClick);
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(HandleButtonClick);
        }
    }

    private void HandleButtonClick()
    {
        if (defMapUIManager != null)
        {
            defMapUIManager.GoToInventory();
        }
    }

    public void SetDefMapUIManager(DefMapUIManager manager)
    {
        defMapUIManager = manager;
    }
}
