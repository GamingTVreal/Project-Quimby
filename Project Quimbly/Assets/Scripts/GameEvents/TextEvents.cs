using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class TextEvents : MonoBehaviour
{
    public TextAsset NextText;
    public int StartLine;
    bool MetDeb = false;
    [SerializeField] GameObject MainCamera, MenuCamera;
    [SerializeField] Button SpeakButton;
    public TextBoxManager TextBox;
    
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
        Invoke("EnableGirlMenu", 4f);
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
        if(SpeakButton.interactable == false && TextBox.endatline < TextBox.currentline)
        {
            SpeakButton.interactable = true;
        }
    }

    public void SmallTalk()
    {
        int Descussion;
        Descussion = Random.Range(0,6);
        switch (Descussion)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }
}
