using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using ProjectQuimbly.UI;
using ProjectQuimbly.Controllers;
using ProjectQuimbly.Feeding;
using System;

public class BasicFunctions : MonoBehaviour
{
    
    [SerializeField] private Inventory_Ui Inventory_UI;
    [SerializeField] GameObject phoneObject;
    public GameObject TextBox;
    [SerializeField] Animator PhoneAnimation;
    [SerializeField] GameObject Phone;
    public SelectedFood Food;
    //[SerializeField] TextMeshProUGUI MoneyText, EnergyText, PlayerName;
    static int Money;
    static int Energy;
    public static string Name;
    private Inventory inventory;
    private Item item;
    public GameObject ScriptHolder;

    // Cursor struct for dynamic cursor
    [SerializeField] LayerMask grabbableLayers;
    [System.Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot; // Used to offset X, Y to move cursor image to be where expected
    }
    [SerializeField] CursorMapping[] cursorMappings = null;

    // Used for dragging item with cursor
    bool isDraggingUI = false;
    bool isHoveringUI = false;

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
        // Debug.Log(Name);
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
        // if (TextBox.activeInHierarchy)
        // {
        //     phoneObject.SetActive(false);
        // }
        // else
        // {
        //     phoneObject.SetActive(true);
        // }
        if (Name == null)
        {
            FirstTimeSetup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCursorOnUI();
        //MoneyText.text = Money.ToString();
        //EnergyText.text = Energy.ToString();
    }

    void FirstTimeSetup()
    {
        // PlayerStats.Instance.AdjustMoney(500,false);
        PlayerStats.Instance.AdjustEnergy(20);
    }

    public void AdjustMoney(int amount)
    {
        PlayerStats.Instance.AdjustMoney(amount);
    }

    public void AdjustMoney(string[] amount)
    {
        if(amount == null) return;

        int value;
        if(int.TryParse(amount[0], out value))
        {
            PlayerStats.Instance.AdjustMoney(value);
        }
    }

    public void AdjustEnergy(int amount)
    {
        PlayerStats.Instance.AdjustEnergy(20);
    }

    public void AdjustEnergy(string[] amount)
    {
        if(amount == null) return;

        int value;
        if(int.TryParse(amount[0], out value))
        {
            PlayerStats.Instance.AdjustEnergy(value);
        }
    }

    public void AddItem(string[] itemParameters)
    {
        Item.ItemType itemType;
        if(Enum.TryParse(itemParameters[0], true, out itemType))
        {
            Inventory.Instance.AddItem(itemType, int.Parse(itemParameters[1]));
        }
    }

    public void OpenthePhone()
    {
        if (PhoneAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Phone.SetActive(true);
        }
    }

    // Check if mouse cursor is over interactable object
    private bool ChangeCursorOnUI()
    {
        // On release left-click, set drag to false
        if(Input.GetMouseButtonUp(0))
        {
            isDraggingUI = false;
        }
        if (isDraggingUI)
        {
            return true;
        }

        // Raycast under mouse curse and see if top item is interactable (can be extended to get array and sort)
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, Mathf.Infinity, grabbableLayers);
        if(hit.collider != null)
        {
            IRayCastable rayCastable = hit.collider.GetComponent<IRayCastable>();
            if (rayCastable != null)
            {
                // On left-click, set drag to true and change cursor
                if(Input.GetMouseButtonDown(0))
                {
                    isDraggingUI = true;
                    isHoveringUI = false;
                    SetCursor(CursorType.Grab);
                    return true;
                }
                else if(!isHoveringUI)
                {
                    isHoveringUI = true;
                    SetCursor(rayCastable.GetCursorType());
                }
                return true;
            }
        }

        isHoveringUI = false;
        SetCursor(CursorType.None);
        return false;
    }

    public void CancelUIDrag()
    {
        isDraggingUI = false;
    }

    private void SetCursor (CursorType type)
    {
        CursorMapping mapping = GetCursorMapping(type);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
    }
    public void ReportBugs()
    {
        Application.OpenURL("https://forms.gle/AjfjdCRSPvLctiaUA");
    }
    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach (CursorMapping mapping in cursorMappings)
        {
            if (mapping.type == type)
            {
                return mapping;
            }
        }
        return cursorMappings[0];
    }
}

