using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Shop
{
    [CreateAssetMenu(fileName = "ShopItemsDB", menuName = "Project Quimbly/ShopItemsDB")]
    public class ShopItemsDB : ScriptableObject
    {
        [SerializeField] ShopItemDetails[] shopItemDetails;

        public IEnumerable<Item.ItemType> GetItems()
        {
            foreach (var shopItem in shopItemDetails)
            {
                yield return shopItem.item;
            }
        }

        public int GetItemCost(int i)
        {
            return shopItemDetails[i].cost;
        }

        [System.Serializable]
        private class ShopItemDetails
        {
            public Item.ItemType item;
            public int cost;
        }
    }
}
