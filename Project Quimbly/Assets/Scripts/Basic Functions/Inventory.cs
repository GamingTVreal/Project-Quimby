﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
    
    }
    

    public void IncreaseItemAmount()
    {
        foreach (Item item in GetItemList())
        {
            if(item.amount > 0)
            {
                item.amount = item.amount + 1;
            }
            else
            {

            }
        }
    }

    

    public void AddItem(Item item)
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                item.amount = item.amount + 1;
                    itemAlreadyInInventory = true;
                    Debug.Log(item.amount);
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);

        }
    public void RemoveItem(Item item)
    {
        
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
       
    
       
        public void UseItem(Item item)
    {
        useItemAction(item);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }

}


