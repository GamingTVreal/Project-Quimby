using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{

    public int x = 0;
    public Sprite[] Deb;
    public TextBoxManager textboxmanager;
    private void Start()
    {

    }

    public void boobs()
    {
        x = 0;
    }
    public void GetSprite()
    {
        switch (x)
        {
            case 0:
                this.GetComponent<Image>().sprite = Deb[x];
                textboxmanager.EnableSpriteImage();
                textboxmanager.EnableTextBox();
                textboxmanager.currentline = 2;
                
                break;
            case 1:
                this.GetComponent<Image>().sprite = Deb[x];
                break;
            default:
                this.GetComponent<Image>().sprite = Deb[x];
                break;
        }
    }
    void SetSprite()
    {
        
    }


}

