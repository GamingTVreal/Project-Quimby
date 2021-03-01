using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private SpriteController spriteController;
    [SerializeField] private TextEvents _textEvents;
    public int Character = 0;
    
    private string[] Names = new string [5];
    private string[] Biography = new string [5];
    private int[] Age = new int [5];
    private string[] CupSize = new string [5];
    private static bool[] UnlockedGirls = new bool [5];
    bool UnlockGirl;

    public float fullness;
    public int happiness;
    public float interestlevel;
    

    // Start is called before the first frame update
    void Start()
    {
        if(UnlockedGirls == null)
        {
            UnlockedGirls[0] = false;
        }
        UnlockGirl = true;
        if(UnlockGirl == true)
        {
            UnlockedGirls[0] = true;
            SetCharacters();
        }
        UnlockGirl = false;
        Debug.Log(UnlockedGirls[0]);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TalkToDeb()
    {

        switch (Character)
        {
            case 0:
                _textEvents.MeetingDeb();
                spriteController.GetSprite();
                break;
        }
    }
    void SetCharacters()
    {
        switch (Character)
        {
            case 0:
                if (UnlockedGirls[0] == true)
                {
                    Names[Character] = "Deb";
                    Biography[Character] = "Deb is the debug girl of your dreams :)";
                    Age[Character] = 21;
                    CupSize[Character] = "D";
                }

                break;
        }
    }
}
        

         
