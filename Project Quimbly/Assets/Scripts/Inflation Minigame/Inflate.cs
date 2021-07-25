using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inflate : MonoBehaviour
{
    private int Pressure, Fullness;
    public InflationMinigame Inflation;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("1");
        Inflation.Inflate();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("2");
        Inflation.Recharge();
    }
}

