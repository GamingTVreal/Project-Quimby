using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Dialogue;
using UnityEngine;

public class GoToWork : MonoBehaviour
{
    [SerializeField] AIConversant conversant = null;

    private void Awake() 
    {
        conversant = GetComponent<AIConversant>();
    }
    public void TryToWork()
    {
        if (PlayerStats.Instance.CurrentJob != 0)
        {
            if (PlayerStats.Instance.Energy == 0)
            {
                NoEnergy();
            }
            else if (PlayerStats.Instance.Energy < 15 && PlayerStats.Instance.Energy > 0)
            {
                NotEnoughEnergy();
            }
            else
            {
                PlayerStats.Instance.Energy -= 15;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<LoadingScreenScript>().Work();
            }
        }
        else
        {
            conversant.StartDialogue("No Job");
        }
    }
    public void NoEnergy()
    {
        conversant.StartDialogue("No Energy");
    }
    public void NotEnoughEnergy()
    {
        conversant.StartDialogue("Low Energy");
    }
}
