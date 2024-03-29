﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

using ProjectQuimbly.Feeding;

public class Inventory_Ui : MonoBehaviour
{
    private BasicFunctions Items;
    Item item;
    private Inventory Inventory;
    private Transform ItemTransfom;
    public GameObject InventoryUi;
    public GameObject ItemPrefab;
    public SelectedFood food;
    private void Awake()
    {
        
        //Debug.Log(Inventory.itemList.Count);
        var count = Inventory.Instance.itemList.Count;
        
    }
    public void Add()
    {
        foreach (Item item in Inventory.Instance.GetItemList())
        {
            if (item.amount == 0)
            {
                item.amount = item.amount + 1;
                foreach (Transform child in ItemTransfom)
                {
                    if (child == ItemTransfom) continue;
                    child.gameObject.SetActive(true);
                    
                }
            }
            else
            {
                item.amount = item.amount + 1;
            }

        }
    }
    public void Remove()
    {
            foreach (Item item in Inventory.Instance.GetItemList())
            {
                item.amount = item.amount - 1 + 1;

            if (item.amount < 1)
            {
                
                foreach (Transform child in ItemTransfom)
                {
                    if (child == ItemTransfom) continue;
                    child.gameObject.SetActive(false);
                }
            }
        }


    }
    public void RefreshInventory()
    {
        foreach (Item item in Inventory.Instance.GetItemList())
        {
            
            TextMeshProUGUI uiText = ItemTransfom.Find("ItemCount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
                
            }
            else
            {
                
                uiText.SetText("");
            }
        }
    }
    public void SetItems(BasicFunctions Items)
    {
        this.Items = Items;
    }
    private void Start()
    {

        InventoryUI();
    }
    private void Update()
    {
        
    }
    public void SetInventory(Inventory Inventory)
    {
        this.Inventory = Inventory;
    }

    public void InventoryUI()
    {
        foreach(Item _item in Inventory.Instance.itemList)
        {
            GameObject itemObj = Instantiate(ItemPrefab, InventoryUi.transform);
            ItemTransfom = itemObj.transform;
            ItemTransfom.GetChild(0).GetComponent<Image>().sprite = _item.icon;
            ItemTransfom.GetChild(1).GetComponent<TextMeshProUGUI>().text = _item.itemType.ToString();
            ItemTransfom.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + _item.amount;
        }
    }

    public void DebugListItems()
    {
        foreach (Item item in Inventory.Instance.GetItemList())
        {
            Debug.Log("Item: " + item.itemType.ToString() + " Count: " + item.amount);
        }
    }

    // Add 5 of every item
    public void DebugAddItems()
    {
        foreach (Item.ItemType itemType in System.Enum.GetValues(typeof(Item.ItemType)))
        {
            Inventory.Instance.AddItem(itemType, 5);
        }
    }
}