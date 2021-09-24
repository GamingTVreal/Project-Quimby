using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectQuimbly.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] 
        string defaultSpeakerName = "Speaker";
        [SerializeField]
        bool alternateSpeaker = false;
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();
        [SerializeField]
        List<DialogueNode> rootNodes = new List<DialogueNode>();
        [SerializeField]
        Vector2 newNodeOffset = new Vector2(300, 0);
        
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();
        Dictionary<string, DialogueNode> rootNodeLookup = new Dictionary<string, DialogueNode>();

        private void Awake() 
        {
            OnValidate();
        }

        private void OnValidate() 
        {
            nodeLookup.Clear();

            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
                if(node.IsRootNode())
                {
                    rootNodeLookup[node.GetConversationChainName()] = node;
                }
            }
        }

        public string GetDefaultSpeaker()
        {
            return defaultSpeakerName;
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public DialogueNode GetRootNode(int index)
        {
            if(index >= 0 && index < rootNodes.Count)
            {
                return rootNodes[index];
            }
            return nodes[0];
        }

        public DialogueNode GetRootNode(string rootName)
        {
            DialogueNode rootNode;
            if(rootNodeLookup.TryGetValue(rootName, out rootNode))
            {
                return rootNode;
            }
            Debug.Log("No conversation chain found with name: " + rootName);
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.GetChildren())
            {
                if(nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if(node.IsPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }

        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (!node.IsPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }

#if UNITY_EDITOR
        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNode);
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Delete Dialogue Node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }

        private DialogueNode MakeNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            newNode.SetSpeaker(defaultSpeakerName);
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                if(alternateSpeaker)
                {
                    newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
                }
                else
                {
                    newNode.SetPlayerSpeaking(parent.IsPlayerSpeaking());
                }
                newNode.SetGirlToDisplay(parent.GetGirlToDisplay());
                newNode.SetSpriteToDisplay(parent.GetSpriteToDisplay());
                newNode.SetPosition(parent.GetRect().position + newNodeOffset);
            }
            else
            {
                newNode.SetRootNode(true);
                rootNodes.Add(newNode);
            }

            return newNode;
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                AddNode(newNode);
            }

            if(AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if(AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }

                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
        }
    }
}
