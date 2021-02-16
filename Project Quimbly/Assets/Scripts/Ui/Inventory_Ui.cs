﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class Inventory_Ui : MonoBehaviour
{
    private BasicFunctions Items;
    Item item;
    private Inventory Inventory;
    private Transform ItemTransfom;
    public GameObject InventoryUi;
    public GameObject ItemPrefab;

    private void Awake()
    {
        Debug.Log(Inventory.itemList.Count);
        var count = this.Inventory.itemList.Count;
        
    }
    public void Add()
    {
        foreach (Item item in Inventory.GetItemList())
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
            foreach (Item item in Inventory.GetItemList())
            {
                item.amount = item.amount - 1;

            if (item.amount < 1)
            {
                Debug.Log("No");
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
        foreach (Item item in Inventory.GetItemList())
        {
            
            TextMeshProUGUI uiText = ItemTransfom.Find("ItemCount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
                Debug.Log("Over1");
            }
            else
            {
                Debug.Log("Else");
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
        foreach(Item _item in Inventory.itemList)
        {
            GameObject itemObj = Instantiate(ItemPrefab, InventoryUi.transform);
            ItemTransfom = itemObj.transform;
            ItemTransfom.GetChild(1).GetComponent<TextMeshProUGUI>().text = _item.itemType.ToString();
            ItemTransfom.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + _item.amount;
        }
    }
}