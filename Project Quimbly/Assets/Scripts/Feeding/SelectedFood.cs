using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.BasicFunctions;
using ProjectQuimbly.Controllers;
using ProjectQuimbly.UI;
using ProjectQuimbly.UI.Dragging;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectQuimbly.Feeding
{
    public class SelectedFood : MonoBehaviour, IRayCastable, IDragSource<Item>
    {
        [SerializeField] Item item = new Item();
        [SerializeField] Image foodImage = null;
        private CharacterController Character;
        private PlateSelector Plate;
        private void Awake() 
        {
            Character = FindObjectOfType<CharacterController>();
            Plate = FindObjectOfType<PlateSelector>();

            if (!foodImage)
            {
                foodImage = GetComponent<Image>();
            }
        }

        public void SetItem(Item.ItemType itemType, int amount, float filling, Sprite icon = null)
        {
            item.itemType = itemType;
            item.amount = amount;
            item.icon = icon;
            item.filling = filling;
            foodImage.sprite = item.icon;
        }

        // Return value for IRayCastable interface
        public CursorType GetCursorType()
        {
            return CursorType.Hand;
        }

        public Item GetItem()
        {
            return item;
        }

        // Return value for IDragSource
        public int GetNumber()
        {
            // Can be modified later to return non-integer values
            return 1;
        }

        // Removes item from inventory
        public void RemoveItems(int number)
        {
            Inventory.Instance.RemoveItem(item.itemType, number);
            // Debug.Log("Item Removed: " + item.itemType.ToString() + " Remaining " + Inventory.Instance.GetItemAmount(item.itemType));
        }
       
    }
}
