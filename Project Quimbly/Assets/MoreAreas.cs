using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreAreas : MonoBehaviour
{
    public Sprite[] OtherLocations;
    public Image Background;
     int Locations = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
        

    public void NextRoom()
    {
        

        if(Locations >= OtherLocations.Length){
            Locations = 0;
            Background.sprite = OtherLocations[Locations];
        }
        else{
            Locations += 1;
            Background.sprite = OtherLocations[Locations];
        }
    }
    public void PreviousRoom()
    {  
        if(Locations >= OtherLocations.Length){
        Locations -= 1;
        Background.sprite = OtherLocations[Locations];
        }
        else
        {
            NextRoom();
        }
    }
}
