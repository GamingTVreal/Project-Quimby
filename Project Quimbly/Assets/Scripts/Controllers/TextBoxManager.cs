
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
    public float speed = 1f;
    public TextMeshProUGUI textboxText;
    public TextMeshProUGUI speakerText;
    public bool isTextboxActive = false;
    [SerializeField] Button Sleep;
    // Use this for initialization

    public SpriteController Sprite;
    public TextAsset textfile;
    public string[] defaultTextlines;
    private string TypedLine = "";
    public int currentline;
    public int endatline;
    public int nameLine;
    public string SpriteFinder;
    public int currentSprite;
    Coroutine showTextCoroutine = null;

    // Action for the end of the coroutine
    public event Action textboxCloseEvent;
    string parsedContent;
    bool isScriptComplete;

    void Start()
    {
        StartDefaultDialogue();
    }

    private void StartDefaultDialogue()
    {
        if (textfile != null)
        {
            defaultTextlines = (textfile.text.Split('\n'));

            if (endatline == 0)
            {
                endatline = defaultTextlines.Length - 1;
            }

            if (!isTextboxActive)
            {
                showTextCoroutine = StartCoroutine(ShowText(defaultTextlines));
            }
        }
    }

    // Take parsed textlines and print them to textbox based on textspeed
    IEnumerator ShowText(string[] coTextLines)
    {
        EnableTextBox();
        SetupNextLine(coTextLines);

        float timeSinceLastSubstring = Mathf.Infinity;
        isScriptComplete = false;

        while (!isScriptComplete)
        {
            timeSinceLastSubstring += Time.deltaTime;
            if (textboxText.maxVisibleCharacters < parsedContent.Length && timeSinceLastSubstring > speed)
            {
                timeSinceLastSubstring = 0;
                textboxText.maxVisibleCharacters++;
            }

            // Check if user is trying to advance text
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                AdvanceTextBox(coTextLines);
            }
            yield return null;
        }

        DisableSpriteImage();
        DisableTextBox();
    }

    private void SetupNextLine(string[] coTextLines)
    {
        // Get sprite/name info
        SetSpriteField(coTextLines);
        SetNameField(coTextLines);
        coTextLines[currentline] = coTextLines[currentline].Replace("Mark", BasicFunctions.Name);

        textboxText.maxVisibleCharacters = 0;
        textboxText.text = coTextLines[currentline];
        textboxText.ForceMeshUpdate();
        parsedContent = textboxText.GetParsedText();
    }

    // Complete or advance text
    private void AdvanceTextBox(string[] coTextLines)
    {
        // If line is not complete, instantly fill in line
        if (textboxText.maxVisibleCharacters < parsedContent.Length)
        {
            textboxText.maxVisibleCharacters = parsedContent.Length;
        }
        // If already complete, load next line to display or close textbox
        else
        {
            currentline += 3;
            // Check if lines are left to display
            if (currentline <= endatline)
            {
                SetupNextLine(coTextLines);
            }
            else
            {
                isScriptComplete = true;
            }
        }
    }

    // Set sprite from script line
    private void SetSpriteField(string[] coTextLines)
    {
        // Try to parse sprite select from text file
        if (int.TryParse(coTextLines[currentline - 2], out currentSprite))
        {
            // 6 is used for "no sprite"
            if (currentSprite != 6)
            {
                EnableSpriteImage();
                Sprite.GetSprite(currentSprite);
            }
            else
            {
                DisableSpriteImage();
            }
        }
        else
        {
            DisableSpriteImage();
        }
    }

    // Set name from script line
    private void SetNameField(string[] coTextLines)
    {
        // Replace and display name or disable box on "null"
        nameLine = currentline - 1;
        string speakerName = coTextLines[nameLine];
        speakerName = speakerName.Replace("Mark", BasicFunctions.Name);
        speakerText.text = speakerName;

        if (speakerName.Contains("null"))
        {
            DisableNameBox();
        }
        else
        {
            EnableNameBox();
        }
    }

    // Maybe obsolete
    private void OnEnable()
    {
        // Check active states to display self
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
            // isTextboxActive = true;
        }
        else
        {
            Phone.SetActive(true);
        }

        // if (isactive)
        // {
        //     EnableTextBox();
        //     if (SpriteImage != null)
        //     {
        //         EnableSpriteImage();
        //     }
        // }
        // else
        // {
        //     DisableSpriteImage();
        //     DisableTextBox();
        // }
    }
    
    public void NextLine()
    {
        showTextCoroutine = StartCoroutine(ShowText(defaultTextlines));
    }

    void EnableNameBox()
    {
        NameBox.SetActive(true);
    }

    void DisableNameBox()
    {
        NameBox.SetActive(false);
    }

    private void EnableTextBox()
    {
        TextBox.SetActive(true);
        isTextboxActive = true;
    }

    private void DisableTextBox()
    {
        isTextboxActive = false;
        NameBox.SetActive(false);
        TextBox.SetActive(false);
        if(Phone != null)
        {
            Phone.SetActive(true);
        } 
        if(Sleep != null)
        {
            Sleep.interactable = true;
        }
        textboxCloseEvent?.Invoke();
    }

    public void EnableSpriteImage()
    {
        SpriteImage.SetActive(true);
    }

    public void DisableSpriteImage()
    {
        SpriteImage.SetActive(false);
    }

    public void Letsseeifitworks()
    {
        showTextCoroutine = StartCoroutine(ShowText(defaultTextlines));
    }

    // Start dialogue script with passed in script or scene default
    public void ReloadScript(TextAsset newText = null)
    {
        gameObject.SetActive(true);
        if (showTextCoroutine != null)
        {
            StopCoroutine(showTextCoroutine);
        }
        isTextboxActive = false;

        if(newText != null)
        {
            // Parse new text and begin dialogue
            string[] tempTextLines = new string[1];
            tempTextLines = (newText.text.Split('\n'));
            showTextCoroutine = StartCoroutine(ShowText(tempTextLines));
        }
        // Start dialogue with default text
        else
        {
            StartDefaultDialogue();
        }
    }
}
