using System;
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

        PlayerConversant player = null;

        public event Action onConversationEnd;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerConversant>();
        }
        
        public void StartDialogue()
        {
            if(player != null)
            {
                player.StartDialogue(this, dialogue);
                player.onConversationEnd += OnConversationEnd;
            }
        }

        public void StartDialogue(Dialogue newDialogue)
        {
            if(player != null)
            {
                player.StartDialogue(this, newDialogue);
                player.onConversationEnd += OnConversationEnd;
            }
        }

        public void StartDialogue(string convoStart)
        {
            if (player != null)
            {
                player.StartDialogue(this, dialogue, convoStart);
                player.onConversationEnd += OnConversationEnd;
            }
        }

        public void StartDialogue(Dialogue newDialogue, string convoStart)
        {
            if (player != null)
            {
                player.StartDialogue(this, newDialogue, convoStart);
                player.onConversationEnd += OnConversationEnd;
            }
        }

        public void StartDialogue(bool randomConvo = false)
        {
            if (player != null)
            {
                if (!randomConvo)
                {
                    player.StartDialogue(this, dialogue, conversationChain);
                    player.onConversationEnd += OnConversationEnd;
                }
                else
                {
                    int choice = UnityEngine.Random.Range(0, randomConvoOptions.Length);
                    choice = randomConvoOptions[choice];
                    player.StartDialogue(this, dialogue, choice);
                    player.onConversationEnd += OnConversationEnd;
                }
            }
        }

        public void SetNewDialog(Dialogue newDialog)
        {
            dialogue = newDialog;
        }
        public void OnConversationEnd()
        {
            onConversationEnd?.Invoke();
            player.onConversationEnd -= OnConversationEnd;
        }
    }
}
