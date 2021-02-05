using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    } 

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
           
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
       else {
          itemList.Add(item);
        }
        
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }

}

