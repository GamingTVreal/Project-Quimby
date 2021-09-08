using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "MusicTrackDB", menuName = "Project Quimbly/MusicTrackDB", order = 0)]
public class MusicTrackDB : ScriptableObject 
{
    [SerializeField] MusicTrack[] musicTracks;
    Dictionary<string, AudioClip> trackLookup = null;

    public AudioClip GetAudioClip(string track)
    {
        BuildLookup();
        AudioClip clip = null;
        trackLookup.TryGetValue(track, out clip);
        return clip;
    }

    public List<string> GetAllTrackNames()
    {
        BuildLookup();

        List<string> trackList = new List<string>();
        trackList.Add("None");
        foreach (string track in trackLookup.Keys)
        {
            trackList.Add(track);
        }
        return trackList;
    }

    private void BuildLookup()
    {
        if(trackLookup != null) return;

        trackLookup = new Dictionary<string, AudioClip>();
        foreach (MusicTrack track in musicTracks)
        {
            trackLookup[track.trackName] = track.clip;
        }
    }
    
    [System.Serializable]
    private class MusicTrack
    {
        public string trackName;
        public AudioClip clip;
    }
}
