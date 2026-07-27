using UnityEngine;
using System.Collections.Generic;
using System;

namespace mercenary_guild
{
    public class SaveSlotUI : MonoBehaviour
    {
        [SerializeField] private Transform grid;
        [SerializeField] private SaveSlotButton buttonTemplate;
        [SerializeField] private SaveDataManager saveDataManager;

        private List<SaveSlotButton> activeButtons = new List<SaveSlotButton>();
        private Action<SaveMetadata> onSaveSlotClick;

        private void Start()
        {
            LoadSaveSlots();
        }

        public void Initialize(Action<SaveMetadata> onSaveSlotClick)
        {
            this.onSaveSlotClick = onSaveSlotClick;
            LoadSaveSlots();
        }

        public void LoadSaveSlots()
        {
            // Clear existing buttons
            foreach (var button in activeButtons)
            {
                Destroy(button.gameObject);
            }
            activeButtons.Clear();

            if (saveDataManager == null)
            {
                Debug.LogError("SaveDataManager reference is missing");
                return;
            }

            List<SaveMetadata> saveMetadataList = saveDataManager.GetSaveMetadataList();

            for (int i = 0; i < saveMetadataList.Count; i++)
            {
                var metadata = saveMetadataList[i];
                var buttonObject = Instantiate(buttonTemplate.gameObject, grid);
                buttonObject.SetActive(true);
                var buttonComponent = buttonObject.GetComponent<SaveSlotButton>();

                if (buttonComponent != null)
                {
                    buttonComponent.Initialize(metadata.saveName, metadata.slotIndex, this, OnButtonClick);
                    activeButtons.Add(buttonComponent);
                }
            }

            int maxSlots = saveDataManager.MaxSaveSlots;
            for (int i = saveMetadataList.Count; i < maxSlots; i++)
            {
                var buttonObject = Instantiate(buttonTemplate.gameObject, grid);
                buttonObject.SetActive(true);
                var buttonComponent = buttonObject.GetComponent<SaveSlotButton>();

                if (buttonComponent != null)
                {
                    buttonComponent.Initialize("Empty Slot", i, this, OnButtonClick);
                    activeButtons.Add(buttonComponent);
                }
            }
        }

        private void OnButtonClick(int slotIndex)
        {
            if (saveDataManager != null)
            {
                var metadataList = saveDataManager.GetSaveMetadataList();
                var metadata = metadataList.Find(m => m.slotIndex == slotIndex);
                onSaveSlotClick?.Invoke(metadata ?? new SaveMetadata { slotIndex = slotIndex });
            }
        }
    }
}