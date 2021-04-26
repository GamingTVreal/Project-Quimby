using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public Sprite icon;
    public int amount;
    public float filling;
    public bool isdrink;
    bool isConsumable;

    public void SetType(string type)
    {
        itemType = (ItemType)Enum.Parse(typeof(ItemType), type);
    }

    public bool IsConsumable(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.BikePump:
            case Item.ItemType.Roses:
                return false;
            default:
                return true;
        }
    }

}
