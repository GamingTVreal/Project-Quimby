using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VoiceLineDB", menuName = "Project Quimbly/VoiceLineDB", order = 0)]
public class VoiceLineDB : ScriptableObject 
{
    [SerializeField] CharacterVoice[] characterVoices;
    Dictionary<string, Dictionary<string, AudioClip>> voiceLookup = null;

    public AudioClip GetVoiceClip(string character, string lineName)
    {
        BuildLookup();

        AudioClip clip = null;
        if(voiceLookup.ContainsKey(character))
        {
            voiceLookup[character].TryGetValue(lineName, out clip);
        }
        return clip;
    }

    public List<string> GetCharacterNames()
    {
        BuildLookup();

        List<string> characterList = new List<string>();
        characterList.Add("None");
        foreach (var characterName in voiceLookup.Keys)
        {
            characterList.Add(characterName);
        }
        return characterList;
    }

    public List<string> GetVoiceLines(string character)
    {
        BuildLookup();

        List<string> voiceLineList = new List<string>();
        voiceLineList.Add("None");
        if(voiceLookup.ContainsKey(character))
        {
            foreach (var voiceLine in voiceLookup[character].Keys)
            {
                voiceLineList.Add(voiceLine);
            }
        }
        return voiceLineList;
    }

    private void BuildLookup()
    {
        if (voiceLookup != null) return;

        voiceLookup = new Dictionary<string, Dictionary<string, AudioClip>>();
        foreach (var character in characterVoices)
        {
            var sfxLookup = new Dictionary<string, AudioClip>();
            foreach (var sample in character.voiceLines)
            {
                sfxLookup[sample.lineName] = sample.voiceClip;
            }
            voiceLookup[character.name] = sfxLookup;
        }
    }
    
    [System.Serializable]
    private class CharacterVoice
    {
        public string name;
        public VoiceLine[] voiceLines;
    }

    [System.Serializable]
    private class VoiceLine
    {
        public string lineName;
        public AudioClip voiceClip;
    }
}
