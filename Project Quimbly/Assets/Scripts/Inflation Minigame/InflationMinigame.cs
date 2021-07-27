using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectQuimbly.Dialogue;

public class InflationMinigame : MonoBehaviour
{
    public TMP_Text Pressure2, Fullness2;
    private float Pressure, Fullness;
    public AudioClip[] SFX;
    public AudioSource source;

    bool hasDoneDialogue1 = false;
    //0-4 Releasing Pump, 5-10 Charging Pump
    public void Inflate()
    {
        Pressure = Pressure + 25;
        Fullness = Fullness + Random.Range(1, 7);
        int x = Random.Range(0, 4);
        source.PlayOneShot(SFX[x]);

        if(Pressure >= 50 && !hasDoneDialogue1)
        {
            GetComponent<AIConversant>().StartDialogue("Fullness 1");
            hasDoneDialogue1 = true;
        }

        if(Pressure >= 100)
        {
            GetComponent<AIConversant>().StartDialogue("Fullness 2");
        }
    }
    public void Recharge()
    {
        int x = Random.Range(5, 9);
        source.PlayOneShot(SFX[x]);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
           
            if (hit.collider != null && hit.collider.name == "BellyRubArea")
            {
                Pressure = Pressure - Time.deltaTime * 3;
            }

        }

        Pressure2.text = Pressure.ToString("0.00");
        Fullness2.text = Fullness.ToString();
        if (Pressure > 0 && Pressure < 101)
        {
            Pressure = Pressure - Time.deltaTime;
        }
    }
}
