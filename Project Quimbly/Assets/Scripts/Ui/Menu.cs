﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Home");
    }
    public void QuitGame()
    {
        Application.Quit();  
    }
   public void Discord()
    {
        Application.OpenURL("https://discord.gg/vpqseunFQa");
    }
   public void Itch()
    {
        Application.OpenURL("https://teamquimbly.itch.io/pjquimbly");
    }
   public void WeightGaming()
    {
        Application.OpenURL("https://forum.weightgaming.com/t/about-the-project-quimbly-development-name-category/18431/4");
    }
    public void Patreon()
    {
        Application.OpenURL("https://www.patreon.com/TeamQuimbly");
    }
        public void DeviantArt()
    {
        Application.OpenURL("https://www.deviantart.com/teamquimbly");
    }

}
