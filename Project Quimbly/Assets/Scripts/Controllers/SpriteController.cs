using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{

    public int x = 0;
    public Sprite[] Deb, Extras;
    public TextBoxManager textboxmanager;
    private void Start()
    {

    }
    public void SetSprite()
    {
        switch (x)
        {
            case 0:
                this.GetComponent<Image>().sprite = Deb[x];
                break;
            case 1:
                this.GetComponent<Image>().sprite = Deb[x];
                break;
            default:
                this.GetComponent<Image>().sprite = Deb[x];
                break;
        }
    }
    public void GetSprite()
    {
        if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("1"))
        {
            x = 0; 
        }
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("2"))
        {
            x = 1;
        }
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("3"))
        {
            x = 2;
        }
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("4"))
        {
            x = 3;
        }
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("5"))
        {
            x = 4;
        }
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("6"))
        {
            x = 0;
            textboxmanager.DisableSpriteImage();
            
        }
        SetSprite();
    }


}

