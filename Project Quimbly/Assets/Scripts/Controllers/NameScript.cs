using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectQuimbly.Dialogue;

public class NameScript : MonoBehaviour
{
    public GameObject nameMenu;
    public TMP_InputField characterName;
    [SerializeField] Dialogue tutorialDialogue;

    void Start()
    {
        if (BasicFunctions.Name != null)
        {
            if(nameMenu != null)
            {
                characterName = null;
                nameMenu.SetActive(false);
                // Debug.Log(BasicFunctions.Name);
            }
        }
    }

   public void SelectName()
    {
        BasicFunctions.Name = characterName.text;
        if (BasicFunctions.Name != null)
        {
            AIConversant conversant = GetComponent<AIConversant>();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerConversant>().StartDialogue(conversant, tutorialDialogue);
            nameMenu.SetActive(false);
        }
    }
}
