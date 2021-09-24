using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectQuimbly.Dialogue;
using UnityEngine.UI;

public class DateScript : MonoBehaviour
{
    [SerializeField] int DP = 0;
    [SerializeField] Slider dateSlider;
    [SerializeField] Gradient Gradient;
    [SerializeField] Image Fill;
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
        Fill.color = Gradient.Evaluate(dateSlider.normalizedValue);
        dateLevel = girlController.GetDateLevel();
        dateLevel = 2; //Comment this out 
        conversant.StartDialogue("Date " + dateLevel);
    }

    public void ModifyDP(int amount)
    {
        DP += amount;
        dateSlider.value = DP;
        Fill.color = Gradient.Evaluate(dateSlider.normalizedValue);
        if (DP <= -5)
        {

           GameObject.FindWithTag("GameController").GetComponent<PlayerConversant>().Quit();
            conversant.StartDialogue("BadDate");
        }
    }

    public void EndDate()
    {
        if (dateLevel >= 2)
        {
            if (DP >= 5)
            {
                dateLevel += 1;
                musicSource.Stop();
                conversant.StartDialogue("DateFinale");
                girlController.IncreaseDateLevel(dateLevel);
                Debug.Log(girlController.GetDateLevel());
            }
            else if (DP >= 0 && DP < 5)
            {
                dateLevel += 1;
                musicSource.Stop();
                conversant.StartDialogue("DateFinale");
                girlController.IncreaseDateLevel(dateLevel);
                Debug.Log(girlController.GetDateLevel());
            }
            else
            {
                musicSource.Stop();
                conversant.StartDialogue("FinalDateFail");
            }
        }

        else
        {
            if (DP >= 5)
            {
                dateLevel += 1;
                musicSource.Stop();
                conversant.StartDialogue("BestDate");
                girlController.IncreaseDateLevel(dateLevel);
                Debug.Log(girlController.GetDateLevel());
            }
            else if (DP >= 0 && DP < 5)
            {
                dateLevel += 1;
                musicSource.Stop();
                conversant.StartDialogue("GoodDate");
                girlController.IncreaseDateLevel(dateLevel);
                Debug.Log(girlController.GetDateLevel());
            }
            else
            {
                musicSource.Stop();
                conversant.StartDialogue("BadDate");
            }
        }
    }

    
}