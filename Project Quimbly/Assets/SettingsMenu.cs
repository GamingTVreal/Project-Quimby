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
    List<int> widths = new List<int>() { 568, 960, 1280, 1920 };
    List<int> heights = new List<int>() { 329, 540, 800, 1080 };

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);

    }

}
