using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectQuimbly.Dialogue;
using UnityEngine.UI;

public class InflationMinigame : MonoBehaviour
{
    public Image InflatedGirl;
    public Sprite[] Deb , DebW;
    public GameObject GameOver, PumpObject;
    public TMP_Text Pressure2, Fullness2;
    public float Pressure, Fullness;
    public AudioClip[] SFX;
    public AudioClip[] VoiceLines;
    public AudioSource source;
    public int CurrentPump;
    bool InfLine = false;
    bool hasDoneDialogue1 = false;
    private int FullnessSprites, x;
    int currentSprite = 0;
    //0-4 Releasing Pump, 5-10 Charging Pump

    public void ChoosePump(int choice)
    {
        CurrentPump = choice;
        Debug.Log(CurrentPump + " is the current pump");
    }
    public void Inflate()
    {  
        if (CurrentPump == 0)
        {
            Pressure = Pressure + 25;
            Fullness = Fullness + Random.Range(1, 7);
            SpriteCheck();

            int x = Random.Range(0, 4);
            source.PlayOneShot(SFX[x]);

                
            
            Fullness2.text = Fullness.ToString();
            Debug.Log(Fullness);
           

        }
        else if (CurrentPump == 1)
        {
            InfLine = false;
            Pressure = Pressure + (Time.deltaTime * 5) ;
            Fullness = Fullness + Time.deltaTime;
            SpriteCheck();

            Fullness2.text = Fullness.ToString("0.00");
            Debug.Log(Fullness);

        }

        if (Fullness >= 100)
        {
            Fullness = 100;
            GetComponent<AIConversant>().StartDialogue("DebMax");
        }
        if(Pressure >= 75)
        {
            if (source.isPlaying == false)
            {
                source.PlayOneShot(VoiceLines[Random.Range(3, 5)]);
            }

            
        }
        if (Pressure >= 100)
        {
            PumpObject.SetActive(false);
            GameOver.SetActive(true);
        }
            
    }
    public void SpriteCheck()
    {
        int spriteLevel = Mathf.FloorToInt(Fullness / 5);
        if (spriteLevel > currentSprite)
        {
            currentSprite = spriteLevel;
            if (CurrentPump == 0)
            {
                InflatedGirl.sprite = Deb[spriteLevel];
            }
            if (CurrentPump == 1)
            {
                InflatedGirl.sprite = DebW[spriteLevel];
            }

        }
    }
    public void Recharge()
    {
        InfLine = false;
        int x = Random.Range(5, 9);
        source.PlayOneShot(SFX[x]);
    }
    private void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (Input.GetMouseButton(0))
        {
            
           
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
                if (Pressure < 1)
                {
                    Pressure = 0;
                }
                
            }

        }

        Pressure2.text = Pressure.ToString("0.00");
        if (Pressure > 0 && Pressure < 101)
        {
            Pressure = Pressure - Time.deltaTime;
        }
        

    
    }
    public void WaterInflate()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        
        if (CurrentPump == 1 && Input.GetMouseButton(0) && hit.collider.name == "Enemabag")
        {
            Debug.Log(hit.collider.name);
            Inflate();
          
        }
    }
    public void test()
    {
        Fullness += 5;
        FullnessSprites += Mathf.FloorToInt(Fullness);
        SpriteCheck();
    }
    public void LeaveInflation()
    {
        if (Fullness < 99)
        {
            GetComponent<AIConversant>().StartDialogue("LeaveDeb");
        }
        else
        {
            GetComponent<AIConversant>().StartDialogue("MaxDeb");
        }

    }
}
