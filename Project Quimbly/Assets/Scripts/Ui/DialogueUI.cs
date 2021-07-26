using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectQuimbly.Dialogue;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using ProjectQuimbly.UI;

namespace ButtonGame.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] Image characterImage;
        [SerializeField] TextMeshProUGUI conversantName;
        [SerializeField] TextMeshProUGUI dialogueText;
        [SerializeField] GameObject dialogueContainer;
        [SerializeField] GameObject speakerContainer;

        // Action for the end of the coroutine
        public event Action textboxCloseEvent;
        string parsedContent;
        bool isScriptComplete;

        private void Awake() 
        {
            playerConversant = GetComponent<PlayerConversant>();
            playerConversant.onConversationStart += StartCoroutine;
        }

        // Start is called before the first frame update
        void Start()
        {
            // UpdateUI();
        }

        private void StartCoroutine()
        {
            if(!dialogueContainer.activeSelf)
            {
                StartCoroutine(ShowText());
            }
        }

        // Print current node's dialog to textbox
        IEnumerator ShowText()
        {
            dialogueContainer.SetActive(true);
            characterImage.gameObject.SetActive(true);
            SetupTextboxGroup();

            // float timeSinceLastSubstring = Mathf.Infinity;

            while (playerConversant.IsActive())
            {
                // timeSinceLastSubstring += Time.deltaTime;
                if(dialogueText.maxVisibleCharacters < parsedContent.Length)
                {
                    dialogueText.maxVisibleCharacters++;
                }
                // Check if user is trying to advance text
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    AdvanceTextbox();
                }
                yield return null;
            }

            characterImage.gameObject.SetActive(false);
            dialogueContainer.SetActive(false);
        }

        private void AdvanceTextbox()
        {
            if (dialogueText.maxVisibleCharacters < parsedContent.Length)
            {
                dialogueText.maxVisibleCharacters = parsedContent.Length;
            }
            else
            {
                if (playerConversant.HasNext())
                {
                    playerConversant.Next();
                    SetupTextboxGroup();
                }
                else
                {
                    playerConversant.Quit();
                }
            }
        }

        private void SetupTextboxGroup()
        {
            if (!playerConversant.IsActive()) return;

            SetNameAndSprite();
            dialogueText.text = ReplaceSubstringVariables(playerConversant.GetText());
            dialogueText.maxVisibleCharacters = 0;
            dialogueText.ForceMeshUpdate();
            parsedContent = dialogueText.GetParsedText();
        }

        private string ReplaceSubstringVariables(string sInput)
        {
            if(sInput == null) return "";
            string sModified = sInput;
            sModified = sModified.Replace("[p]", BasicFunctions.Name);
            sModified = sModified.Replace("[]", "    ");
            return sModified;
        }

        private void SetNameAndSprite()
        {
            Sprite newSprite = playerConversant.GetSprite();
            Color c = characterImage.color;
            if (newSprite != null)
            {
                characterImage.sprite = newSprite;
                characterImage.color = new Color(c.r, c.g, c.b, 255);
            }
            else
            {
                characterImage.color = new Color(c.r, c.g, c.b, 0);
            }

            string speakerName = playerConversant.GetCurrentConversantName();
            if (speakerName == "")
            {
                speakerContainer.SetActive(false);
            }
            else
            {
                speakerContainer.SetActive(true);
                conversantName.text = speakerName;
            }
        }

        public void ChangeBackground(string[] newPhoto)
        {
            Sprite photo;
            BGPhotoDB bgPhotoDB = (BGPhotoDB)Resources.Load("Game/BGPhotoDB");
            if(bgPhotoDB != null)
            {
                photo = bgPhotoDB.GetSprite(newPhoto[0]);
                Image bgImage = GameObject.FindGameObjectWithTag("BackgroundImage").GetComponent<Image>();
                bgImage.sprite = photo;
            }
        }

        public void LoadScene(string[] newScene)
        {
            string sceneToLoad = newScene[0];
            GetComponent<LoadingScreenScript>().LoadNewArea(sceneToLoad);
        }
    }
}
