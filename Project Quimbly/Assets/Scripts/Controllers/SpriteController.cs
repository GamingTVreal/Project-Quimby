using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{

    public int x = 0;
    public int y = 0;
    public int z = 0;
    public Sprite[] Deb, Extras, foodBellies, BelliesS;
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

    // Called by feeding script on Girl Object
    public void UpdateFeedingSprite(int spriteLevel)
    {
        if(spriteLevel >= foodBellies.GetLowerBound(0) && spriteLevel <= foodBellies.GetUpperBound(0))
        {
            Feedee.sprite = foodBellies[spriteLevel];
        }
    }

    public void GetBelly()
    {
        if(y >= foodBellies.GetUpperBound(0))
        {
            y = foodBellies.GetUpperBound(0);
        }
        Feedee.sprite = foodBellies[y];
    }
    public void GetBellyS()
    {
        Feedee.sprite = BelliesS[z];
    }

}

