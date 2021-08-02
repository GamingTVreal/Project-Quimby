using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControlTest : MonoBehaviour
{
    public void OpenPhoneMenu()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        
        if(gameController != null)
        {
            gameController.GetComponent<BasicFunctions>().OpenthePhone();
        }
    }
}
