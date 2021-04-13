using System;
using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.BasicFunctions;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;
    ItemIconDB itemIconDB = null;

    private int _savedListCount;

    private void Awake()
    {
        LoadItemIconDB();
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
                item.amount += 1;
                itemAlreadyInInventory = true;
                Debug.Log(item.amount);
                break;
            }
        }
        if (!itemAlreadyInInventory)
        {
            item.icon = itemIconDB.GetSprite(item.itemType);
            item.filling = itemIconDB.GetFullness(item.itemType);
            itemList.Add(item);
        }
        Debug.Log(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        SaveList();
    }

    public void AddItem(Item.ItemType itemType, int amount)
    {
        bool itemAlreadyInInventory = false;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == itemType)
            {
                inventoryItem.amount += amount;
                itemAlreadyInInventory = true;
                break;
            }
        }
        if (!itemAlreadyInInventory)
        {
            Item newItem = new Item();
            newItem.itemType = itemType;
            newItem.amount = amount;
            newItem.icon = itemIconDB.GetSprite(newItem.itemType);
            newItem.filling = itemIconDB.GetFullness(newItem.itemType);
            itemList.Add(newItem);
        }
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

    public void RemoveItem(Item.ItemType itemType, int amount)
    {
        foreach (var inventoryItem in itemList)
        {
            if(inventoryItem.itemType == itemType)
            {
                inventoryItem.amount -= amount;
                if(inventoryItem.amount <= 0)
                {
                    itemList.Remove(inventoryItem);
                }

                break;
            }
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

    public int GetItemAmount(Item.ItemType itemType)
    {
        foreach (var inventoryItem in itemList)
        {
            if (inventoryItem.itemType == itemType)
            {
                return inventoryItem.amount;
            }
        }
        return 0;
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

    // Loads ScriptableObject with item/sprite pairing lookup
    private void LoadItemIconDB()
    {
        if (itemIconDB == null)
        {
            itemIconDB = Resources.Load<ItemIconDB>("Core/ItemIconDB");
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
                newItem.icon = itemIconDB.GetSprite(newItem.itemType);
                newItem.filling = itemIconDB.GetFullness(newItem.itemType);
                itemList.Add(newItem);
            }
        }
        else
        {
            itemList = new List<Item>();
            Item item = new Item();
            item.itemType = Item.ItemType.Soda;
            item.amount = 1;
            item.icon = itemIconDB.GetSprite(item.itemType);
            item.filling = itemIconDB.GetFullness(item.itemType);
            AddItem(item);
        }
    }
}


