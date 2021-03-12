using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtyDishScript : MonoBehaviour
{
    [SerializeField] private DishwashingMinigame Dish;
    public int Amounttoadd;
    public GameObject Dirt,DirtClone;
    // Start is called before the first frame update
    public void OnDirtCleaned()
    {
        
        Dish = GameObject.Find("Game").GetComponent<DishwashingMinigame>();
        Dish.AddToTotal();
        Destroy(Dish.DirtObj);
    }
}
