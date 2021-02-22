using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameScript : MonoBehaviour
{
    private BasicFunctions B;
    NameScript Names;
    public GameObject NameMenu;
    public TMP_InputField Name;
    public GameObject Tutorial;
    public static GameObject Carrot;
    // Start is called before the first frame update
    void Start()
    {
     
     if (BasicFunctions.Name != null)
        {
            
            if(NameMenu != null)
            {
                Name = null;
                NameMenu.SetActive(false);
                Debug.Log(BasicFunctions.Name);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void SelectName()
    {

        BasicFunctions.Name = Name.text;
        if (BasicFunctions.Name != null)
        {
            Tutorial.SetActive(true);
            NameMenu.SetActive(false);
        }
        
    }
}
