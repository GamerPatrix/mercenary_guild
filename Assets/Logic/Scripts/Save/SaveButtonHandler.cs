using UnityEngine;

namespace mercenary_guild
{
    public class SaveButtonHandler : MonoBehaviour
    {
        [SerializeField] private int saveSlotIndex = 0;
        [SerializeField] private string saveName = "New Save";

        public void OnClickCreateSave()
        {
            if (SaveDataManager.instance != null)
            {
                SaveDataManager.instance.CreateNewSave(saveSlotIndex, saveName);
                Debug.Log($"Save created in slot {saveSlotIndex} with name: {saveName}");
            }
            else
            {
                Debug.LogError("SaveDataManager not found in scene!");
            }
        }
    }
}
