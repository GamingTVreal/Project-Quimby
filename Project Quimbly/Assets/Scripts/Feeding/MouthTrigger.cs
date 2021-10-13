using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Inventories;
using ProjectQuimbly.UI.Dragging;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectQuimbly.Feeding
{
    public class MouthTrigger : MonoBehaviour, IDragDestination
    {
        [SerializeField] GirlFeeding feedingScript = null;

        [SerializeField] int biteSize = 1;
        [SerializeField][Range(0, 3f)]
        float eatingCooldown = 1f;
        float timeSinceLastBite = Mathf.Infinity;
        // Event set to cancel UI dragging in BasicFunctions
        // Tried canceling in the DragItem draghandler, but I couldn't get the PointerEventData to agree
        [SerializeField] UnityEvent onMouthEvent;

        private void Awake()
        {
            if(feedingScript == null)
            {
                feedingScript = GetComponentInParent<GirlFeeding>();
            }

            // Setting up trigger events
            FeedingRoom feedingRoom = GameObject.FindWithTag("MainCamera").GetComponentInChildren<FeedingRoom>();
            BasicFunctions basicFunctions = GameObject.FindWithTag("GameController").GetComponent<BasicFunctions>();

            onMouthEvent.AddListener(() => 
            {
                basicFunctions.CancelUIDrag();
                feedingRoom.ConsumeItem();
                feedingRoom.PlaySFX();
                feedingRoom.UpdateFullnessText();
            });
        }

        private void Update() 
        {
            timeSinceLastBite += Time.deltaTime;
        }
        
        public float MaxAcceptable()
        {
            return biteSize;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryToEat(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TryToEat(other);
        }

        private void TryToEat(Collider2D other)
        {
            if (timeSinceLastBite >= eatingCooldown)
            {
                SelectedFood food = other.GetComponent<SelectedFood>();
                if (food != null)
                {
                    float foodFillAmount = food.GetItem().filling;
                    feedingScript.IncreaseFullness(foodFillAmount);
                    timeSinceLastBite = 0;
                    food.RemoveItems(biteSize);
                    onMouthEvent?.Invoke();
                }
            }
        }
    }
}