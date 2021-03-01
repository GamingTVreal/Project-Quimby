using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.Rendering;

public class BasicFunctions : MonoBehaviour
{
    
    [SerializeField] private Inventory_Ui Inventory_UI;
    [SerializeField] GameObject phoneObject;
    public GameObject TextBox;
    [SerializeField] Animator PhoneAnimation;
    [SerializeField] GameObject Phone;
    [SerializeField] TextMeshProUGUI MoneyText, EnergyText, PlayerName;
    static int Money;
    static int Energy;
    public static string Name;
    private Inventory inventory;
    private Item item;
    public GameObject ScriptHolder;
    public void Awake()
    {
        SetName();
        inventory = new Inventory(UseItem);
        Inventory_UI.SetItems(this);
        Inventory_UI.SetInventory(inventory);
        Inventory_UI.InventoryUI();
        inventory.AddItem(new Item { itemType = Item.ItemType.Soda, amount = 1});
    }

    public void AddSoda()
    {
        inventory.AddItem(new Item { itemType = Item.ItemType.Soda, amount = 1});
        Inventory_UI.Add();
        Inventory_UI.RefreshInventory();
    }
    public void RemoveSoda()
    {
        inventory.RemoveItem(new Item { itemType = Item.ItemType.Soda, amount = 1 });
        Inventory_UI.RefreshInventory();
    }
    public void SetName()
    {
        PlayerName.text = Name;
        Debug.Log(Name);
    }
    public Item GetItem()
    {
        return item;
    }
    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Cake:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Cake, amount = 1 });
                break;
            case Item.ItemType.BikePump:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.BikePump, amount = 1 });
                break;
            case Item.ItemType.Soda:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Soda, amount = 1 });
                break;
        }
    }


    void Start()
    {
        DontDestroyOnLoad(ScriptHolder);
        if (TextBox.activeInHierarchy)
        {
            phoneObject.SetActive(false);
        }
        else
        {
            phoneObject.SetActive(true);
        }
        if (Name == null)
        {
            FirstTimeSetup();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FirstTimeSetup()
    {
        Energy = 20;
        Money = 2500;

    }
    public void OpenthePhone()
    {
        if (PhoneAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Phone.SetActive(true);
        }
    }
}

