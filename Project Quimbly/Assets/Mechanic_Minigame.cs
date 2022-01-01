using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mechanic_Minigame : MonoBehaviour
{
    public AudioSource SFX;
    public AudioClip pop;
    private int _pay, _bonus, _TargetPSI, _Money, Target1, Target2;
    private Transform DishTransform;
    [SerializeField] Image TireImage;
    [SerializeField] Sprite[] TireSprites;
    [SerializeField] private GameObject EndPage, Pump;
    [SerializeField] private TMP_Text Pay, TiresInflated, Bonus, CurrentPSI, TargetPSI;
    public Slider RemainingTime; 
    int AmountSpawned = 0;
    float PSI;
    [SerializeField] RectTransform SpawnArea;



    private float _timeRemaining, _maxTime;
    //float minutes = Mathf.FloorToInt(timeRemaining / 60);

    private void Start()
    {
        SetPSI();
        switch (PlayerStats.Instance.JobLevel)
        {
            case 0:
                _maxTime = 30;
                break;
            default:
                _maxTime = 30;
                break;
        }
        _timeRemaining = Mathf.FloorToInt(_maxTime);

    }
    private void Update()
    {

        _timeRemaining -= Time.deltaTime;
        DisplayTime(_timeRemaining);



        Pay.text = _pay.ToString();
        if (_timeRemaining <= 0)
        {
            Pump.SetActive(false);
            Bonus.text = _bonus.ToString();
            EndPage.SetActive(true);
        }
    }
    public void Inflate()
    {

        PSI = PSI + (Time.deltaTime * 8); 
        CurrentPSI.text = PSI.ToString("0.00");


         if (PSI > _TargetPSI + 2)
        {
            JudgeFullness();
        }
        
        else if (PSI > _TargetPSI)
        {
            
            
            TireImage.sprite = TireSprites[3];
            if (SFX.isPlaying == false)
            {
                SFX.PlayOneShot(pop);
            }    
            
            
            
        }

        else if (PSI > Target2 && PSI < Target1)
        {
            TireImage.sprite = TireSprites[1];
        }
        else if (PSI > Target1 && PSI < _TargetPSI)
        {
            TireImage.sprite = TireSprites[2];
        }
        else if (PSI == _TargetPSI)
        {
            TireImage.sprite = TireSprites[2];
        }


    }

    void SetPSI()
    {
        _TargetPSI = Random.Range(25, 31);
        Target1 = _TargetPSI - 3;
        Target2 = _TargetPSI / 3;
        TargetPSI.text = _TargetPSI.ToString();
    }
    void DisplayTime(float timeToDisplay)
    {
        
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        RemainingTime.value = seconds;
        //Debug.Log(timeToDisplay);

    }
    public void TirePump()
    {
       
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (Input.GetMouseButton(0) && hit.collider.name == "Enemabag")
        {
            Debug.Log(hit.collider.name);
            Inflate();

        }
    }

    public void JudgeFullness()
    {
        
        if (PSI > _TargetPSI)
        {
            _Money = 0;
        }
        else if (PSI == _TargetPSI)
        {
            _Money = Random.Range(100, 500);
        }
        else if (PSI >= Target1 && PSI < _TargetPSI)
        {
            _Money = Random.Range(10, 25);
        }
        else if (PSI >= Target2 && PSI < Target1)
        {
            _Money = Random.Range(5, 10);
        }
        _pay = _pay + _Money;
         Pay.text = _pay.ToString();
        _bonus = _pay / 2;
        _Money = 0;
        PSI = 0;
        TireImage.sprite = TireSprites[0];
        SetPSI();
    }
    public void AddMoneyToPlayer()
    {
        PlayerStats.Instance.AdjustMoney(_pay, false);
        PlayerStats.Instance.AdjustMoney(_bonus, false);
    }
}
