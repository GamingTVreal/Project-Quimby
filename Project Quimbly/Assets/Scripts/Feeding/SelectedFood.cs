using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Controllers;
using ProjectQuimbly.UI;
using ProjectQuimbly.UI.Dragging;
using UnityEngine;

namespace ProjectQuimbly.Feeding
{
    public class SelectedFood : MonoBehaviour, IRayCastable, IDragSource<Item>
    {
        [SerializeField] Item item = new Item();

        public void SetItem(Item.ItemType itemType, int amount, Sprite icon = null)
        {
            item.itemType = itemType;
            item.amount = amount;
            item.icon = icon;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Hand;
        }

        public Item GetItem()
        {
            return item;
        }

        public int GetNumber()
        {
            // Can be modified later to return non-integer values
            return 1;
        }

        public void RemoveItems(int number)
        {
            Inventory.Instance.RemoveItem(item.itemType, number);
            Debug.Log("Item Removed: " + item.itemType.ToString() + " Remaining " + Inventory.Instance.GetItemAmount(item.itemType));
        }
    }
}
