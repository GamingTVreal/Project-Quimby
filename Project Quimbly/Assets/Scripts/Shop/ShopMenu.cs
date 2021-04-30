using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ProjectQuimbly.BasicFunctions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectQuimbly.Shop
{
    public class ShopMenu : MonoBehaviour
    {
        [SerializeField] ShopItemsDB shopItemsDB = null;
        [SerializeField] ItemIconDB itemIconDB = null;
        [SerializeField] RectTransform shopContent;
        [SerializeField] RectTransform invContent;
        [SerializeField] TextMeshProUGUI moneyText;
        [SerializeField] Transform itemSlotPool;
        [SerializeField] ItemSlotUI itemSlotPrefab;

        [SerializeField] int itemSpacing = 120;
        Dictionary<Item.ItemType, ItemSlotUI> invSlotLookup = null;

        private void OnEnable()
        {
            PopulateShopMenus();
        }

        private void OnDisable() 
        {
            ReturnItemSlotsToPool(shopContent);
            ReturnItemSlotsToPool(invContent);
        }

        private void PopulateShopMenus()
        {
            SetupShopMenuSlots();
            SetupInvMenuSlots();
            UpdateMoney();
        }

        // Update money text here
        private void UpdateMoney()
        {
            int money = PlayerStats.Instance.GetMoney();
            moneyText.text = money.ToString();
        }

        // Loop through ShopItemsDB and set up item slots for each entry
        private void SetupShopMenuSlots()
        {
            int i = 0;
            ItemSlotUI itemSlot = null;
            foreach (Item.ItemType itemType in shopItemsDB.GetItems())
            {
                if (itemSlotPool.childCount > 0)
                {
                    // Pull from pool
                    itemSlot = itemSlotPool.GetChild(0).GetComponent<ItemSlotUI>();
                    itemSlot.transform.SetParent(shopContent, false);
                }
                else
                {
                    // Instantiate new entries
                    itemSlot = Instantiate(itemSlotPrefab, shopContent);
                }

                // Add listener for buy button
                Button slotButton = itemSlot.GetButton();
                slotButton.onClick.RemoveAllListeners();
                int itemCost = shopItemsDB.GetItemCost(i);
                slotButton.onClick.AddListener(() => BuyItem(itemType, itemCost));

                // Supposed to separate camel case?
                string itemName = Regex.Replace(itemType.ToString(), "(//B[A-Z])", " $1");
                itemSlot.SetupItemEntry(itemIconDB.GetSprite(itemType), itemName, itemCost, true);

                // Position slot in content window
                itemSlot.transform.localPosition = new Vector2(itemSlot.transform.localPosition.x, i * -itemSpacing);
                itemSlot.gameObject.SetActive(true);

                i++;

                //  Resize shop menu
                int contentHeight = i * itemSpacing;
                shopContent.sizeDelta = new Vector2(shopContent.sizeDelta.x, contentHeight);
            }
        }

        // Loop through inventory and set up item slots
        private void SetupInvMenuSlots()
        {
            int i = 0;
            invSlotLookup = new Dictionary<Item.ItemType, ItemSlotUI>();
            foreach (Item item in Inventory.Instance.GetItemList())
            {
                InvSlotSetup(i, item);

                i++;

                //  Resize shop menu
                int contentHeight = i * itemSpacing;
                invContent.sizeDelta = new Vector2(invContent.sizeDelta.x, contentHeight);
            }
        }

        private void InvSlotSetup(int i, Item item)
        {
            ItemSlotUI itemSlot = null;
            if (itemSlotPool.childCount > 0)
            {
                // Pull from pool
                itemSlot = itemSlotPool.GetChild(0).GetComponent<ItemSlotUI>();
                itemSlot.transform.SetParent(invContent, false);
            }
            else
            {
                // Instantiate new entries
                itemSlot = Instantiate(itemSlotPrefab, invContent);
            }
            invSlotLookup[item.itemType] = itemSlot;

            // Supposed to separate camel case?
            string itemName = Regex.Replace(item.itemType.ToString(), "(//B[A-Z])", "$1 ");
            itemSlot.SetupItemEntry(item.icon, itemName, item.amount, false);

            // Position slot in content window
            itemSlot.transform.localPosition = new Vector2(itemSlot.transform.localPosition.x, i * -itemSpacing);
            itemSlot.gameObject.SetActive(true);
        }

        private void BuyItem(Item.ItemType itemType, int cost)
        {
            // Check if player has money
            int playerMoney = PlayerStats.Instance.GetMoney();
            if(cost <= playerMoney)
            {
                // Add item and subtract money
                Inventory.Instance.AddItem(itemType, 1);
                PlayerStats.Instance.AdjustMoney(-cost);
                UpdateMoney();

                // Update inventory side of shop
                ItemSlotUI itemSlot = null;
                if(invSlotLookup.TryGetValue(itemType, out itemSlot))
                {
                    itemSlot.UpdateItemQuantity(Inventory.Instance.GetItemAmount(itemType));
                }
                else
                {
                    InvSlotSetup(invContent.childCount, Inventory.Instance.GetItem(itemType));
                }
            }
        }

        private void ReturnItemSlotsToPool(Transform content)
        {
            int maxChildIndex = content.childCount - 1;
            for (int i = maxChildIndex; i > -1; i--)
            {
                Transform slot = content.GetChild(i).transform;
                slot.SetParent(itemSlotPool, false);
                slot.gameObject.SetActive(false);
            }
        }
    }
}
