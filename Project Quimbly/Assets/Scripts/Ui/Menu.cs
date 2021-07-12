using System.Collections;
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
        Application.OpenURL("https://discord.gg/B3ddMNkT");
    }
   public void Itch()
    {
        Application.OpenURL("https://teamquimbly.itch.io/pjquimbly");
    }
   public void WeightGaming()
    {
        Application.OpenURL("https://forum.weightgaming.com/t/project-quimbly/14160");
    }
    public void Patreon()
    {
        Application.OpenURL("https://www.patreon.com/TeamQuimbly");
    }

}
