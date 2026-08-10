using System;
using System.Collections.Generic;
using UnityEngine;
using mercenary_guild.sos;
namespace mercenary_guild
{


    /// <summary>
    /// Stores the collection of items owned by the player. The inventory keeps track of unequipped gear and notifies listeners whenever its contents changes
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<ItemCounted> items = new List<ItemCounted>();

        /// <summary>
        /// Invoked whenever the contents of the inventory change.
        /// </summary>
        public event Action InventoryChanged;

        /// <summary>
        /// Exposes the list of items for inspector access.
        /// Note: Modifications in inspector won't trigger InventoryChanged event.
        /// </summary>
        public List<ItemCounted> Items => items;

        private void Awake()
        {

        }

        private void OnEnable()
        {
            NotifyInventoryChanged();
        }

        /// <summary>
        /// Removes all items currently stored and repopulates the inventory with the starting gear list.
        /// </summary>
        public void RebuildFromStartingGear()
        {
            items.Clear();
            NotifyInventoryChanged();
        }

        /// <summary>
        /// Determines whether the supplied item is currently present in the inventory.
        /// </summary>
        public bool Contains(ItemSO item)
        {
            return item != null && items.Exists(i => i.ItemSO == item);
        }

        /// <summary>
        /// Gets the count of a specific item in the inventory.
        /// </summary>
        public int CountOf(ItemSO item)
        {
            if (item == null) return 0;
            var found = items.Find(i => i.ItemSO == item);
            return found.ItemSO != null ? found.Actualcount : 0;
        }

        /// <summary>
        /// Adds the supplied item to the inventory.
        /// </summary>
        public bool Add(ItemSO item, int count = 1)
        {
            if (item == null)
            {
                return false;
            }

            var existing = items.Find(i => i.ItemSO == item);
            if (existing.ItemSO != null)
            {
                existing.addCount(count);
            }
            else
            {
                items.Add(new ItemCounted(item, count));
            }

            NotifyInventoryChanged();
            return true;
        }


        /// <summary>
        /// Removes the supplied item from the inventory.
        /// </summary>
        public bool Remove(ItemSO item, int count = 1)
        {
            if (item == null)
            {
                return false;
            }

            var index = items.FindIndex(i => i.ItemSO == item);
            if (index == -1)
            {
                return false;
            }

            var itemCounted = items[index];
            if (!itemCounted.removeCount(count))
            {
                return false;
            }

            if (itemCounted.Actualcount <= 0)
            {
                items.RemoveAt(index);
            }
            else
            {
                items[index] = itemCounted;
            }

            NotifyInventoryChanged();
            return true;
        }

        private void NotifyInventoryChanged()
        {
            InventoryChanged?.Invoke();
        }

        public List<ItemCounted> GetAllItemCounted()
        {
            return new List<ItemCounted>(items);
        }

        public List<ItemSO> GetAllItems()
        {
            return items.ConvertAll(i => i.ItemSO);
        }

        [System.Serializable]
        public struct ItemCounted
        {
            [SerializeField] private ItemSO itemSO;
            // The actual count of the item in the inventory from 1 so 0 means it shouldnt exist in the inventory
            [SerializeField] private int actualcount;

            public ItemCounted(ItemSO itemSO, int count)
            {
                this.itemSO = itemSO;
                this.actualcount = count;
            }

            public ItemCounted(ItemSO itemSO)
            {
                this.itemSO = itemSO;
                this.actualcount = 1;
            }

            public ItemSO ItemSO { get => itemSO; set => itemSO = value; }
            public int Actualcount { get => actualcount; set => actualcount = value; }

            public void addCount(int count)
            {
                this.actualcount += count;
            }

            public void addOne()
            {
                this.actualcount++;
            }

            public bool removeCount(int count)
            {
                if (this.actualcount >= count)
                {
                    this.actualcount -= count;
                    return true;
                }
                Debug.LogError("Attempted to remove more items than available. Actualcount: " + this.actualcount + ", tried to remove: " + count);
                return false;
            }

            public bool removeOne()
            {
                if (this.actualcount > 0)
                {
                    this.actualcount--;
                    return true;
                }
                Debug.LogError("Attempted to remove an item when count is already zero. Actualcount: " + this.actualcount);
                return false;
            }

        }



    }

}





