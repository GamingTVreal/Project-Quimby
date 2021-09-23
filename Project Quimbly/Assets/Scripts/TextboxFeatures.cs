using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextboxFeatures : MonoBehaviour
{
    [SerializeField] GameObject Textbox, Namebox, Textbox2;
    [SerializeField] TMP_Text DefaultText;
    string WrittenText, PreviousText;
    bool Hideable;
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
        if (Input.GetKeyDown("enter") || Input.GetMouseButtonDown(0))
        {
            CheckText();
        }
    }
    void CheckText()
    {
        Debug.Log(WrittenText);
        PreviousText = DefaultText.text;
        Debug.Log(PreviousText);
        if (PreviousText == WrittenText)
        {
            
            DefaultText.text = "New Text";
        }
        else
        {
            WrittenText = DefaultText.text;
        }
        
    }
    void HideTextbox()
    {
        WrittenText = DefaultText.text;

        if (Textbox.activeInHierarchy == true && Namebox.activeInHierarchy == true)
        {
            Textbox.SetActive(false);
            Namebox.SetActive(false);
        }
        else if (Textbox2.activeInHierarchy == true)
        {
            Textbox2.SetActive(false);
        }
        else if (Textbox.activeInHierarchy == false && Namebox.activeInHierarchy == false && WrittenText != "New Text")
        {
            Textbox.SetActive(true);
            Namebox.SetActive(true);
        }
        else if (Textbox2.activeInHierarchy == true)
        {
            Textbox2.SetActive(true);
        }

    }
}
