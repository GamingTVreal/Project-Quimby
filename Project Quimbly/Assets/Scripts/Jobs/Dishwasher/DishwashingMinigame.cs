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
    [SerializeField] private GameObject DirtPrefab;
    [SerializeField] private TMP_Text Pay,DirtCleaned,Bonus;
    public GameObject DirtObj;
    public float TimeRemaing = 2;
    [SerializeField] RectTransform SpawnArea;

    public float timeRemaining = 10;
    //float minutes = Mathf.FloorToInt(timeRemaining / 60);

    private void Start()
    {
        DishTransform = GameObject.Find("Dirt Spawn Area").transform;
        CreateDirtOnDish();
    }
    private void Update()
    {
        if (SpawnArea.childCount > 1)
        {
            foreach (Transform child in SpawnArea)
            {
                Destroy(child.gameObject);
            }
        }
        Pay.text = _pay.ToString();
        DirtCleaned.text = _dirtCleaned.ToString();
            
    }
    public void CreateDirtOnDish()
    {
        DirtObj = Instantiate(DirtPrefab, new Vector3(Random.Range(SpawnArea.rect.xMin, SpawnArea.rect.xMax),
       Random.Range(SpawnArea.rect.yMin, SpawnArea.rect.yMax), 0) + SpawnArea.transform.position, Quaternion.identity,SpawnArea);
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
}
