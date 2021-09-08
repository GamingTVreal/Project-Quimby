using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectQuimbly.Dialogue;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using ProjectQuimbly.UI;

namespace ProjectQuimbly.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] Image characterImage;
        [SerializeField] TextMeshProUGUI conversantName;
        [SerializeField] TextMeshProUGUI dialogueText;
        [SerializeField] GameObject textContainer;
        [SerializeField] GameObject speakerContainer;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] AudioSampleDB audioSampleDB;
        [SerializeField] MusicTrackDB musicTrackDB;

        List<string> hotkeys = new List<string>();
        List<Button> choiceButtons = new List<Button>();
        int choiceCount;
        bool isWaitingOnChoice = false;

        // Action for the end of the coroutine
        public event Action textboxCloseEvent;
        string parsedContent;
        bool isScriptComplete;

        private void Awake()
        {
            foreach (Transform item in choiceRoot)
            {
                choiceButtons.Add(item.GetComponent<Button>());
                item.gameObject.SetActive(false);
            }

            // Hotkey list for choice buttons
            hotkeys = new List<string> {"Button0", "Button1", "Button2", "Button3", "Button4"};

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
            if(!textContainer.activeSelf)
            {
                StartCoroutine(ShowText());
            }
        }

        // Print current node's dialog to textbox
        IEnumerator ShowText()
        {
            textContainer.SetActive(true);
            speakerContainer.SetActive(true);
            characterImage.gameObject.SetActive(true);
            SetupTextboxGroup();

            // float timeSinceLastSubstring = Mathf.Infinity;

            while (playerConversant.IsActive())
            {
                if(playerConversant.IsChoosing())
                {
                    if(!isWaitingOnChoice)
                    {
                        ReturnChoicesToPool(choiceCount);
                        BuildChoiceList();
                    }
                    for (int i = 0; i < choiceButtons.Count; i++)
                    {
                        if (choiceButtons[i].IsActive() && Input.GetButtonDown(hotkeys[i]))
                        {
                            choiceButtons[i].onClick.Invoke();
                        }
                    }
                }
                else
                {
                    // timeSinceLastSubstring += Time.deltaTime;
                    if (dialogueText.maxVisibleCharacters < parsedContent.Length)
                    {
                        dialogueText.maxVisibleCharacters++;
                    }
                    // Check if user is trying to advance text
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        AdvanceTextbox();
                    }
                }
                yield return null;
            }

            characterImage.gameObject.SetActive(false);
            textContainer.SetActive(false);
            speakerContainer.SetActive(false);
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
            textContainer.SetActive(true);
            dialogueText.text = ReplaceSubstringVariables(playerConversant.GetText());
            dialogueText.maxVisibleCharacters = 0;
            dialogueText.ForceMeshUpdate();
            parsedContent = dialogueText.GetParsedText();
        }

        private void BuildChoiceList()
        {
            choiceCount = 0;
            foreach (DialogueNode choiceNode in playerConversant.GetChoices())
            {
                Button button;
                if (choiceButtons.Count > choiceCount)
                {
                    button = choiceButtons[choiceCount];
                    button.gameObject.SetActive(true);
                }
                else
                {
                    GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                    button = choiceInstance.GetComponent<Button>();
                    choiceButtons.Add(button);
                }
                button.onClick.AddListener(() =>
                {
                    isWaitingOnChoice = false;
                    playerConversant.SelectChoice(choiceNode);
                    SetupTextboxGroup();
                    ReturnChoicesToPool();
                });
                TextMeshProUGUI buttonTextComponent = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonTextComponent.text = ReplaceSubstringVariables(choiceNode.GetText());
                choiceCount++;
            }

            speakerContainer.SetActive(false);
            textContainer.SetActive(false);
            choiceRoot.gameObject.SetActive(true);
            isWaitingOnChoice = true;
        }

        private void ReturnChoicesToPool()
        {
            foreach (Transform child in choiceRoot)
            {
                child.gameObject.SetActive(false);
            }
            choiceRoot.gameObject.SetActive(false);
        }

        private void ReturnChoicesToPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].gameObject.SetActive(false);
            }
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

        public void PlayAudioSample(string[] audioString)
        {
            AudioClip clip = audioSampleDB.GetAudioClip(audioString[0], audioString[1]);
            AudioSource source = GameObject.FindWithTag("GameController").GetComponent<AudioSource>();
            if(source != null)
            {
                source.Stop();
                source.clip = clip;
                source.Play();
            }
        }

        public void ChangeMusicTrack(string[] audioString)
        {
            AudioClip song = musicTrackDB.GetAudioClip(audioString[0]);
            AudioSource source = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
            if(source != null)
            {
                source.Stop();
                source.clip = song;
                source.Play();
            }
        }

        public void LoadScene(string[] newScene)
        {
            string sceneToLoad = newScene[0];
            GetComponent<LoadingScreenScript>().LoadNewArea(sceneToLoad);
        }
    }
}
