
using System;
using System.Collections;
using ProjectQuimbly.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    public string areaToLoad;
    public GameObject loadingScreenObj;
    public Slider slider;
    public GameObject Complete;
    public JobAssigner Job;

    [SerializeField] GameObject chSceneObjPrefab;

    AsyncOperation async;
    public void InflationMinigame()
    {
        areaToLoad = "Inflation Minigame";
        LoadScreenExample();
    }
    public void FeedingRoom()
    {
        areaToLoad = "Feeding Minigame";
        LoadScreenExample();
    }
    public void GroceryStore()
    {
        areaToLoad = "Grocery Store";
        LoadScreenExample();
    }
    public void Park()
    {
        areaToLoad = "Park";
        LoadScreenExample();
    }

    public void Home()
    {
        areaToLoad = "Home";
        LoadScreenExample();
    }
    public void JobAgency()
    {
        areaToLoad = "JobAgency";
        LoadScreenExample();
    }
    public void Beach()
    {
        areaToLoad = "Beach";
        LoadScreenExample();
    }
    public void MainMenu()
    {
        areaToLoad = "Main Menu";
        LoadScreenExample();
    }

    public void LoadNewArea(string newArea)
    {
        areaToLoad = newArea;
        ChangeSceneButton changeSceneObj = Instantiate(chSceneObjPrefab).GetComponent<ChangeSceneButton>();
        changeSceneObj.SetSceneToLoad(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + areaToLoad + ".unity"));
        changeSceneObj.SetDestination(areaToLoad);
        changeSceneObj.ChangeScene();
        // StartCoroutine(LoadingScreen());
    }

    public void LoadScreenExample()
    {
        ChangeSceneButton changeSceneObj = Instantiate(chSceneObjPrefab).GetComponent<ChangeSceneButton>();
        changeSceneObj.SetSceneToLoad(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + areaToLoad + ".unity"));
        changeSceneObj.SetDestination(areaToLoad);
        changeSceneObj.ChangeScene();
        // StartCoroutine(LoadingScreen());
    }

    public void ReturnToMainMenu()
    {
        ChangeSceneButton changeSceneObj = Instantiate(chSceneObjPrefab).GetComponent<ChangeSceneButton>();
        changeSceneObj.SetSceneToLoad(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + areaToLoad + ".unity"));
        changeSceneObj.SetDestination(areaToLoad);
        changeSceneObj.ReturnToMainMenu();
    }

    public void Work()
    {
        switch (PlayerStats.Instance.CurrentJob)
        {
            case 0:
                break;
            case 1: 
                    areaToLoad = "Jobs/Dishwasher";
                    LoadScreenExample();
                break;

        }
        
    }
    public void Feed()
    {
        areaToLoad = "Feeding Minigame";
        LoadScreenExample();
    }

    public void Continue()
    {
        {
            async.allowSceneActivation = true;
            loadingScreenObj.SetActive(false);
            Complete.SetActive(false);

            // Autosave
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
        }

    }

    IEnumerator LoadingScreen()
    {


        // loadingScreenObj.SetActive(true);
        // async = SceneManager.LoadSceneAsync(Area);
        // async.allowSceneActivation = false;

        // while (async.isDone == false)
        // {
        //     slider.value = async.progress;
        //     if (async.progress == 0.9f)
        //     {
        //         slider.value = 1f;
        //         Complete.SetActive(true);
        //     }


            yield return null;

        // }
    }
}

