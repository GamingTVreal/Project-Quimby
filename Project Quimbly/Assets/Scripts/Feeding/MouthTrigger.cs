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
        private CharacterController Character;
        private FeedingRoom Feed;

        [SerializeField] int biteSize = 1;
        [SerializeField][Range(0, 3f)]
        float eatingCooldown = 1f;
        float timeSinceLastBite = Mathf.Infinity;
        // Event set to cancel UI dragging in BasicFunctions
        // Tried canceling in the DragItem draghandler, but I couldn't get the PointerEventData to agree
        [SerializeField] UnityEvent onMouthEvent;

        private void Awake()
        {
            Character = FindObjectOfType<CharacterController>();
            Feed = FindObjectOfType<FeedingRoom>();
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
                    Feed.GetSFX(0);
                    float foodFillAmount = food.GetItem().filling;
                    Character.fullness = foodFillAmount + Character.fullness;
                    Feed.GetFullnessSprite(foodFillAmount);
                    timeSinceLastBite = 0;
                    food.RemoveItems(biteSize);
                    onMouthEvent?.Invoke();
                }
            }
        }
    }
}