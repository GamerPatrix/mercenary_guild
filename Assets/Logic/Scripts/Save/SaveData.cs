using System;
using System.Collections.Generic;

namespace mercenary_guild
{
    [System.Serializable]
    public class SaveData
    {
        public string CharacterName;
        public float MaxHealth;
        public float CurrentHealth;
        public float PhysicalResistance;
        public float MagicResistance;
        public List<ItemData> InventoryItemNames;

        public SaveData()
        {
            InventoryItemNames = new List<ItemData>();
        }

        [System.Serializable]
        public struct ItemData
        {
            public string itemName;
            public int count;
        }
    }
}
