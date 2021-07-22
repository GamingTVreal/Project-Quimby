using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{

    public int x = 0;
    public int y = 0;
    public int z = 0;
    public Sprite[] Deb, Extras, Bellies, BelliesS;
    public TextBoxManager textboxmanager;
    public Image Feedee;
    private void Start()
    {

    }
    public void SetSprite()
    {
        this.GetComponent<Image>().sprite = Deb[x];
    }
    public void GetSprite(int spriteSelect = 1)
    {
        x = spriteSelect - 1;
        
        SetSprite();
    }
    public void GetBelly()
    {
        Feedee.sprite = Bellies[y];
    }
    public void GetBellyS()
    {
        Feedee.sprite = BelliesS[z];
    }

}

