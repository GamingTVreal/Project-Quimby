using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectQuimbly.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;

        public event Action onConversationStart;
        public event Action onConversationEnd;

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationStart?.Invoke();
        }

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue, string convoChainName)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode(convoChainName);
            TriggerEnterAction();
            onConversationStart?.Invoke();
        }

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue, int convoChain)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode(convoChain);
            TriggerEnterAction();
            onConversationStart?.Invoke();
        }

        public void Quit()
        {
            TriggerExitAction();
            currentConversant = null;
            currentDialogue = null;
            currentNode = null;
            onConversationEnd?.Invoke();
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return false;
        }

        public string GetText()
        {
            if(currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public string GetText(DialogueNode queryNode)
        {
            return queryNode.GetText();
        }

        public AIConversant GetCurrentConversant()
        {
            return currentConversant;
        }

        public string GetCurrentConversantName()
        {
            if(currentNode.IsPlayerSpeaking())
            {
                return BasicFunctions.Name;
            }
            else
            {
                return currentNode.GetSpeaker();
            }
        }

        public string GetCurrentConversantName(DialogueNode queryNode)
        {
            return queryNode.GetSpeaker();
        }

        public Sprite GetSprite()
        {
            return currentNode.GetSpriteToDisplay();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode));
        }

        public IEnumerable<DialogueNode> GetResponses()
        {
            return FilterOnCondition(currentDialogue.GetAIChildren(currentNode));
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            Next();
        }

        public void Next()
        {
            DialogueNode[] children = FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).ToArray();
            TriggerExitAction();

            int randomIndex = UnityEngine.Random.Range(0, children.Length);
            currentNode = children[randomIndex];
            TriggerEnterAction();
        }

        public bool HasNext()
        {
            return FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if(node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        private void TriggerEnterAction()
        {
            if(currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction(), currentNode.GetOnEnterActionParameters().ToArray());
            }
        }

        private void TriggerExitAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction(), currentNode.GetOnExitActionParameters().ToArray());
            }
        }

        private void TriggerAction(OnDialogueAction action, string[] actionParameters)
        {
            if(action == OnDialogueAction.None) return;

            foreach (DialogueTrigger trigger in GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action, actionParameters);
            }
        }
    }
}
