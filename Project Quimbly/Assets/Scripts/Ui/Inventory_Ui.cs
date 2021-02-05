using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventory_Ui : MonoBehaviour
{
    
    [SerializeField] private RectTransform Menu;
    private BasicFunctions Items;
    Item item;
    private Inventory Inventory;
    public TextMeshProUGUI [] visibleamounts;

    private void Awake()
    {
        int count = this.Inventory.itemList.Count;
        
    }
    private void Update()
    {
        visibleamounts[1] = Item.ItemType.Cake = Item.ItemType.Cake;
    }
    public void SetInventory(Inventory Inventory)
    {
        this.Inventory = Inventory;
    }

    public void InventoryUI()
    {


    }
}