using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Dialogue;
using ProjectQuimbly.Saving;
using UnityEngine;
using UnityEngine.UI;

public class JobAgency : MonoBehaviour, ISaveable
{
    [SerializeField] GameObject jobMenu;
    bool hasBeenToJobAgency = false;

    public object CaptureState()
    {
        return hasBeenToJobAgency;
    }

    public void RestoreState(object state)
    {
        hasBeenToJobAgency = (bool)state;
    }

    public void TalkToWorker()
    {
        Button button = GetComponent<Button>();
        button.interactable = false;
        if (hasBeenToJobAgency == true)
        {
            jobMenu.SetActive(true);
        }
        else
        {
            GetComponent<AIConversant>().StartDialogue();
            hasBeenToJobAgency = true;
        }

        button.interactable = true;
    }

}
