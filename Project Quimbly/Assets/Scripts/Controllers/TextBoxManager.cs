﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class TextBoxManager : MonoBehaviour
{
    public GameObject NameBox;
    public GameObject Phone;
    public GameObject TextBox;
    public float speed = 0.1f;
    public TMP_Text thetext;
    public TMP_Text Speaker;
    public bool isactive;
    // Use this for initialization

    BasicFunctions Basic;
    public TextAsset textfile;
    public string[] textlines;
    private string TypedLine = "";
    public int currentline;
    public int endatline;
    public int NameLine;
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

        for (int i = 0; i < textlines[currentline].Length; i++)
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
            Phone.SetActive(false);
            isactive = true;
        }
        else
        {
            Phone.SetActive(true);
        }
        if (isactive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

        NameLine = currentline - 1;
        thetext.text = TypedLine;
        Speaker.text = textlines[NameLine];
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A))
        {
            StopCoroutine(showTextCoroutine);
            TypedLine = textlines[currentline];
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A) && TypedLine == textlines[currentline])
        {

            showTextCoroutine = StartCoroutine(ShowText());
            currentline += 2;
            NameLine += 2;
        }

        if (currentline > endatline)

        {
            DisableTextBox();
        }

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
    }
    public void DisableTextBox()
    {
        NameBox.SetActive(false);
        TextBox.SetActive(false);
        Phone.SetActive(true);
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
