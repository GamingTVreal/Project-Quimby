using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Dialogue;
using ProjectQuimbly.Saving;
using UnityEngine;
using UnityEngine.UI;

public class GirlController : MonoBehaviour, ISaveable
{
    // Character variables
    bool hasMet = false;
    static int DateLevel;

    // Cache
    AIConversant girlConversant = null;

    private void Awake() 
    {
        GameObject go = transform.GetChild(0).gameObject;
        girlConversant = go.GetComponent<AIConversant>();
    }

    public void TalkWithGirl()
    {
        if(!hasMet)
        {
            // Get Speak Button object
            girlConversant.GetComponent<Button>().interactable = false;
            girlConversant.StartDialogue();
            hasMet = true;

            girlConversant.onConversationEnd += EnableSpeakButton;
        }
        else
        {
            // Child 1 is interact menu
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public int GetDateLevel()
    {
        return DateLevel;
    }
    public void IncreaseDateLevel(int amount)
    {
        DateLevel = amount;
    }
    public void TryFeeding()
    {
        if(PlayerStats.Instance.Energy >= 15)
        {
            PlayerStats.Instance.Energy -= 15;
            girlConversant.StartDialogue("Feeding");
        }
        else
        {
            Dialogue errorDialogue = (Dialogue)Resources.Load("Dialogue/ErrorMessages");
            girlConversant.StartDialogue(errorDialogue, "Feeding No Energy");
        }
    }

    public void TryInflation()
    {
        if(PlayerStats.Instance.Energy >= 15)
        {
            PlayerStats.Instance.Energy -= 15;
            girlConversant.StartDialogue("Inflation");
        }
        else
        {
            Dialogue errorDialogue = (Dialogue)Resources.Load("Dialogue/ErrorMessages");
            girlConversant.StartDialogue(errorDialogue, "Low Energy");
        }
    }
    public void TryDating()
    {
        if (PlayerStats.Instance.Energy >= 5 && PlayerStats.Instance.Money >= 50)
        {
            girlConversant.StartDialogue("Date");
        }
        else
        {
            
            girlConversant.StartDialogue("CannotAffordDate");
        }
    }

    public void EnableSpeakButton()
    {
        transform.GetChild(0).GetComponent<Button>().interactable = true;
        girlConversant.onConversationEnd -= EnableSpeakButton;
    }

    public object CaptureState()
    {
        return hasMet;
    }

    public void RestoreState(object state)
    {
        hasMet = (bool)state;
    }
}
