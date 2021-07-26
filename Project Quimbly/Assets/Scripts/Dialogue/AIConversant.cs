using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] string npcName = "";
        [SerializeField] Dialogue dialogue;
        
        public void StartDialogue()
        {
            PlayerConversant player = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerConversant>();
            if(player != null)
            {
                player.StartDialogue(this, dialogue);
            }
        }

        public void StartDialogue(Dialogue newDialogue)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>().StartDialogue(this, newDialogue);
        }
        
        public string GetCharacterInfo()
        {
            return npcName;
        }
    }
}
