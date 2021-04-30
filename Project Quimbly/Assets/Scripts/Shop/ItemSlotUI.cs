using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ProjectQuimbly.Shop
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] TextMeshProUGUI itemCostOrCountText;
        [SerializeField] Button button;

        string prefix = "Cost: ";
        Item.ItemType itemType;

        public void SetupItemEntry(Sprite icon, string itemName, int value, bool isShopItem)
        {
            itemImage.sprite = icon;
            itemNameText.text = itemName;
            if(!isShopItem)
            {
                prefix = "Qty: ";
            }
            button.interactable = isShopItem;
            itemCostOrCountText.text = prefix + value.ToString();
        }

        public void UpdateItemQuantity(int value)
        {
            itemCostOrCountText.text = prefix + value.ToString();
        }

        public Button GetButton()
        {
            return button;
        }
    }
}
