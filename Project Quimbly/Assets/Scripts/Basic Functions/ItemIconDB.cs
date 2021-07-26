using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Inventories
{      
    [CreateAssetMenu(fileName = "ItemIconDB", menuName = "Project Quimbly/ItemIconDB", order = 0)]
    public class ItemIconDB : ScriptableObject 
    {
        [SerializeField] ItemSprite[] itemSprites;
        Dictionary<Item.ItemType, Sprite> spriteLookup = null;
        Dictionary<Item.ItemType, float> FullnessLookup = null;
        Dictionary<Item.ItemType, bool> DrinkableLookup = null;

        public Sprite GetSprite(Item.ItemType itemType)
        {
            BuildLookup();
            Sprite itemSprite = null;
            spriteLookup.TryGetValue(itemType, out itemSprite);
            return itemSprite;
            
        }
        public float GetFullness (Item.ItemType itemType)
        {
            BuildLookup();
            float filling = 0;
            FullnessLookup.TryGetValue(itemType, out filling);
            return filling;
        }
        public bool GetWater (Item.ItemType itemType)
        {
            BuildLookup();
            bool isdrink = true;
            DrinkableLookup.TryGetValue(itemType, out isdrink);
            return isdrink;
        }
        private void BuildLookup()
        {
            if(spriteLookup != null) return;

            FullnessLookup = new Dictionary<Item.ItemType, float>();
            spriteLookup = new Dictionary<Item.ItemType, Sprite>();
            DrinkableLookup = new Dictionary<Item.ItemType, bool>();
            foreach (ItemSprite item in itemSprites)
            {
                spriteLookup[item.itemType] = item.sprite;
                FullnessLookup[item.itemType] = item.filling;
                DrinkableLookup[item.itemType] = item.isdrink;
            }
        }

        [System.Serializable]
        private class ItemSprite
        {
            public Item.ItemType itemType;
            public Sprite sprite;
            public float filling;
            public bool isdrink;
        }    
    }
}
