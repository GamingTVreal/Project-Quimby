﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JobAssigner : MonoBehaviour
{
    [SerializeField] TMP_Text Currentjob;
    int AssignedJob;
    public string CurrentAssignedJob;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Dishwasher()
    {
        AssignedJob = 1;
        AssignJob();
    }
    public void AssignJob()
    {
        PlayerStats.Instance.CurrentJob  = AssignedJob;

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
}
