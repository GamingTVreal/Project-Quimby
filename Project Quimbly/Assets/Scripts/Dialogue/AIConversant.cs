using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue;
        [SerializeField] string conversationChain;
        [SerializeField] int[] randomConvoOptions;
        
        public void StartDialogue()
        {
            PlayerConversant player = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerConversant>();
            if(player != null)
            {
                player.StartDialogue(this, dialogue);
            }
        }

        public void StartDialogue(bool randomConvo = false)
        {
            PlayerConversant player = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerConversant>();
            if (player != null)
            {
                if (!randomConvo)
                {
                    player.StartDialogue(this, dialogue, conversationChain);
                }
                else
                {
                    int choice = Random.Range(0, randomConvoOptions.Length);
                    choice = randomConvoOptions[choice];
                    player.StartDialogue(this, dialogue, choice);
                }
            }
        }

        public void StartDialogue(Dialogue newDialogue)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>().StartDialogue(this, newDialogue);
        }

        public void StartDialogue(string convoStart)
        {
            PlayerConversant player = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerConversant>();
            if (player != null)
            {
                player.StartDialogue(this, dialogue, convoStart);
            }
        }
    }
}
