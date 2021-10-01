﻿using System.Collections;
using System.Collections.Generic;
// using ProjectQuimbly.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectQuimbly.SceneManagement
{
    public class ChangeSceneButton : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] string destination;
        [SerializeField] UnityEvent onSceneChange;
        // [SerializeField] GameEvent locationChangeEvent;

        public void ChangeScene()
        {
            transform.SetParent(null);
            onSceneChange?.Invoke();
            // locationChangeEvent.RaiseEvent(destination);
            StartCoroutine(Transition());
        }

        public void ChangeScene(string saveFile)
        {
            transform.SetParent(null);
            onSceneChange?.Invoke();
            // locationChangeEvent.RaiseEvent(destination);
            StartCoroutine(Transition(saveFile));
        }

        public void ReturnToMainMenu()
        {
            transform.SetParent(null);
            onSceneChange?.Invoke();
            StartCoroutine(ExitToMenu());
        }

        public void SetSceneToLoad(int newScene)
        {
            sceneToLoad = newScene;
        }

        public void SetDestination(string newDestination)
        {
            destination = newDestination;
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            yield return null;

            DontDestroyOnLoad(this.gameObject);
            LoadFader loadFader = FindObjectOfType<LoadFader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            loadFader.FadeOutImmediate();

            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            
            yield return savingWrapper.Load();

            savingWrapper.Save();
            loadFader.FadeInImmediate();
            Time.timeScale = 1f;

            Destroy(this.gameObject);
        }

        private IEnumerator Transition(string saveFile)
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            DontDestroyOnLoad(this.gameObject);
            LoadFader loadFader = FindObjectOfType<LoadFader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            loadFader.FadeOutImmediate();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            yield return savingWrapper.Load(saveFile);

            loadFader.FadeInImmediate();
            Time.timeScale = 1f;

            Destroy(this.gameObject);
        }

        private IEnumerator ExitToMenu()
        {
            DontDestroyOnLoad(this.gameObject);
            LoadFader loadFader = FindObjectOfType<LoadFader>();
            loadFader.FadeOutImmediate();

            yield return SceneManager.LoadSceneAsync(0);

            loadFader.FadeInImmediate();
            Time.timeScale = 1f;

            Destroy(this.gameObject);
        }
    }
}
