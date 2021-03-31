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

        public Sprite GetSprite(Item.ItemType itemType)
        {
            BuildLookup();
            Sprite itemSprite = null;
            spriteLookup.TryGetValue(itemType, out itemSprite);
            return itemSprite;
        }

        private void BuildLookup()
        {
            if(spriteLookup != null) return;

            spriteLookup = new Dictionary<Item.ItemType, Sprite>();
            foreach (ItemSprite item in itemSprites)
            {
                spriteLookup[item.itemType] = item.sprite;
            }
        }

        [System.Serializable]
        private class ItemSprite
        {
            public Item.ItemType itemType;
            public Sprite sprite;
        }    
    }
}
