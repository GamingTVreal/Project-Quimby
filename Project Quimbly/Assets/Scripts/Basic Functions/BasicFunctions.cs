using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BasicFunctions : MonoBehaviour
{
    
    [SerializeField] private Inventory_Ui Inventory_UI;
    [SerializeField] GameObject phoneObject;
    public GameObject TextBox;
    [SerializeField] Animator PhoneAnimation;
    [SerializeField] GameObject Phone;
    //[SerializeField] TextMeshProUGUI MoneyText, EnergyText, PlayerName;
    static int Money;
    static int Energy;
    public static string Name;
    private Inventory inventory;
    private Item item;
    public GameObject ScriptHolder;
    public void Awake()
    {
        if(PlayerStats.Instance.Name == null)
        {
            SetName();
        }
        

        //Inventory.Instance.InventoryN(UseItem);
        //inventory = new Inventory(UseItem);
        //Inventory_UI.SetItems(this);
        //Inventory_UI.SetInventory(inventory);
        //Inventory_UI.InventoryUI();
        //Inventory.Instance.AddItem(new Item { itemType = Item.ItemType.Soda, amount = 1});
    }

    public void AddSoda()
    {
        Inventory.Instance.AddItem(new Item { itemType = Item.ItemType.Soda, amount = 1});
        Inventory_UI.Add();
        Inventory_UI.RefreshInventory();
    }
    public void RemoveSoda()
    {
        Inventory.Instance.RemoveItem(new Item { itemType = Item.ItemType.Soda, amount = 1 });
        Inventory_UI.Remove();
        Inventory_UI.RefreshInventory();
    }
    public void SetName()
    {
        PlayerStats.Instance.Name = Name;
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
                Inventory.Instance.RemoveItem(new Item { itemType = Item.ItemType.Cake, amount = 1 });
                break;
            case Item.ItemType.BikePump:
                Inventory.Instance.RemoveItem(new Item { itemType = Item.ItemType.BikePump, amount = 1 });
                break;
            case Item.ItemType.Soda:
                Inventory.Instance.RemoveItem(new Item { itemType = Item.ItemType.Soda, amount = 1 });
                break;
        }
    }


    void Start()
    {
        //Inventory.Instance.AddItem(new Item { itemType = Item.ItemType.Soda, amount = 1 });
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
        //MoneyText.text = Money.ToString();
        //EnergyText.text = Energy.ToString();
    }
    void FirstTimeSetup()
    {
        PlayerStats.Instance.AdjustMoney(2500,false);
        PlayerStats.Instance.AdjustEnergy(20);

    }
    public void OpenthePhone()
    {
        if (PhoneAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Phone.SetActive(true);
        }
    }
}

