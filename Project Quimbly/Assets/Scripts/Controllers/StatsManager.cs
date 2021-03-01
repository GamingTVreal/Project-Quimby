using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class StatsManager : MonoBehaviour
{
    
    public PlayerStats Player;
    public AudioSource SnoringSFX;
    public Animator Fadeout;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject Singleton = GameObject.Find("Singleton PlayerStats");
        Player = Singleton.GetComponent<PlayerStats>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToBed()
    {

        Player.Sleep();
    }
}
