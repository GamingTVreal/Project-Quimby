using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Saving
{
    public class InventorySaver : MonoBehaviour, ISaveable
    {
        public object CaptureState()
        {
            List<Item> itemList = Inventory.Instance.GetItemList();

            List<ItemRecord> records = new List<ItemRecord>();
            foreach (var item in itemList)
            {
                ItemRecord record = new ItemRecord();
                record.itemType = item.itemType.ToString();
                record.count = item.amount;
                records.Add(record);
            }

            return records;
        }

        public void RestoreState(object state)
        {
            List<ItemRecord> records = (List<ItemRecord>)state;
            List<Item> itemList = new List<Item>();
            foreach(var record in records) 
            {
                Item item = new Item();
                item.SetType(record.itemType);
                item.amount = record.count;
                itemList.Add(item);
            }
            Inventory.Instance.RestoreItemList(itemList);
        }

        [System.Serializable]
        private class ItemRecord
        {
            public string itemType;
            public int count;
        }
    }
}
