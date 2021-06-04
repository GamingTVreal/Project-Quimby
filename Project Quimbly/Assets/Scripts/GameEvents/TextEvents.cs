using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEvents : MonoBehaviour
{

    public TextAsset ErrorMessages;
    public int StartLine;
    int x = 0;
    static bool MetDeb = false;
    bool BeenToJobAgency = false;
    [SerializeField] AudioSource SnoringSFX, Music;
    [SerializeField] Animator Fadeout;
    [SerializeField] GameObject MainCamera, MenuCamera, JobMenu, DebMenu;
    [SerializeField] Button SpeakButton, ChatButton;
    public TextBoxManager TextBox;
    public TextBoxManager TB2;
    public PlayerStats Player;
    public LoadingScreenScript Load;
    

    public void Start()
    {
    }
    public void MeetingDeb()
    {
        
        
         if (MetDeb == true)
        {
            GirlMenu();
        }
        else if (MetDeb == false)
        {
            SpeakButton.interactable = false;
            TextBox.EnableSpriteImage();
            TextBox.EnableTextBox();
            TextBox.currentline = 2;
            TextBox.endatline = 51;
            MetDeb = true;
        }
        
    }
    public void GirlMenu()
    {
        MainCamera.SetActive(false);
        MenuCamera.SetActive(true);
    }
    private void EnableGirlMenu()
    {
        
    }
    public void Feeding()
    {
        GameObject FeedingButton = GameObject.Find("Feed");
        FeedingButton.SetActive(false);
        TB2.isactive = true;
        TB2.EnableSpriteImage();
        TB2.EnableTextBox();
        TB2.currentline = 236;
        TB2.endatline = 251;
        feeding2();

        
    }

    void feeding2()
    {
        if (PlayerStats.Instance.Energy > 5)
        {
            if (TB2.isactive == false)
            {
                PlayerStats.Instance.Energy = PlayerStats.Instance.Energy - 5;
                Load.FeedingRoom();
            }
            else
            {
                Invoke("feeding2", 1f);
            }
        }
        else
        {
            TB2.ReloadScript(ErrorMessages);
            TB2.textfile = ErrorMessages;
            TB2.isactive = true;
            TB2.EnableSpriteImage();
            TB2.EnableTextBox();
            TB2.currentline = 27;
            TB2.endatline = 34;
            Leave2();
        }

    }

    public void Leave()
    {
        TextBox.isactive = true;
        TextBox.EnableSpriteImage();
        TextBox.EnableTextBox();
        TextBox.currentline = 40;
        TextBox.endatline = 61;
        Leave2();
        Debug.Log("I made it here :)");
    }
    void Leave2()
    {
        if (TextBox.isactive == false && TB2.isactive == false || TB2 == null)
        {
            Load.Home();
        }
        else
        {
            Invoke("Leave2", 1f);
        }
    }
    private void FixedUpdate()
    {
        if (SpeakButton != null)
        {
            if (SpeakButton.interactable == false && TextBox.endatline < TextBox.currentline)
            {
                SpeakButton.interactable = true;
            }
        }
    }


    public void SmallTalk()
    {
        ChatButton.interactable = false;
        int Descussion;
        Descussion = UnityEngine.Random.Range(0,6);
        
        switch (Descussion)
        {
            case 0:
                TB2.NextLine();
                Debug.Log("Case0");
                TB2.EnableSpriteImage();
                TB2.EnableTextBox();
                TB2.endatline = 90;
                TB2.currentline = 56;
                TB2.isactive = true;
                TB2.NextLine();
                break;

            case 1:
                Debug.Log("Case1");
                TB2.EnableSpriteImage();
                TB2.EnableTextBox();
                TB2.endatline = 156;
                TB2.currentline = 131;
                TB2.isactive = true;
                TB2.NextLine();
                break;
            case 2:
                Debug.Log("Case2");
                TB2.EnableSpriteImage();
                TB2.EnableTextBox();
                TB2.endatline = 126;
                TB2.currentline = 95;
                TB2.isactive = true;
                TB2.NextLine();
                PlayerStats.Instance.AdjustMoney(100000);
                break;
            case 3:
                Debug.Log("Case3");
                TB2.EnableSpriteImage();
                TB2.EnableTextBox();
                TB2.endatline = 179;
                TB2.currentline = 161;
                TB2.isactive = true;
                TB2.NextLine();
                break;

            case 4:
                Debug.Log("Case4");
                TB2.EnableSpriteImage();
                TB2.EnableTextBox();
                TB2.endatline = 90;
                TB2.currentline = 56;
                TB2.isactive = true;
                TB2.NextLine();
                break;
            case 5:
                Debug.Log("Case5");
                TB2.EnableSpriteImage();
                TB2.EnableTextBox();
                TB2.endatline = 212;
                TB2.currentline = 184;
                TB2.isactive = true;
                TB2.NextLine();
                break;
            case 6:
                Debug.Log("Case6");
                TB2.EnableSpriteImage();
                TB2.EnableTextBox();
                TB2.endatline = 232;
                TB2.currentline = 216 ;
                TB2.isactive = true;
                TB2.NextLine();
                break;
        }
        ChatButton.interactable = true;
    }
    public void Sleep()
    {
         
        Fadeout.SetBool("Fadeout", true);
        if (Fadeout.GetBool("Fadeout") == true)
        {
            if (x != 4)
            {
                Music.Pause();
                SnoringSFX.Play();
                x = 4;
            }

            if (SnoringSFX.isPlaying)
            {
                Invoke("Sleep", 4f);
            }
            if (!SnoringSFX.isPlaying) 
            {
                PlayerStats.Instance.AdjustEnergy(1,true);
                Fadeout.SetBool("Fadeout", false);
                x = 0;
                Music.Play();
            }
        }
    }
    public void JobAgency()
    {
        SpeakButton.interactable = false;
        if (BeenToJobAgency == true)
        {
            JobMenu.SetActive(true);
        }
        else
        {
            TextBox.EnableTextBox();
            TextBox.currentline = 2;
            TextBox.endatline = 15;
            BeenToJobAgency = true;
        }
        SpeakButton.interactable = true;
    }
    public void NoEnergy()
    {
        TextBox.EnableTextBox();
        TextBox.ReloadScript(ErrorMessages);
        TextBox.CurrentSprite = 1;
        TextBox.NameLine = 2;
        TextBox.currentline = 3;
        TextBox.endatline = 10;
    }
    public void NotEnoughEnergy()
    {
        TextAsset OldText;
        OldText = TextBox.textfile;
        TextBox.EnableTextBox();
        TextBox.ReloadScript(ErrorMessages);
        TextBox.textfile = ErrorMessages;
        TextBox.CurrentSprite = 12;
        TextBox.NameLine = 13;
        TextBox.currentline = 14;
        TextBox.endatline = 15;
        TextBox.textfile = OldText;
    }
    public void GoToWork()
    {
        if (PlayerStats.Instance.CurrentJob != 0)
        {
            if (PlayerStats.Instance.Energy == 0)
            {
                NoEnergy();
            }
            else if (PlayerStats.Instance.Energy < 15 && PlayerStats.Instance.Energy != 0)
            {
                NotEnoughEnergy();
            }
            else
            {
                PlayerStats.Instance.Energy = PlayerStats.Instance.Energy - 15;
                Load.Work();
            }
        }
        else
        {
            TextBox.ReloadScript(ErrorMessages);
            TextBox.EnableTextBox();
            TextBox.CurrentSprite = 17;
            TextBox.NameLine = 18;
            TextBox.currentline = 19;
            TextBox.endatline = 22;
        }

    }
    public void TooFullToContinue()
    {
        bool talked = false;
        if (TextBox.isactive == false && talked == false)
        {

            TextBox.isactive = true;
            TextBox.EnableTextBox();
            TextBox.currentline = 17;
            TextBox.endatline = 35;
            talked = true;
        }
        TooFullToContinue2(); 
        
    }
    public void TooFullToContinue2()
    {
        
        if (TextBox.isactive == false)
        {
            Load.Home();
        }
        else
        {
            Invoke("TooFullToContinue2", 1f);
        }
        

    }
}
