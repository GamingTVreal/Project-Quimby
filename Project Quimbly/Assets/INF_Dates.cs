using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectQuimbly.Dialogue;

public class INF_Dates : MonoBehaviour
{
   [SerializeField] AudioSource musicSource;


    int dateLevel;
    AIConversant conversant = null;
    GirlController girlController = null;

    private void Awake() {
        conversant = GetComponent<AIConversant>();
        girlController = GetComponent<GirlController>();
        musicSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
    }

    void Start()
    {
        dateLevel = girlController.GetInflatedDateLevel();
        Debug.Log("This is the " + dateLevel + " Date");
        //dateLevel = 2;
        conversant.StartDialogue("Date " + dateLevel);
    }

    public void EndInflationDate()
    {
        girlController.IncreaseINFDateLevel();
        conversant.onConversationEnd += ResetGirlLocation;
    }
    
public void ResetGirlLocation()
    {
        girlController.ResetLocation();
        conversant.onConversationEnd -= ResetGirlLocation;
    }

    
}


