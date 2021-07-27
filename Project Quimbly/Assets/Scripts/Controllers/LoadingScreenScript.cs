
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    public string Area;
    public GameObject loadingScreenObj;
    public Slider slider;
    public GameObject Complete;
    public JobAssigner Job;

    AsyncOperation async;
    public void InflationMinigame()
    {
        Area = "Inflation Minigame";
        LoadScreenExample();
    }
    public void FeedingRoom()
    {
        Area = "Feeding Minigame";
        LoadScreenExample();
    }
    public void GroceryStore()
    {
        Area = "Grocery Store";
        LoadScreenExample();
    }
    public void Park()
    {
        Area = "Park";
        LoadScreenExample();
    }

    public void Home()
    {
        Area = "Home";
        LoadScreenExample();
    }
    public void JobAgency()
    {
        Area = "JobAgency";
        LoadScreenExample();
    }

    public void LoadNewArea(string newArea)
    {
        Area = newArea;
        StartCoroutine(LoadingScreen());
    }

    public void LoadScreenExample()
    {
        StartCoroutine(LoadingScreen());
    }
    public void Work()
    {
        switch (PlayerStats.Instance.CurrentJob)
        {
            case 0:
                break;
            case 1: 
                    Area = "Dishwasher";
                    LoadScreenExample();
                break;

        }
        
    }
    public void Feed()
    {
        Area = "Feeding Minigame";
        LoadScreenExample();
    }

    public void Continue()
    {
        {
            async.allowSceneActivation = true;
            loadingScreenObj.SetActive(false);
            Complete.SetActive(false);
        }

    }

    IEnumerator LoadingScreen()
    {
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(Area);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                Complete.SetActive(true);
            }


            yield return null;

        }
    }
}

