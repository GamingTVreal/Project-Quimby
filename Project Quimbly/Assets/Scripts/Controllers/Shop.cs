using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TextBoxManager Peter;
    public GameObject Store;
    bool TalkedToShopLady = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void StartBrowsing()
    {
        if (TalkedToShopLady == true)
        {
            Store.SetActive(true);
        }
        else
        {
            Peter.currentline = 20;
            Peter.EnableTextBox();
            Peter.EnableSpriteImage();
            Peter.endatline = 23;
            TalkedToShopLady = true;
        }

        
    }
}
