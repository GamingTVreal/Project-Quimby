using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.UI.Dragging;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectQuimbly.Feeding
{
    public class MouthTrigger : MonoBehaviour, IDragDestination
    {
        [SerializeField] int biteSize = 1;
        // Event set to cancel UI dragging in BasicFunctions
        // Tried canceling in the DragItem draghandler, but I couldn't get the PointerEventData to agree
        [SerializeField] UnityEvent onMouthEvent; 
        
        public float MaxAcceptable()
        {
            return biteSize;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            SelectedFood food = other.GetComponent<SelectedFood>();
            if(food != null)
            {
                food.RemoveItems(biteSize);
                onMouthEvent?.Invoke();
            }
        }
    }
}
