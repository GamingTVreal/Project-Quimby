using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCache : MonoBehaviour
{
    static Dictionary<string, AudioClip> clipLookupCache;

    public static AudioClip GetAudioByFilename(string clipName)
    {
        if(clipLookupCache == null)
        {
            clipLookupCache = new Dictionary<string, AudioClip>();
            Debug.Log("Starting to load");
            var clipList = Resources.LoadAll<AudioClip>("");
            Debug.Log("Finished loading");
            foreach (var clip in clipList)
            {
                if(clipLookupCache.ContainsKey(clip.name))
                {
                    Debug.LogError(string.Format("Looks like there's a duplicate audio trach name for audio: {0}", clip.name));
                }

                clipLookupCache[clip.name] = clip;
            }
        }

        if(clipName == null || !clipLookupCache.ContainsKey(clipName)) return null;
        return clipLookupCache[clipName];
    }
}
