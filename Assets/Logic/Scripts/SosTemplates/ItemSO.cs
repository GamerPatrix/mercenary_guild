using UnityEngine;
using UnityEngine.Localization;
using System;
namespace mercenary_guild.sos
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "GuildItems/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] public LocalizedString itemName;
        [SerializeField] public Sprite UIsprite;
        [SerializeField] public Sprite WorldSprite;

        public string ItemName => itemName.TableEntryReference.Key;

        public string GetItemName() {
            return itemName.GetLocalizedString();
        }
    }
}