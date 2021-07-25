using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InflationMinigame : MonoBehaviour
{
    private float Pressure, Fullness;
    public AudioClip[] SFX;
    public AudioSource source;
    //0-4 Releasing Pump, 5-10 Charging Pump
    public void Inflate()
    {
        Pressure = Pressure + 25;
        int x = Random.Range(0, 4);
        source.PlayOneShot(SFX[x]);
    }
    public void Recharge()
    {
        int x = Random.Range(5, 9);
        source.PlayOneShot(SFX[x]);
    }
    private void Update()
    {
        if(Pressure > 0 && Pressure < 101)
        {
            Pressure = Pressure - Time.deltaTime;
        }
    }
}
