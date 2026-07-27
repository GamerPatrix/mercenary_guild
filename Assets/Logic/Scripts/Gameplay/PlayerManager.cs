using System;
using System.Collections.Generic;
using UnityEngine;

namespace mercenary_guild
{
    public class PlayerManager : MonoBehaviour
    {
        public event EventHandler OnPlayerDeath;
        public event EventHandler<float> OnHealthChanged;

        public string CharacterName { get; internal set; }
        public float MaxHealth { get; internal set; }
        public float CurrentHealth { get; internal set; }
        public float PhysicalResistance { get; internal set; }
        public float MagicResistance { get; internal set; }
        public List<AttackSO> attacks = new List<AttackSO>();    

        public static PlayerManager instance { get; private set; }

        public Inventory inventory { get; private set; }

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            this.inventory = GetComponent<Inventory>();

            LoadPlayerData(0);
        }

        public void LoadPlayerData(int slotIndex = 0)
        {
            SaveData savedData = SaveManager.LoadPlayerData(slotIndex);

            if (savedData != null)
            {
                // Load saved data
                CharacterName = savedData.CharacterName;
                MaxHealth = savedData.MaxHealth;
                CurrentHealth = savedData.CurrentHealth;
                PhysicalResistance = savedData.PhysicalResistance;
                MagicResistance = savedData.MagicResistance;

                // Load inventory items
                LoadInventoryItems(savedData);
            }
            else
            {
                // No save found, set default stats
                SetDefaultStats();
            }
        }

        private void LoadInventoryItems(SaveData saveData)
        {
            // TODO: Implement inventory loading
            // For now, just clear and use defaults
            if (inventory != null)
            {
                inventory.RebuildFromStartingGear();
            }
        }

        private void SetDefaultStats()
        {
            CharacterName = "Player";
            MaxHealth = 100f;
            CurrentHealth = MaxHealth;
            PhysicalResistance = 0.1f;
            MagicResistance = 0.05f;
        }

        public void setStats(string name, float maxHealth, float currentHealth, float physicalResistance, float magicResistance)
        {
            CharacterName = name;
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
            PhysicalResistance = physicalResistance;
            MagicResistance = magicResistance;
        }

        public void SetHealth(float newHealth)
        {
            CurrentHealth = Mathf.Clamp(newHealth, 0f, MaxHealth);
            OnHealthChanged?.Invoke(this, CurrentHealth);
        }

        public void Die()
        {
            Debug.Log("Player has bedieded!");
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }

        public void SaveCurrentState(int slotIndex = 0)
        {
            SaveManager.SavePlayerData(this, inventory, slotIndex);
        }
    }
}