using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer Music, SFX;

    public void SetMusicVol(float MusicVolume)
    {
        Music.SetFloat("MusicVol", MusicVolume);
    }
    public void SetSFXVol(float SFXVolume)
    {
        Music.SetFloat("MusicVol", SFXVolume);
    }
    public void SetFullScreen(bool isfullscreen)
    {
        if(isfullscreen == true)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }
}
