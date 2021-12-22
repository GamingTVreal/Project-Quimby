using System;
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
    int dateLevel;
    int bellyCapacity = 25;

    // Cache
    AIConversant girlConversant = null;
    Scheduler girlSchedule = null;

    private void Awake() 
    {
        girlConversant = GetComponent<AIConversant>();
        girlSchedule = GetComponent<Scheduler>();
    }
    private void Start()
    {
        // dateLevel = 0; //comment this out again
        // Debug.Log(dateLevel);
    }

    public void TalkWithGirl()
    {
        if(!hasMet)
        {
            // Get Speak Button object
            dateLevel = 0;
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

    public void IncreaseDateLevel()
    {
        dateLevel++;
    }

    internal void ModifyBellyCapacity(int amount)
    {
        bellyCapacity += amount;
    }

    internal int GetBellyCapacity()
    {
        return bellyCapacity;
    }

    public void ResetLocation()
    {
        girlSchedule.ResetLocation();
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
        //PlayerStats.Instance.Energy = 10000;
        //dateLevel = 3;
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
        saveRecord.bellyCapacity = bellyCapacity;
        return saveRecord;
    }

    public void RestoreState(object state)
    {
        CharRecord saveRecord = (CharRecord)state;
        hasMet = saveRecord.hasMet;
        dateLevel = saveRecord.dateLevel;
        bellyCapacity = saveRecord.bellyCapacity;
    }

    [System.Serializable]
    private class CharRecord
    {
        public bool hasMet;
        public int dateLevel;
        public int bellyCapacity;
    }
}
