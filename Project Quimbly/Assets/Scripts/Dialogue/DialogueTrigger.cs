using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectQuimbly.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] ActionTriggerPair[] actionTriggerPairs;
        Dictionary<OnDialogueAction, UnityEvent<string[]>> actionLookup = null;
        // [SerializeField]
        // OnDialogueAction action;
        // [SerializeField]
        // UnityEvent<string[]> onTrigger;

        private void Awake() 
        {
            BuildLookup();
        }

        private void BuildLookup()
        {
            actionLookup = new Dictionary<OnDialogueAction, UnityEvent<string[]>>();
            foreach (var action in actionTriggerPairs)
            {
                actionLookup[action.action] = action.onTrigger;
            }
        }

        public void Trigger(OnDialogueAction actionToTrigger, string[] actionParameters)
        {
            // if(actionToTrigger == action)
            // {
            //     onTrigger.Invoke(actionParameters);
            // }
            if(actionLookup.ContainsKey(actionToTrigger))
            {
                actionLookup[actionToTrigger].Invoke(actionParameters);
            }
        }

        [System.Serializable]
        private class ActionTriggerPair
        {
            public OnDialogueAction action;
            public UnityEvent<string[]> onTrigger;
        }
    }
}
