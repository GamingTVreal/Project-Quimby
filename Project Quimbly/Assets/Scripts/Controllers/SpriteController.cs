using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{

    public int x = 0;
    public int y = 0;
    public Sprite[] Deb, Extras, Bellies;
    public TextBoxManager textboxmanager;
    public Image Feedee;
    private void Start()
    {

    }
    public void SetSprite()
    {
        this.GetComponent<Image>().sprite = Deb[x];
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
    public void GetBelly()
    {
        Feedee.sprite = Bellies[y];
    }


}

