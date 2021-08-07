using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectQuimbly.Dialogue;
using UnityEngine.UI;

public class InflationMinigame : MonoBehaviour
{
    public Image InflatedGirl;
    public Sprite[] Deb;
    public GameObject GameOver, PumpObject;
    public TMP_Text Pressure2, Fullness2;
    public float Pressure;
    public AudioClip[] SFX;
    public AudioClip[] VoiceLines;
    public AudioSource source;
    bool InfLine = false;
    bool hasDoneDialogue1 = false;
    private int Fullness, FullnessSprites, x;
    //0-4 Releasing Pump, 5-10 Charging Pump
    public void Inflate()
    {
        Pressure = Pressure + 25;
        Fullness = Fullness + Random.Range(1, 7);
        FullnessSprites += Fullness;
        SpriteCheck();

        Fullness2.text = Fullness.ToString();
        Debug.Log(Fullness);
        int x = Random.Range(0, 4);
        source.PlayOneShot(SFX[x]);
        if (Fullness >= 100)
        {
            GetComponent<AIConversant>().StartDialogue("DebMax");
        }
        if(Pressure >= 75)
        {
            source.PlayOneShot(VoiceLines[Random.Range(3, 5)]);
        }
        if (Pressure >= 100)
        {
            PumpObject.SetActive(false);
            GameOver.SetActive(true);
        }
            
    }
    public void SpriteCheck()
    {
        if (FullnessSprites > 5 && x < 19)
        {
            x = x + 1;
            FullnessSprites = FullnessSprites - 5;
        }
        InflatedGirl.sprite = Deb[x];
    }
    public void Recharge()
    {
        InfLine = false;
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

                if(InfLine == false)
                {
                    if(source.isPlaying == false)
                    {
                        source.PlayOneShot(VoiceLines[Random.Range(0, 2)]);
                    }

                    InfLine = true;
                }
                if (Pressure > 1)
                {
                    Pressure = Pressure - Time.deltaTime * 3;
                }
                
            }

        }

        Pressure2.text = Pressure.ToString("0.00");
        if (Pressure > 0 && Pressure < 101)
        {
            Pressure = Pressure - Time.deltaTime;
        }
    
    
    }
    public void test()
    {
        Fullness += 5;
        FullnessSprites += Fullness;
        SpriteCheck();
    }
}
