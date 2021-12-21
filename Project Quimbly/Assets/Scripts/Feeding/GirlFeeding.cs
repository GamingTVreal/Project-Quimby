using System;
using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Dialogue;
using ProjectQuimbly.Schedules;
using UnityEngine;

public class GirlFeeding : MonoBehaviour
{
    float fullness = 0;
    [SerializeField] SpriteController spriteController = null;
    // Will need reworked if not all girls have the same dialogue counts
    [SerializeField] int numFullConvos;
    [SerializeField] int numMaxFullConvos;
    [SerializeField] int bonusComboFullness = 10;

    // Cache
    int capacity;
    int currentSprite = 0;
    GameObject foodUI = null;
    Item itemLastEaten;

    public event Action OnItemCombo;

    // Set up references
    private void Start() 
    {
        capacity = GetComponent<GirlController>().GetBellyCapacity();
        spriteController = GetComponent<SpriteController>();
        foodUI = GameObject.FindWithTag("FoodUI");
        itemLastEaten = new Item();
    }

    // Change sprite to next level every 5 fullness
    private void UpdateFullnessSprite()
    {
        int spriteLevel = Mathf.FloorToInt(fullness/5);
        if(spriteLevel > currentSprite)
        {
            currentSprite = spriteLevel;
            spriteController.UpdateFeedingSprite(spriteLevel);
        }
    }

    // From MouthTrigger event
    public void IncreaseFullness(float amount)
    {
        fullness += amount;
        UpdateFullnessSprite();

        if (fullness >= capacity)
        {
            CapacityReached();
        }
    }

    // From MouthTrigger event
    public void ItemTypeConsumed(Item.ItemType newItemType)
    {
        // Debug.Log(newItemType.ToString());
        if(itemLastEaten.itemType != newItemType)
        {
            if(IsItemCombination(newItemType))
            {
                IncreaseFullness(bonusComboFullness);
                OnItemCombo?.Invoke();
            }
            itemLastEaten.itemType = newItemType;
        }
    }

    private bool IsItemCombination(Item.ItemType newItemType)
    {
        switch (itemLastEaten.itemType)
        {
            case Item.ItemType.Cola:
                if(newItemType == Item.ItemType.Mints)
                    return true;
                return false;
            case Item.ItemType.Banana:
                if(newItemType == Item.ItemType.Soda)
                    return true;
                return false;
            case Item.ItemType.Soda:
                if(newItemType == Item.ItemType.Banana)
                    return true;
                return false;
            default:
                return false;
        }
    }

    // Display conversation based on capacity
    private void CapacityReached()
    {
        foodUI.SetActive(false);

        AIConversant conversant = GetComponent<AIConversant>();
        if(capacity >= 100)
        {
            capacity = 100;
            int randomConvo = UnityEngine.Random.Range(0, numMaxFullConvos) + 1;
            conversant.StartDialogue("Max Full " + randomConvo);
        }
        else
        {
            int randomConvo = UnityEngine.Random.Range(0, numFullConvos) + 1;
            conversant.StartDialogue("Full " + randomConvo);
            GetComponent<GirlController>().ModifyBellyCapacity(5);
        }
    }

    public float GetFullness()
    {
        return fullness;
    }

    // Leave scene via button
    public void TryLeaving()
    {
        Scheduler schedule = GetComponent<Scheduler>();
        schedule.ResetLocation();

        if(fullness <= capacity)
        {
            AIConversant conversant = GetComponent<AIConversant>();
            conversant.StartDialogue("Out of Food");
        }
        else
        {
            LoadingScreenScript loadingScreenScript = GameObject.FindWithTag("GameController").GetComponent<LoadingScreenScript>();
            loadingScreenScript.LoadNewArea("Home");
        }
    }
}
