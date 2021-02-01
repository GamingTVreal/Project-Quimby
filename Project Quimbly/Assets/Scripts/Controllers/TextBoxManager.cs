
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class TextBoxManager : MonoBehaviour
{
    public GameObject Phone;
    public GameObject TextBox;
    public float speed = 0.1f;
    public TMP_Text thetext;
    public bool isactive;
    // Use this for initialization

    public TextAsset textfile;
    public string[] textlines;
    private string TypedLine = "";
    public int currentline;
    public int endatline;
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

            TypedLine = textlines[currentline].Substring(0, i);
            yield return new WaitForSeconds(speed);

 


        }

    }
    void Update()
    {
        if (isactive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

        thetext.text = TypedLine;
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A))
        {
            StopCoroutine(showTextCoroutine);
            TypedLine = textlines[currentline];
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A) && TypedLine == textlines[currentline])
        {

            showTextCoroutine = StartCoroutine(ShowText());
            currentline += 1;
        }

        if (currentline > endatline)

        {
            DisableTextBox();
        }

    }
    public void EnableTextBox()
    {
        TextBox.SetActive(true);
    }
    public void DisableTextBox()
    {
        TextBox.SetActive(false);
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
