using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "AudioSampleDB", menuName = "Project Quimbly/AudioSampleDB", order = 0)]
public class AudioSampleDB : ScriptableObject 
{
    [SerializeField] SampleGroup[] sampleGroups;
    Dictionary<string, Dictionary<string, AudioClip>> sampleLookup = null;

    public AudioClip GetAudioClip(string groupName, string sfxName)
    {
        BuildLookup();

        AudioClip clip = null;
        if(sampleLookup.ContainsKey(groupName))
        {
            sampleLookup[groupName].TryGetValue(sfxName, out clip);
        }
        return clip;
    }

    public List<string> GetGroupNames()
    {
        BuildLookup();

        List<string> groupList = new List<string>();
        groupList.Add("None");
        foreach (var sampleGroup in sampleLookup.Keys)
        {
            groupList.Add(sampleGroup);
        }
        return groupList;
    }

    public List<string> GetSampleNames(string sampleGroup)
    {
        BuildLookup();
        
        List<string> sampleList = new List<string>();
        sampleList.Add("None");
        if(sampleLookup.ContainsKey(sampleGroup))
        {
            foreach (var sample in sampleLookup[sampleGroup].Keys)
            {
                sampleList.Add(sample);
            }
        }
        return sampleList;
    }

    private void BuildLookup()
    {
        if(sampleLookup != null) return;

        sampleLookup = new Dictionary<string, Dictionary<string, AudioClip>>();
        foreach (var group in sampleGroups)
        {
            var sfxLookup = new Dictionary<string, AudioClip>();
            foreach (var sample in group.sfxSamples)
            {
                sfxLookup[sample.sfxName] = sample.clip;
            }
            sampleLookup[group.name] = sfxLookup;
        }
    }
    
    [System.Serializable]
    private class SampleGroup
    {
        public string name;
        public SFXSample[] sfxSamples;
    }

    [System.Serializable]
    private class SFXSample
    {
        public string sfxName;
        public AudioClip clip;
    }
}
