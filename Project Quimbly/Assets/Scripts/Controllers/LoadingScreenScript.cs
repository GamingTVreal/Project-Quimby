﻿
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
        public GameObject loadingScreenObj;
        public Slider slider;

        AsyncOperation async;

        public void LoadScreenExample()
        {
            StartCoroutine(LoadingScreen());
        }

        IEnumerator LoadingScreen()
        {
            loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            async.allowSceneActivation = false;

            while (async.isDone == false)
            {
                slider.value = async.progress;
                if (async.progress == 0.9f)
                {
                    slider.value = 1f;
                    async.allowSceneActivation = true;
                    loadingScreenObj.SetActive(false);
                }
                yield return null;

            }
        }
    }
