using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.SceneManagement
{
    public class PhoneLocationHelper : MonoBehaviour
    {
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
