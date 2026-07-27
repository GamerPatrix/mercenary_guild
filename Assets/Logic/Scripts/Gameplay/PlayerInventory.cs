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
        private List<ItemCounted> items = new List<ItemCounted>();

        /// <summary>
        /// Invoked whenever the contents of the inventory change.
        /// </summary>
        public event Action InventoryChanged;

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
            return item != null && items.Exists(i => i.itemSO == item);
        }

        /// <summary>
        /// Gets the count of a specific item in the inventory.
        /// </summary>
        public int CountOf(ItemSO item)
        {
            if (item == null) return 0;
            var found = items.Find(i => i.itemSO == item);
            return found.itemSO != null ? found.Actualcount : 0;
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

            var existing = items.Find(i => i.itemSO == item);
            if (existing.itemSO != null)
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

            var index = items.FindIndex(i => i.itemSO == item);
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
            return items.ConvertAll(i => i.itemSO);
        }

        public struct ItemCounted
        {
            public ItemSO itemSO;
            // The actual count of the item in the inventory from 1 so 0 means it shouldnt exist in the inventory
            public int Actualcount;
            public ItemCounted(ItemSO itemSO, int count)
            {
                this.itemSO = itemSO;
                this.Actualcount = count;
            }
            public ItemCounted(ItemSO itemSO)
            {
                this.itemSO = itemSO;
                this.Actualcount = 1;
            }

            public void addCount(int count)
            {
                this.Actualcount += count;
            }
            public void addOne()
            {
                this.Actualcount++;
            }
            public bool removeCount(int count)
            {
                if (this.Actualcount >= count)
                {
                    this.Actualcount -= count;
                    return true;
                }
                Debug.LogError("Attempted to remove more items than available. Actualcount: " + this.Actualcount + ", tried to remove: " + count);
                return false;
            }
            public bool removeOne()
            {
                if (this.Actualcount > 0)
                {
                    this.Actualcount--;
                    return true;
                }
                Debug.LogError("Attempted to remove an item when count is already zero. Actualcount: " + this.Actualcount);
                return false;
            }

        }



    }

}





