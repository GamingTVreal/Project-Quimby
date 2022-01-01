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
    

    public void AssignJob(int Job)
    {
        if(Currentjob == null) return;

        PlayerStats.Instance.CurrentJob = Job;

        switch (Job)
        {
            case 0:
                CurrentAssignedJob = "Unenployed";
                Currentjob.text = CurrentAssignedJob;
                break;
            case 1:
                CurrentAssignedJob = "Dishwasher";
                Currentjob.text = CurrentAssignedJob;
                break;
            case 2:
                CurrentAssignedJob = "Mechanic";
                Currentjob.text = CurrentAssignedJob;
                break;
        }

        AssignedJob = Job;
    }

    public object CaptureState()
    {
        return PlayerStats.Instance.CurrentJob;
    }

    public void RestoreState(object state)
    {
        PlayerStats.Instance.CurrentJob = (int)state;
        AssignedJob = (int)state;

        AssignJob(PlayerStats.Instance.CurrentJob);
    }
}
