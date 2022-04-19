using System.Collections;
using System.Collections.Generic;
using ProjectQuimbly.Schedules;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDB", menuName = "Project Quimbly/CharacterDB", order = 0)]
public class CharacterDB : ScriptableObject 
{
    [SerializeField] CharacterEntry[] characters;
    Dictionary<string, CharacterEntry> characterLookup = null;

    public string[] GetNames()
    {
        BuildLookup();

        List<string> s = new List<string>();
        foreach (string key in characterLookup.Keys)
        {
            s.Add(key);
        }

        return s.ToArray();
    }

    public GameObject GetBasePrefab(string charName)
    {
        BuildLookup();

        if(characterLookup.ContainsKey(charName))
        {
            return characterLookup[charName].basePrefab;
        }
        return null;
    }

    public GameObject GetDatePrefab(string charName)
    {
        BuildLookup();

        if (characterLookup.ContainsKey(charName))
        {
            return characterLookup[charName].datePrefab;
        }
        return null;
    }

    public GameObject GetFeedingPrefab(string charName)
    {
        BuildLookup();

        if (characterLookup.ContainsKey(charName))
        {
            return characterLookup[charName].feedingPrefab;
        }
        return null;
    }

    public GameObject GetInflationPrefab(string charName)
    {
        BuildLookup();

        if (characterLookup.ContainsKey(charName))
        {
            return characterLookup[charName].inflationPrefab;
        }
        return null;
    }

    public GameObject GetInfDatePrefab(string charName)
    {
        BuildLookup();

        if (characterLookup.ContainsKey(charName))
        {
            return characterLookup[charName].infDatePrefab;
        }
        return null;
    }

    // public string GetBio(string charName)
    // {
    //     BuildLookup();

    //     if(characterLookup.ContainsKey(charName))
    //     {
    //         return characterLookup[charName].biography;
    //     }
    //     return "No bio set.";
    // }

    private void BuildLookup()
    {
        if(characterLookup != null) return;

        characterLookup = new Dictionary<string, CharacterEntry>();
        foreach (var character in characters)
        {
            characterLookup[character.girlName] = character;
        }
    }
    

    [System.Serializable]
    private class CharacterEntry
    {
        public string girlName;
        public GameObject basePrefab;
        public GameObject datePrefab;
        public GameObject feedingPrefab;
        public GameObject inflationPrefab;
        public GameObject infDatePrefab;
        // public string biography;
        // public int age;
        // public string cupSize;
    }
}
