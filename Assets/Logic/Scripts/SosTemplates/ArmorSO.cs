using UnityEngine;


namespace mercenary_guild.sos
{
    [CreateAssetMenu(fileName = "ArmorSO", menuName = "GuildItems/ArmorSO")]
    public class ArmorSO : GearSO
    {
        public ArmorType armorType;
        [Range(0f, 1f)] public float physicalResistance;
        [Range(0f, 1f)] public float magicResistance;
    }

    public enum ArmorType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots,
        Gloves
    }
}