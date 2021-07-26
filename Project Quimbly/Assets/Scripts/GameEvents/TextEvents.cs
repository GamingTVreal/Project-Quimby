using System;
using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Dialogue;
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
    [SerializeField] GameObject MainCamera, MenuCamera, JobMenu, DebMenu,FeedingStuff;
    [SerializeField] Button SpeakButton, ChatButton;
    public TextBoxManager TextBox;
    public TextBoxManager TB2;
    public PlayerStats Player;
    public LoadingScreenScript Load;
    static bool talked, MaxFed = false;

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
            SpeakButton.GetComponent<AIConversant>().StartDialogue();
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
        TB2.currentline = 236;
        TB2.endatline = 251;
        TB2.ReloadScript();
        feeding2();
    }

    void feeding2()
    {
        if (PlayerStats.Instance.Energy >= 15)
        {
            if (TB2.isTextboxActive == false)
            {
                PlayerStats.Instance.Energy = PlayerStats.Instance.Energy - 15;
                DebMenu.SetActive(false);
                Load.FeedingRoom();
            }
            else
            {
                // Textbox is active, subscribe to close event from TB Manager to exit
                TB2.textboxCloseEvent += feeding2;
            }
        }
        else
        {
            TB2.currentline = 256;
            TB2.endatline = 257;
            TB2.ReloadScript();
            Leave2();
        }

    }

    public void Leave()
    {
        FeedingStuff.SetActive(false);
        TextBox.currentline = 40;
        TextBox.endatline = 61;
        TextBox.ReloadScript();
        Leave2();
        Debug.Log("I made it here :)");
    }
    void Leave2()
    {
        if (TextBox.isTextboxActive == false && TB2.isTextboxActive == false || TextBox.isTextboxActive == false && TB2 == null)
        {
            if (DebMenu != null)
            {
                DebMenu.SetActive(false);
            }
            Load.Home();
        }
        else
        {
            // Textbox is active, subscribe to close event from TB Manager to exit
            TB2.textboxCloseEvent += Leave2;
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
                TB2.endatline = 90;
                TB2.currentline = 56;
                TB2.ReloadScript();
                TB2.NextLine();
                break;

            case 1:
                Debug.Log("Case1");
                TB2.endatline = 156;
                TB2.currentline = 131;
                TB2.ReloadScript();
                TB2.NextLine();
                break;
            case 2:
                Debug.Log("Case2");
                TB2.endatline = 126;
                TB2.currentline = 95;
                TB2.ReloadScript();
                TB2.NextLine();
                PlayerStats.Instance.AdjustMoney(100000);
                break;
            case 3:
                Debug.Log("Case3");
                TB2.endatline = 179;
                TB2.currentline = 161;
                TB2.ReloadScript();
                TB2.NextLine();
                break;

            case 4:
                Debug.Log("Case4");
                TB2.endatline = 90;
                TB2.currentline = 56;
                TB2.ReloadScript();
                TB2.NextLine();
                break;
            case 5:
                Debug.Log("Case5");
                TB2.endatline = 212;
                TB2.currentline = 184;
                TB2.ReloadScript();
                TB2.NextLine();
                break;
            case 6:
                Debug.Log("Case6");
                TB2.endatline = 232;
                TB2.currentline = 216;
                TB2.ReloadScript();
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
            TextBox.currentline = 2;
            TextBox.endatline = 15;
            TextBox.ReloadScript();
            BeenToJobAgency = true;
        }
        SpeakButton.interactable = true;
    }
    public void NoEnergy()
    {
        TextBox.currentline = 3;
        TextBox.endatline = 10;
        TextBox.ReloadScript(ErrorMessages);
    }
    public void NotEnoughEnergy()
    {
        TextBox.currentline = 14;
        TextBox.endatline = 15;
        TextBox.ReloadScript(ErrorMessages);
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
            // Set line range, pass in script to display
            TextBox.currentline = 19;
            TextBox.endatline = 19;
            TextBox.ReloadScript(ErrorMessages);
        }
    }

    public void HighestFullnessValue()
    {
        FeedingStuff.SetActive(false);
        if (MaxFed == false)
        {
            TextBox.currentline = 193;
            TextBox.endatline = 247;
            TextBox.ReloadScript();
            MaxFed = true;
        }
        else
        {
            TextBox.currentline = 252;
            TextBox.endatline = 270;
            TextBox.ReloadScript();
        }
        TooFullToContinue2();
    }
    public void TooFullToContinue()
    {
        //talked = true;
        FeedingStuff.SetActive(false);
        int x = UnityEngine.Random.Range(0, 6);
        if (TextBox.isTextboxActive == false && talked == false)
        {
            TextBox.currentline = 17;
            TextBox.endatline = 35;
            TextBox.ReloadScript();
            talked = true;
        }
        else if (TextBox.isTextboxActive == false && talked == true)
        {
            switch (x)
            {
                case 0:
                    TextBox.currentline = 17;
                    TextBox.endatline = 23;
                    TextBox.ReloadScript();
                    break;
                case 1:
                    TextBox.currentline = 66;
                    TextBox.endatline = 102;
                    TextBox.ReloadScript();
                    break;
                case 2:
                    TextBox.currentline = 107;
                    TextBox.endatline = 125;
                    TextBox.ReloadScript();
                    break;
                case 3:
                    TextBox.currentline = 130;
                    TextBox.endatline = 145;
                    TextBox.ReloadScript();
                    break;
                case 4:
                    TextBox.currentline = 150;
                    TextBox.endatline = 171;
                    TextBox.ReloadScript();
                    break;
                case 5:
                    TextBox.currentline = 176;
                    TextBox.endatline = 188;
                    TextBox.ReloadScript();
                    break;
            }
        }

        TooFullToContinue2(); 
        
    }
    public void TooFullToContinue2()
    {
        
        if (TextBox.isTextboxActive == false)
        {
            Load.Home();
        }
        else
        {
            Invoke("TooFullToContinue2", 1f);
        }
        

    }

    public void FeedingSmalltalk()
    {


    }
}
