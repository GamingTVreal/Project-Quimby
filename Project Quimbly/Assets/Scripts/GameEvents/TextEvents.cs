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
    [SerializeField] GameObject MainCamera, MenuCamera, DebMenu,FeedingStuff;
    [SerializeField] Button SpeakButton, ChatButton;
    public TextBoxManager TextBox;
    public TextBoxManager TB2;
    public PlayerStats Player;
    public LoadingScreenScript Load;
    static bool talked, MaxFed = false;
    
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

    public void Sleep()
    {
        if (PlayerStats.Instance.Energy < 20)
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
                    Invoke("Sleep", 0.25f);
                }
                if (!SnoringSFX.isPlaying)
                {
                    PlayerStats.Instance.AdjustEnergy(1, true);
                    Fadeout.SetBool("Fadeout", false);
                    x = 0;
                    Music.Play();
                }
            }
        }
        else
        {
            GetComponent<AIConversant>().StartDialogue("Can't Sleep");
        }
    }

    public void LeavingInflation()
    {

    }
    public void FeedingSmalltalk()
    {


    }
}
