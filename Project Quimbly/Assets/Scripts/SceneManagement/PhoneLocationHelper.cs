using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectQuimbly.SceneManagement
{
    public class PhoneLocationHelper : MonoBehaviour
    {
        [SerializeField] GameObject phoneMenuGO = null;

        private void Awake() 
        {
            if(phoneMenuGO == null)
            {
                phoneMenuGO = GameObject.FindWithTag("PhoneMenu");
            }
        }

        public void LoadLocation(string newLocation)
        {
            GameObject controllerGO = GameObject.FindWithTag("GameController");
            if (controllerGO != null)
            {
                if (SceneManager.GetActiveScene().name == newLocation)
                {
                    gameObject.SetActive(true);
                    phoneMenuGO.SetActive(true);
                    return;
                }
                LoadingScreenScript loadingScript = controllerGO.GetComponent<LoadingScreenScript>();
                loadingScript.LoadNewArea(newLocation);
            }
        }

        public void ReturnToMainMenu()
        {
            GameObject controllerGO = GameObject.FindWithTag("GameController");
            if(controllerGO != null)
            {
                LoadingScreenScript loadingScript = controllerGO.GetComponent<LoadingScreenScript>();
                loadingScript.ReturnToMainMenu();
            }
        }
    }
}
