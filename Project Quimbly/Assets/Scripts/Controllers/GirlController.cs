using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Dialogue;
using ProjectQuimbly.Saving;
using ProjectQuimbly.Schedules;
using UnityEngine;
using UnityEngine.UI;

public class GirlController : MonoBehaviour, ISaveable
{
    // Character variables
    bool hasMet = false;
    static int dateLevel;

    // Cache
    AIConversant girlConversant = null;

    private void Awake() 
    {
        girlConversant = GetComponent<AIConversant>();
    }

    public void TalkWithGirl()
    {
        if(!hasMet)
        {
            // Get Speak Button object
            transform.GetChild(0).GetComponent<Button>().interactable = false;
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
        return dateLevel;
    }

    public void IncreaseDateLevel(int amount)
    {
        dateLevel = amount;
    }

    public void TryFeeding()
    {
        if(PlayerStats.Instance.Energy >= 15 && dateLevel > 2)
        {
            PlayerStats.Instance.Energy -= 15;
            girlConversant.StartDialogue("Feeding");
        }
        else if(dateLevel < 3)
        {
            girlConversant.StartDialogue("InflateBeforeDate");
        }
        else
        {
            Dialogue errorDialogue = (Dialogue)Resources.Load("Dialogue/ErrorMessages");
            girlConversant.StartDialogue(errorDialogue, "Feeding No Energy");
        }
    }

    public void TryInflation()
    {
        if(PlayerStats.Instance.Energy >= 15 && dateLevel > 2)
        {
            PlayerStats.Instance.Energy -= 15;
            girlConversant.StartDialogue("Inflation");
        }
        else if (dateLevel < 3)
        {
            girlConversant.StartDialogue("InflateBeforeDate");
        }
        else
        {
            Dialogue errorDialogue = (Dialogue)Resources.Load("Dialogue/ErrorMessages");
            girlConversant.StartDialogue(errorDialogue, "Low Energy");
        }
    }

    public void TryDating()
    {
        if (PlayerStats.Instance.Energy >= 5 && PlayerStats.Instance.Money >= 50 && dateLevel < 3)
        {
            Scheduler schedule = GetComponent<Scheduler>();
            if(schedule != null)
            {
                schedule.ChangeLocation("Dates");
            }
            girlConversant.StartDialogue("Date");
        }
        else if (dateLevel >= 3)
        {
            girlConversant.StartDialogue("AllDatesComplete");
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
        CharRecord saveRecord = new CharRecord();
        saveRecord.hasMet = hasMet;
        saveRecord.dateLevel = dateLevel;
        return saveRecord;
    }

    public void RestoreState(object state)
    {
        CharRecord saveRecord = (CharRecord)state;
        hasMet = saveRecord.hasMet;
        dateLevel = saveRecord.dateLevel;
    }

    [System.Serializable]
    private class CharRecord
    {
        public bool hasMet;
        public int dateLevel;
    }
}
