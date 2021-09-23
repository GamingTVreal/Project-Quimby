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
    List<int> widths = new List<int>() { 1920, 1280, 960, 568};
    List<int> heights = new List<int>() { 1080, 800, 540, 320};

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);

    }

}
