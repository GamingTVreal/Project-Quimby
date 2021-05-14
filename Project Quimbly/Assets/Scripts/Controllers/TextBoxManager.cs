
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class TextBoxManager : MonoBehaviour
{
    [SerializeField] GameObject SpriteImage;
    public GameObject Phone,NameBox,TextBox;
    public float speed = 0.1f;
    public TMP_Text thetext;
    public TMP_Text Speaker;
    public bool isactive;
    [SerializeField] Button Sleep;
    // Use this for initialization

    public SpriteController Sprite;
    public TextAsset textfile;
    public string[] textlines;
    private string TypedLine = "";
    public int currentline;
    public int endatline;
    public int NameLine;
    public string SpriteFinder;
    public int CurrentSprite;
    Coroutine showTextCoroutine = null;

    void Start()
    {
        if (textfile != null)
        {
            textlines = (textfile.text.Split('\n'));
        }

        if (endatline == 0)
        {
            endatline = textlines.Length - 1;
        }
        showTextCoroutine = StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {

        for (int i = 0; i < textlines[currentline].Length + 1; i++)
        {

            textlines[currentline] = textlines[currentline].Replace("Mark", BasicFunctions.Name);
            TypedLine = textlines[currentline].Substring(0, i);
            yield return new WaitForSeconds(speed);

 


        }

    }
    void Update()
    {
        textlines[NameLine] = textlines[NameLine].Replace("Mark", BasicFunctions.Name);
        if (textlines[NameLine].Contains("null"))
        {
            DisableNameBox();
        }
        else
        {
            EnableNameBox();
        }
        if (TextBox.activeSelf)
        {
            if (Phone != null) 
            {
                Phone.SetActive(false);
            }
            if (Sleep != null)
            {
               Sleep.interactable = false;
            }
            
            isactive = true;
        }
        else
        {
            Phone.SetActive(true);
        }
        if (isactive)
        {
            EnableTextBox();
            if(SpriteImage != null)
            {
             EnableSpriteImage();
            }
            
        }
        else
        {
            DisableSpriteImage();
            DisableTextBox();
        }
        CurrentSprite = currentline - 2;
        NameLine = currentline - 1;
        thetext.text = TypedLine;
        Speaker.text = textlines[NameLine];
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Sprite.GetSprite();
            StopCoroutine(showTextCoroutine);
            TypedLine = textlines[currentline];
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)  || Input.GetKeyDown(KeyCode.A) && TypedLine == textlines[currentline])
        {
            Sprite.GetSprite();
            showTextCoroutine = StartCoroutine(ShowText());
            currentline += 3;
        }

        if (currentline > endatline)

        {
            DisableSpriteImage(); 
            DisableTextBox();
        }
        else
        {
            EnableSpriteImage();
            EnableTextBox();
        }

    }

    public void NextLine()
    {
        showTextCoroutine = StartCoroutine(ShowText());
    }
    void EnableNameBox()
    {
        NameBox.SetActive(true);
    }
    void DisableNameBox()
    {
        NameBox.SetActive(false);
    }
    public void EnableTextBox()
    {
        TextBox.SetActive(true);
        isactive = true;
    }
    public void DisableTextBox()
    {
        NameBox.SetActive(false);
        TextBox.SetActive(false);
        isactive = false;
       if(Phone != null)
        {
            Phone.SetActive(true);
        } 
       if(Sleep != null)
        {
            Sleep.interactable = true;
        }
        
    }
    public void EnableSpriteImage()
    {
        SpriteImage.SetActive(true);
        Sprite.GetSprite();
    }
    public void DisableSpriteImage()
    {
        SpriteImage.SetActive(false);
    }
    public void Letsseeifitworks()
    {
        StartCoroutine(ShowText());
    }
    public void ReloadScript(TextAsset NewText)
    {
       if(NewText != null)
        {
            textlines = new string[1];
            textlines = (NewText.text.Split('\n'));
        }
    }
}
