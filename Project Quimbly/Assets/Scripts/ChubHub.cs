using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectQuimbly.Dialogue;
using ProjectQuimbly.Controllers;

public class ChubHub : MonoBehaviour
{
    GameObject Phone, Character;
    //Scheduler VictoriaSchedule = GameObject.FindWithTag("GirlContainer").GetComponentInChildren<Scheduler>();
    GirlController girlController = null;
    //LoadingScreenScript Loading = null;
    AIConversant conversant = null;
    GirlManager girlManager = null;

    void Awake()
    {
        Phone = GameObject.FindWithTag("PhoneMenu");
        conversant = GetComponent<AIConversant>();
        //Loading = GameObject.FindWithTag("GameController").GetComponent<LoadingScreenScript>();
        girlController = GameObject.FindWithTag("GameController").GetComponent<GirlController>();
        girlManager = GameObject.FindWithTag("GirlContainer").GetComponent<GirlManager>();
    }
    public void Food()
    {
        Phone.SetActive(false);
        conversant.StartDialogue("ChubhubOrder");
        girlManager.ChangeInactiveCharacterLocation("Victoria", "Home");

        //VictoriaSchedule.ChangeLocation("Home");

    }
}
