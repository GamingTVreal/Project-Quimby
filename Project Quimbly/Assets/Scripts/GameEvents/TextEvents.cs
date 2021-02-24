using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEvents : MonoBehaviour
{
    public TextAsset NextText;
    public int StartLine;
    bool MetDeb = false;


    public TextBoxManager TextBox;
    
    public void MeetingDeb()
    {
        if(MetDeb == false)
        {
            TextBox.EnableSpriteImage();
            TextBox.EnableTextBox();
            TextBox.currentline = 2;
            TextBox.endatline = 51;
        }
    }

}
