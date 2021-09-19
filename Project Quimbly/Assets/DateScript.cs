using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectQuimbly.Dialogue;
using UnityEngine.UI;

public class DateScript : MonoBehaviour
{
    [SerializeField] int DP = 0;
    [SerializeField] Slider DateSlider;
    [SerializeField] Gradient Gradient;
    [SerializeField] Image Fill;
    [SerializeField] AudioSource Music;
    int Datelevel;
    void Start()
    {
        Fill.color = Gradient.Evaluate(DateSlider.normalizedValue);
        Datelevel = GameObject.FindWithTag("GirlContainer").GetComponentInChildren<GirlController>().GetDateLevel();
       /// Datelevel = 0; //Comment this out 
        GetComponentInChildren<AIConversant>().StartDialogue("Date " + Datelevel);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ModifyDP(int amount)
    {
        DP += amount;
        DateSlider.value = DP;
        Fill.color = Gradient.Evaluate(DateSlider.normalizedValue);
        if (DP <= -5)
        {

           GameObject.FindWithTag("GameController").GetComponent<PlayerConversant>().Quit();
           GetComponent<AIConversant>().StartDialogue("BadDate");
        }
    }
   public void EndDate()
    {
        if (DP >= 5)
        {
            Datelevel += 1;
            Music.Stop();
            GetComponent<AIConversant>().StartDialogue("BestDate");
            GameObject.FindWithTag("GirlContainer").GetComponentInChildren<GirlController>().IncreaseDateLevel(Datelevel);
            Debug.Log(GameObject.FindWithTag("GirlContainer").GetComponentInChildren<GirlController>().GetDateLevel());
        }
        else if (DP >= 0 && DP < 5)
        {
            Datelevel += 1;
            Music.Stop();
            GetComponent<AIConversant>().StartDialogue("GoodDate");
            GameObject.FindWithTag("GirlContainer").GetComponentInChildren<GirlController>().IncreaseDateLevel(Datelevel);
            Debug.Log(GameObject.FindWithTag("GirlContainer").GetComponentInChildren<GirlController>().GetDateLevel());
        }
        else
        {
            Music.Stop();
            GetComponent<AIConversant>().StartDialogue("BadDate");
        }
    }

    
}
