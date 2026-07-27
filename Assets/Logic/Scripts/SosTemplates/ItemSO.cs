using UnityEngine;
using UnityEngine.Localization;
namespace mercenary_guild.sos
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "GuildItems/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        LocalizedString itemName;
        Sprite UIsprite;
        Sprite WorldSprite;

    }
}