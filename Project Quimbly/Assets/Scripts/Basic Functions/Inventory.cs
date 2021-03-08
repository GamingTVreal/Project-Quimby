using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;

    private int _savedListCount;

    private void Awake()
    {
        LoadList();
    }

    private void Update()
    {
        SaveList();
    }

    public void InventoryN(Action<Item> useItemAction)
    {
        if (itemList == null)
        {
            this.useItemAction = useItemAction;
            itemList = new List<Item>();
        }
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
        Debug.Log(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        SaveList();

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
        SaveList();
    }
       
    
       
    public void UseItem(Item item)
    {
        useItemAction(item);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void SaveList()
    {
        if (itemList != null)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                PlayerPrefs.SetString("ItemType" + i, itemList[i].itemType.ToString());
                PlayerPrefs.SetInt("ItemAmount" + i, itemList[i].amount);
            }

            PlayerPrefs.SetInt("Count", itemList.Count);
        }
    }

    public void LoadList()
    {
        //this.useItemAction = useItemAction;
        //itemList = new List<Item>();
        _savedListCount = PlayerPrefs.GetInt("Count");
        if (_savedListCount != 0)
        {
            itemList = new List<Item>();
            itemList?.Clear();

            for (int i = 0; i < _savedListCount; i++)
            {
                Item newItem = new Item();
                newItem.SetType(PlayerPrefs.GetString("ItemType" + i));
                newItem.amount = PlayerPrefs.GetInt("ItemAmount" + i);
                itemList.Add(newItem);
            }
        }
        else
        {
            itemList = new List<Item>();
            Item item = new Item();
            item.itemType = Item.ItemType.Soda;
            item.amount = 1;
            AddItem(item);
        }
    }
}


