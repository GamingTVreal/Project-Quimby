using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ProjectQuimbly.Saving;

public class JobAssigner : MonoBehaviour, ISaveable
{
    [SerializeField] TMP_Text Currentjob;
    int AssignedJob;
    public string CurrentAssignedJob;

    void OnEnable()
    {
        if(PlayerStats.Instance.CurrentJob != 0)
        {
            AssignedJob = PlayerStats.Instance.CurrentJob;
        }
        else
        {
            AssignedJob = 0;
        }
    }
    
    public void Dishwasher()
    {
        AssignedJob = 1;
        AssignJob();
    }

    public void AssignJob()
    {
        if(Currentjob == null) return;
        
        PlayerStats.Instance.CurrentJob = AssignedJob;

        switch (AssignedJob)
        {
            case 0:
                CurrentAssignedJob = "Unenployed";
                Currentjob.text = CurrentAssignedJob;
                break;
            case 1:
                CurrentAssignedJob = "Dishwasher";
                Currentjob.text = CurrentAssignedJob;
                break;
        }
        
    }

    public object CaptureState()
    {
        Debug.Log("Currently assigned job: " + PlayerStats.Instance.CurrentJob);
        return PlayerStats.Instance.CurrentJob;
    }

    public void RestoreState(object state)
    {
        PlayerStats.Instance.CurrentJob = (int)state;
        AssignedJob = (int)state;

        AssignJob();
    }
}
