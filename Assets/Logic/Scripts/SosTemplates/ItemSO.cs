using UnityEngine;
using UnityEngine.Localization;
namespace mercenary_guild.sos
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "GuildItems/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] LocalizedString itemName;
        [SerializeField] Sprite UIsprite;
        [SerializeField] Sprite WorldSprite;

        public string ItemName;
    }
}