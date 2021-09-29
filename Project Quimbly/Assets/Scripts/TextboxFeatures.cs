using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextboxFeatures : MonoBehaviour
{
    [SerializeField] GameObject Textbox, Namebox, Textbox2;
    [SerializeField] TMP_Text DefaultText;
    public string WrittenText, PreviousText;
    public bool Hidden;
    // Start is called before the first frame update
    void Start()
    {
        WrittenText = DefaultText.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            HideTextbox();
        }
    }
    public void CheckText()
    {
        PreviousText = DefaultText.text;
        if (PreviousText == WrittenText)
        {
            
            DefaultText.text = "New Text";
        }
        else
        {
            WrittenText = DefaultText.text;
        }
        
    }
    public void HideTextbox()
    {
        if (Textbox.activeInHierarchy == true && Namebox.activeInHierarchy == true)
        {
            Textbox.SetActive(false);
            Namebox.SetActive(false);
            Hidden = true;
        }
        else if (Textbox2.activeInHierarchy == true)
        {
            Textbox2.SetActive(false);
        }
        else if (Textbox.activeInHierarchy == false && Namebox.activeInHierarchy == false && WrittenText != "New Text")
        {
            Textbox.SetActive(true);
            Namebox.SetActive(true);
            Hidden = false;
        }
        else if (Textbox2.activeInHierarchy == true)
        {
            Textbox2.SetActive(true);
        }

        Debug.Log(WrittenText);
    }
}
