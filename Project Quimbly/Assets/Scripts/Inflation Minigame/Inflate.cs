using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Inflation
{
    public class Inflate : MonoBehaviour
    {
        public InflationMinigame Inflation;

        void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("1");
            Inflation.Inflate();
        }
        void OnTriggerExit2D(Collider2D other)
        {
            //Debug.Log("2");
            Inflation.Recharge();
        }

        public void WaterInflate()
        {
            Inflation.WaterInflate();
        }
    }
}

