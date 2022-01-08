using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectQuimbly.Dialogue;
using ProjectQuimbly.Schedules;

public class ChubHub : MonoBehaviour
{
    GameObject Phone, Character;
    Scheduler VictoriaSchedule = GameObject.FindWithTag("GirlContainer").GetComponentInChildren<Scheduler>();
    GirlController girlController = null;
    LoadingScreenScript Loading = null;
    AIConversant conversant = null;


    // Start is called before the first frame update
    void Awake()
    {
        Phone = GameObject.FindGameObjectWithTag("PhoneMenu");
        conversant = GetComponent<AIConversant>();
        Loading = GameObject.FindWithTag("GameController").GetComponent<LoadingScreenScript>();
        girlController = GameObject.FindWithTag("GameController").GetComponent<GirlController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Food()
    {
        Phone.SetActive(false);
        conversant.StartDialogue("ChubhubOrder");
        //VictoriaSchedule.ChangeLocation("Home");

    }
}
