using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEvents : MonoBehaviour
{

    public TextAsset NextText;
    public int StartLine;
    int x = 0;
    bool MetDeb = false,BeenToJobAgency = false;
    [SerializeField] AudioSource SnoringSFX, Music;
    [SerializeField] Animator Fadeout;
    [SerializeField] GameObject MainCamera, MenuCamera, JobMenu;
    [SerializeField] Button SpeakButton, ChatButton;
    public TextBoxManager TextBox;
    public TextBoxManager TB2;
    public PlayerStats Player;
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
        Descussion = Random.Range(0,6);
        
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
                PlayerStats.Instance.AdjustEnergy(20);
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
        TextBox.currentline = 2;
        TextBox.endatline = 51;
    }
        
    
}
