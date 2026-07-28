using System;
using System.Collections.Generic;
using UnityEngine;
using mercenary_guild.sos;
using mercenary_guild;

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

            // Save inventory items - use item names for serialization
            if (inventory != null)
            {
                List<Inventory.ItemCounted> inventoryItems = inventory.GetAllItemCounted();
                if (inventoryItems != null)
                {
                    saveData.InventoryItemNames = new List<SaveData.ItemData>();
                    foreach (var itemCounted in inventoryItems)
                    {
                        if (itemCounted.itemSO != null)
                        {
                            // Save the item name (LocalizedString.Value) and count
                            SaveData.ItemData itemData = new SaveData.ItemData
                            {
                                itemName = itemCounted.itemSO.ItemName,
                                count = itemCounted.Actualcount
                            };
                            saveData.InventoryItemNames.Add(itemData);
                        }
                    }
                }
            }

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

        public static void LoadInventoryItems(SaveData saveData, Inventory inventory)
        {
            if (saveData == null || inventory == null || saveData.InventoryItemNames == null)
                return;

            // Clear current inventory and rebuild from starting gear
            inventory.RebuildFromStartingGear();

            // Load each item from the saved data
            foreach (var itemData in saveData.InventoryItemNames)
            {
                // Look for the item in Resources/Items folder
                ItemSO item = Resources.Load<ItemSO>($"Items/{itemData.itemName}");
                if (item != null)
                {
                    // Add the item with its saved count
                    inventory.Add(item, itemData.count);
                    Debug.Log($"Loaded inventory item: {itemData.itemName} (count: {itemData.count})");
                }
                else
                {
                    Debug.LogWarning($"Failed to load inventory item: '{itemData.itemName}' - item not found in Resources/Items folder");
                }
            }
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
