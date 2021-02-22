using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
