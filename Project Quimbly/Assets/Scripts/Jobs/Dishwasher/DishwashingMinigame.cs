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
        _timeRemaining = _maxTime;
        DishTransform = GameObject.Find("Dirt Spawn Area").transform;
        CreateDirtOnDish();
    }
    private void Update()
    {
        TimeText.text = _timeRemaining.ToString();
        _timeRemaining -= Time.deltaTime ;
        if (SpawnArea.childCount > 1)
        {
            foreach (Transform child in SpawnArea)
            {
                Destroy(child.gameObject);
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
    public void CreateDirtOnDish()
    {
        DirtObj = Instantiate(DirtPrefab, new Vector3(Random.Range(SpawnArea.rect.xMin - 5, SpawnArea.rect.xMax - 5),
       Random.Range(SpawnArea.rect.yMin - 5, SpawnArea.rect.yMax - 5), 0) + SpawnArea.transform.position, Quaternion.identity,SpawnArea);
        //Instantiate(DirtPrefab, SpawnArea);
        /*Instantiate(DirtPrefab, new Vector3(Random.Range(SpawnArea.rect.xMin, SpawnArea.rect.xMax),
       Random.Range(SpawnArea.rect.yMin, SpawnArea.rect.yMax), 0) + SpawnArea.transform.position, Quaternion.identity);*/
        Invoke("CreateDirtOnDish", Random.Range(1f, 5f));
        
    }
    public void AddToTotal()
    {
        DirtyDish.Amounttoadd = Random.Range(1, 20);
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
