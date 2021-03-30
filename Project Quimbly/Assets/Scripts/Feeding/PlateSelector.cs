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
        List<Item> items = new List<Item>();
        int selectedItemIndex;
        bool isSelectionChanging = false;

        private void Start()
        {
            foreach (Item item in Inventory.Instance.GetItemList())
            {
                if(item.IsConsumable(item.itemType))
                {
                    items.Add(item);
                }
            }
            if(items.Count > 0)
            {
                selectedItemIndex = Random.Range(0, items.Count);
                SetFoodUI(items[selectedItemIndex]);
            }
            else
            {
                OutOfFood();
            }
        }

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
            food.SetItem(items[selectedItemIndex].itemType, 1);
        }

        public void FromConsumeEvent()
        {
            int itemCount = Inventory.Instance.GetItemAmount(items[selectedItemIndex].itemType);
            foodQtyText.text = "(" + itemCount.ToString() + ")";
            if (itemCount <= 0)
            {
                items.Remove(items[selectedItemIndex]);
                ChangeSelectedFood(true);
            }
        }

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

        private void OutOfFood()
        {
            foodText.text = "Out of food";
            foodQtyText.text = "";
            food.gameObject.SetActive(false);
        }
    }
}
