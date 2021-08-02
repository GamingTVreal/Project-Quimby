using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Dialogue;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject Store;
    AIConversant conversant = null;
    bool TalkedToShopLady = false;
    
    private void Awake() 
    {
        conversant = GetComponent<AIConversant>();
    }

   public void StartBrowsing()
    {
        if (TalkedToShopLady == true)
        {
            Store.SetActive(true);
        }
        else
        {
            conversant.StartDialogue();
            TalkedToShopLady = true;
        } 
    }
}
