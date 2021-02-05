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
    [SerializeField] TextMeshProUGUI MoneyText, EnergyText, PlayerName;
    static int Money;
    static int Energy;
    public static string Name;
    public int[] ItemsHeld;
    private Inventory Inventory;

    private void Awake()
    {
        Inventory = new Inventory();
        Inventory_UI.SetInventory(Inventory);
    }


    void Start() 
    {
        if(TextBox.activeInHierarchy)
        {
            phoneObject.SetActive(false);
        }
        else
        {
            phoneObject.SetActive(true);
        }
        if(Name == null)
        {
            FirstTimeSetup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerName.text = Name;
        MoneyText.text = Money.ToString();
        EnergyText.text = Energy.ToString();
    }
    void FirstTimeSetup()
    {
        Energy = 20;
        Money = 2500;
        
    }
   public void OpenthePhone()
    {
        if (PhoneAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime>1)
        {
            Debug.Log("NotPlaying");
            Phone.SetActive(true);
        }
        else
        {
            Debug.Log("Playing");
        }
    }
}
public class Item
{
    public enum ItemType
    {
        Cake,
        Soda,
        Mints,
        BikePump,
        Sandwich,
        Water,
        Pizza,
        Burger,
        Chocolates,
        Roses,
    }
    public ItemType itemType;
    public int amount;
    
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Cake:
            case ItemType.Soda:
            case ItemType.Mints:
            case ItemType.Sandwich:
            case ItemType.Water:
            case ItemType.Pizza:
            case ItemType.Burger:
            case ItemType.Chocolates:
            case ItemType.Roses:
                return true;
            
            case ItemType.BikePump:
                return false;
        }
    }
}