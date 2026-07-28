using System;
using System.Collections.Generic;
using UnityEngine;

namespace mercenary_guild
{


    public class SaveDataManager : MonoBehaviour
    {
        public static SaveDataManager instance { get; private set; }

        [SerializeField] private int maxSaveSlots = 3;
        [SerializeField] private string defaultSaveName = "New Save";

        public int MaxSaveSlots => maxSaveSlots;

        private List<SaveMetadata> saveMetadataList;
        private int currentSaveSlot = -1;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSaveData();
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void InitializeSaveData()
        {
            saveMetadataList = new List<SaveMetadata>();
            for (int i = 0; i < maxSaveSlots; i++)
            {
                if (SaveManager.HasSavedData(i))
                {
                    SaveData saveData = SaveManager.LoadPlayerData(i);
                    if (saveData != null)
                    {
                        SaveMetadata metadata = new SaveMetadata
                        {
                            slotIndex = i,
                            saveName = !string.IsNullOrEmpty(saveData.CharacterName) ? saveData.CharacterName : defaultSaveName,
                            timestamp = DateTime.Now.Ticks,
                            characterName = saveData.CharacterName
                        };
                        saveMetadataList.Add(metadata);
                    }
                }
            }
        }

        public List<SaveMetadata> GetSaveMetadataList()
        {
            return saveMetadataList;
        }

        public void SetCurrentSaveSlot(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < maxSaveSlots && SaveManager.HasSavedData(slotIndex))
            {
                currentSaveSlot = slotIndex;
                ApplySaveData(slotIndex);
            }
        }

        public void CreateNewSave(int slotIndex, string saveName)
        {
            if (slotIndex >= 0 && slotIndex < maxSaveSlots)
            {
                SaveManager.SavePlayerData(PlayerManager.instance, PlayerManager.instance.inventory, slotIndex);

                SaveData saveData = SaveManager.LoadPlayerData(slotIndex);
                SaveMetadata metadata = new SaveMetadata
                {
                    slotIndex = slotIndex,
                    saveName = !string.IsNullOrEmpty(saveName) ? saveName : defaultSaveName,
                    timestamp = DateTime.Now.Ticks,
                    characterName = saveData?.CharacterName ?? "Unknown"
                };

                // Update or add metadata
                int existingIndex = saveMetadataList.FindIndex(m => m.slotIndex == slotIndex);
                if (existingIndex >= 0)
                {
                    saveMetadataList[existingIndex] = metadata;
                }
                else
                {
                    saveMetadataList.Add(metadata);
                }
            }
        }

        public void DeleteSave(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < maxSaveSlots)
            {
                SaveManager.DeleteSave(slotIndex);

                int existingIndex = saveMetadataList.FindIndex(m => m.slotIndex == slotIndex);
                if (existingIndex >= 0)
                {
                    saveMetadataList.RemoveAt(existingIndex);
                }

                if (currentSaveSlot == slotIndex)
                {
                    currentSaveSlot = -1;
                }
            }
        }

        public void SetSaveName(int slotIndex, string newName)
        {
            if (slotIndex >= 0 && slotIndex < maxSaveSlots)
            {
                SaveManager.SetSaveName(slotIndex, newName);

                int existingIndex = saveMetadataList.FindIndex(m => m.slotIndex == slotIndex);
                if (existingIndex >= 0)
                {
                    saveMetadataList[existingIndex].saveName = newName;
                }
            }
        }

        private void ApplySaveData(int slotIndex)
        {
            SaveData saveData = SaveManager.LoadPlayerData(slotIndex);
            if (saveData == null) return;

            PlayerManager.instance.CharacterName = saveData.CharacterName;
            PlayerManager.instance.MaxHealth = saveData.MaxHealth;
            PlayerManager.instance.CurrentHealth = saveData.CurrentHealth;
            PlayerManager.instance.PhysicalResistance = saveData.PhysicalResistance;
            PlayerManager.instance.MagicResistance = saveData.MagicResistance;

            // Load inventory items
            SaveManager.LoadInventoryItems(saveData, PlayerManager.instance.inventory);
        }
        
    }
}

