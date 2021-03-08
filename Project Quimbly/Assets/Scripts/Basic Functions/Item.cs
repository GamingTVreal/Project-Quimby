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
    public int amount;

    public void SetType(string type)
    {
        itemType = (ItemType)Enum.Parse(typeof(ItemType), type);
    }

}
