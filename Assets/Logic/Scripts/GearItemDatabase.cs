using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine;

namespace mercenary_guild
{

    /// <summary>
    /// Central list of gear items used by random drop systems.
    /// </summary>
    [CreateAssetMenu(fileName = "CollectibleItemList", menuName = "Items/Collectible Item List")]
    [MovedFrom(true, null, null, "CollectibleItemList")]
    public class CollectibleItemList : ScriptableObject
    {
        [SerializeField]
        [Tooltip("All gear items that can be referenced by drop tables or rewards.")]
        private List<CollectibleItem> items = new List<CollectibleItem>();

        /// <summary>
        /// Items contained in this database.
        /// </summary>
        public IReadOnlyList<CollectibleItem> Items => items;

    #if UNITY_EDITOR
        private void OnValidate()
        {
            if (items == null) return;
            items.RemoveAll(item => item == null);
        }
    #endif
    }



}






