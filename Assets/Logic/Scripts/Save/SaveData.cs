using System;
using System.Collections.Generic;

namespace mercenary_guild
{
    [Serializable]
    public class SaveData
    {
        public string CharacterName;
        public float MaxHealth;
        public float CurrentHealth;
        public float PhysicalResistance;
        public float MagicResistance;
        public List<string> InventoryItems;

        public SaveData()
        {
            InventoryItems = new List<string>();
        }


    }
}
