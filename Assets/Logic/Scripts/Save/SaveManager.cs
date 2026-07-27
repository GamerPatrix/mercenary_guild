using System;
using System.Collections.Generic;
using UnityEngine;

namespace mercenary_guild
{
    public static class SaveManager
    {
        private const string SAVE_KEY_PREFIX = "mercenary_guild_player_save_";

        public static void SavePlayerData(PlayerManager playerManager, Inventory inventory, int slotIndex)
        {
            SaveData saveData = new SaveData
            {
                CharacterName = playerManager.CharacterName,
                MaxHealth = playerManager.MaxHealth,
                CurrentHealth = playerManager.CurrentHealth,
                PhysicalResistance = playerManager.PhysicalResistance,
                MagicResistance = playerManager.MagicResistance
            };

            // Get inventory items count and info
            // For now, we'll just save the item count
            saveData.InventoryItems = new List<string>();

            // Serialize and save
            string saveString = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(GetSaveKey(slotIndex), saveString);
            PlayerPrefs.Save();
        }

        public static SaveData LoadPlayerData(int slotIndex)
        {
            string saveKey = GetSaveKey(slotIndex);
            if (PlayerPrefs.HasKey(saveKey))
            {
                string saveString = PlayerPrefs.GetString(saveKey);
                SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);
                return saveData;
            }

            return null;
        }

        public static bool HasSavedData(int slotIndex)
        {
            return PlayerPrefs.HasKey(GetSaveKey(slotIndex));
        }

        public static void DeleteSave(int slotIndex)
        {
            PlayerPrefs.DeleteKey(GetSaveKey(slotIndex));
            PlayerPrefs.Save();
        }

        public static void SetSaveName(int slotIndex, string name)
        {
            // For now, we'll just store the save name in PlayerPrefs
            // In a more complex implementation, this could be part of the save data
            PlayerPrefs.SetString($"mercenary_guild_save_name_{slotIndex}", name);
            PlayerPrefs.Save();
        }

        private static string GetSaveKey(int slotIndex)
        {
            return $"{SAVE_KEY_PREFIX}{slotIndex}";
        }
    }

    [System.Serializable]
    public class SaveMetadata
    {
        public string saveName;
        public long timestamp;
        public int slotIndex;
        public string characterName;
    }
}
