using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectQuimbly.Feeding
{
    public class PlateSelector : MonoBehaviour
    {
        [SerializeField] SelectedFood food;
        [SerializeField] TextMeshProUGUI foodText;
        [SerializeField] TextMeshProUGUI foodQtyText;
        [SerializeField] GameObject Exit;
        public List<Item> items = new List<Item>();
        public int selectedItemIndex;
        bool isSelectionChanging = false;

        FoodDragItem foodDragItem = null;

        private void Start()
        {
            BuildConsumableList();
            DisplayStartingFood();
            foodDragItem = food.GetComponent<FoodDragItem>();
        }

        // Loop through inventory and build list of consumable foods for session
        private void BuildConsumableList()
        {
            foreach (Item item in Inventory.Instance.GetItemList())
            {
                if (item.IsConsumable(item.itemType))
                {
                    items.Add(item);
                }
            }
        }

        // If food in inventory, start with random item, otherwise display out of food
        private void DisplayStartingFood()
        {
            if (items.Count > 0)
            {
                selectedItemIndex = Random.Range(0, items.Count);
                SetFoodUI(items[selectedItemIndex]);
            }
            else
            {
                OutOfFood();
            }
        }

        // Check player inputs for selecting new plate
        private void Update() 
        {
            float hInput = Input.GetAxis("Horizontal");
            if(!isSelectionChanging && hInput >= .999f)
            {
                isSelectionChanging = true;
                ChangeSelectedFood(true);
            }
            else if(!isSelectionChanging && hInput <= -.999f)
            {
                isSelectionChanging = true;
                ChangeSelectedFood(false);
            }
            else if(isSelectionChanging && Mathf.Abs(hInput) <= 0.9f)
            {
                isSelectionChanging = false;
            }
        }

        public void SetFoodUI(Item item)
        {
            // Setting up with qty of 1 atm
            food.gameObject.SetActive(true);
            foodText.text = items[selectedItemIndex].itemType.ToString();
            foodQtyText.text = "(" + items[selectedItemIndex].amount.ToString() + ")";
            food.SetItem(items[selectedItemIndex].itemType, 1, item.isdrink, item.filling, item.icon);
        }

        // On mouth trigger, event is called when item is consumed.
        // Decrements consumable item list and updates text/plate.
        public void OnItemConsumeEvent()
        {
            if(items.Count == 0) BuildConsumableList();
            int itemCount = Inventory.Instance.GetItemAmount(items[selectedItemIndex].itemType);
            foodQtyText.text = "(" + itemCount.ToString() + ")";
            if (itemCount <= 0)
            {
                items.Remove(items[selectedItemIndex]);
                ChangeSelectedFood(true);
            }
            foodDragItem.CancelDrag();
        }

        // Used via arrow or button controls to change selected food
        public void ChangeSelectedFood(bool isToRight)
        {
            selectedItemIndex += isToRight ? 1: -1;
            if(items.Count == 0)
            {
                selectedItemIndex = 0;
                OutOfFood();
                return;
            }
            if(selectedItemIndex >= items.Count)
            {
                selectedItemIndex = 0;
            }
            else if(selectedItemIndex < 0)
            {
                selectedItemIndex = items.Count - 1;
            }
            SetFoodUI(items[selectedItemIndex]);
        }

        // Called by FeedingRoom for audio sfx
        public Item GetSelectedItem()
        {
            return items[selectedItemIndex];
        }

        // On empty inventory, disable interactable food and update text
        private void OutOfFood()
        {
            Exit.SetActive(true);
            foodText.text = "Out of food";
            foodQtyText.text = "";
            food.gameObject.SetActive(false);
        }
    }
}
