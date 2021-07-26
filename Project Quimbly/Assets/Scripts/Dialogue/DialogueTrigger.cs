using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectQuimbly.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        OnDialogueAction action;
        [SerializeField]
        UnityEvent<string[]> onTrigger;

        public void Trigger(OnDialogueAction actionToTrigger, string[] actionParameters)
        {
            if(actionToTrigger == action)
            {
                onTrigger.Invoke(actionParameters);
            }
        }
    }
}
