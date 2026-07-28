using UnityEngine;

namespace mercenary_guild
{
    public class SaveTest : MonoBehaviour
    {
        public void TestSave(int slotIndex = 0)
        {
            SaveManager.SavePlayerData(PlayerManager.instance, PlayerManager.instance.inventory, slotIndex);
            Debug.Log("Test save created in slot " + slotIndex);
        }

        public void TestLoad(int slotIndex = 0)
        {
            PlayerManager.instance.LoadPlayerData(slotIndex);
            Debug.Log("Test load from slot " + slotIndex);
        }
    }
}
