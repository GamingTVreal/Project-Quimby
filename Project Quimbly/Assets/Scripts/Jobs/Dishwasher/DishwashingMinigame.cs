using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Assertions.Must;

public class DishwashingMinigame : MonoBehaviour
{
    [SerializeField] private  DirtyDishScript DirtyDish;
    private int _pay, _bonus,_dirtCleaned;
    private Transform DishTransform;
    [SerializeField] private GameObject DirtPrefab,EndPage;
    [SerializeField] private TMP_Text Pay,DirtCleaned,Bonus,TimeText;
    public GameObject DirtObj;
    int AmountSpawned = 0;
    [SerializeField] RectTransform SpawnArea;



    private float _timeRemaining,_maxTime;
    //float minutes = Mathf.FloorToInt(timeRemaining / 60);

    private void Start()
    {
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
        DishTransform = GameObject.Find("Dirt Spawn Area").transform;
        CreateDirtOnDish();
    }
    private void Update()
    {
        TimeText.text = _timeRemaining.ToString();
        
        _timeRemaining -= Time.deltaTime;
        DisplayTime(_timeRemaining);

            if (SpawnArea.childCount < 3)
        {
            foreach (Transform child in SpawnArea)
            {
                if (child.gameObject.activeInHierarchy == false)
                {
                    Destroy(child.gameObject);
                    CancelInvoke();
                    CreateDirtOnDish();
                }

            }
        }
        
           
            
        
        Pay.text = _pay.ToString();
        DirtCleaned.text = _dirtCleaned.ToString();
        if (_timeRemaining <= 0)
        {
            Bonus.text = _bonus.ToString();
            EndPage.SetActive(true);
        }    
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

public void CreateDirtOnDish()
    {
        Debug.Log(AmountSpawned + "If this is less than 3 than there should be dirt appearing");
        AmountSpawned = 0;
        while (AmountSpawned < 3)
        {
            DirtObj = Instantiate(DirtPrefab, new Vector3(Random.Range(SpawnArea.rect.xMin, SpawnArea.rect.xMax),
            Random.Range(SpawnArea.rect.yMin, SpawnArea.rect.yMax), 0) + SpawnArea.transform.position, Quaternion.identity, SpawnArea);
            AmountSpawned += 1;
        }
       if (AmountSpawned > 3)
        {
            Invoke("CreateDirtOnDish", Random.Range(1f, 5f));
        }
        //Instantiate(DirtPrefab, SpawnArea);
        /*Instantiate(DirtPrefab, new Vector3(Random.Range(SpawnArea.rect.xMin, SpawnArea.rect.xMax),
       Random.Range(SpawnArea.rect.yMin, SpawnArea.rect.yMax), 0) + SpawnArea.transform.position, Quaternion.identity);*/
       
        
    }
    public void AddToTotal()
    {
        DirtyDish.Amounttoadd = Random.Range(1, 8);
        _pay = _pay + DirtyDish.Amounttoadd;
        _dirtCleaned = _dirtCleaned + 1;
        _bonus = _pay / 2;
    }
    public void AddMoneyToPlayer()
    {
        PlayerStats.Instance.AdjustMoney(_pay, false);
        PlayerStats.Instance.AdjustMoney(_bonus, false);
    }
}
