
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
        public string Area;
        public GameObject loadingScreenObj;
        public Slider slider;
        public GameObject Complete;

        AsyncOperation async;

        public void Park()
    {
        Area = "Park";
        LoadScreenExample();
    }
        public void LoadScreenExample()
        {
            StartCoroutine(LoadingScreen());
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

