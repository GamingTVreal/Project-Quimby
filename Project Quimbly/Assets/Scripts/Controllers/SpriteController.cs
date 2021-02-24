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

    public void GetSprite()
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

    public void SetSprite() //I know this function sucks but I could not find another way around this. Please forgive me :(
    {

        if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("1"))
        {
            x = 1;
        }  
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("2"))
        {
            x = 2;
        }
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("3"))
        {
            x = 3;
        }
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("4"))
        {
            x = 4;
        }
        else if (textboxmanager.textlines[textboxmanager.CurrentSprite].Contains("5"))
        {
            x = 5;
        }
        else
        {
            x = 0;
        }

        GetSprite();
    }


}

