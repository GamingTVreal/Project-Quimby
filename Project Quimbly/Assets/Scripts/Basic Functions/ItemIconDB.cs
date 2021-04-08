using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.BasicFunctions
{      
    [CreateAssetMenu(fileName = "ItemIconDB", menuName = "Project Quimbly/ItemIconDB", order = 0)]
    public class ItemIconDB : ScriptableObject 
    {
        [SerializeField] ItemSprite[] itemSprites;
        Dictionary<Item.ItemType, Sprite> spriteLookup = null;
        Dictionary<Item.ItemType, float> FullnessLookup = null;

        public Sprite GetSprite(Item.ItemType itemType)
        {
            BuildLookup();
            Sprite itemSprite = null;
            spriteLookup.TryGetValue(itemType, out itemSprite);
            return itemSprite;
        }
        public float GetFloat (Item.ItemType itemType)
        {
            BuildLookup();
            float filling = 0;
            FullnessLookup.TryGetValue(itemType, out filling);
            return filling;
        }
        private void BuildLookup()
        {
            if(spriteLookup != null) return;

            spriteLookup = new Dictionary<Item.ItemType, Sprite>();
            foreach (ItemSprite item in itemSprites)
            {
                spriteLookup[item.itemType] = item.sprite;
                FullnessLookup[item.itemType] = item.filling;
            }
        }

        [System.Serializable]
        private class ItemSprite
        {
            public Item.ItemType itemType;
            public Sprite sprite;
            public float filling;
        }    
    }
}
