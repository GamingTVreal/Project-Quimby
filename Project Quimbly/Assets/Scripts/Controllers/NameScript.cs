using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class NameScript : MonoBehaviour
{
    public BasicFunctions B;
    NameScript Names;
    public GameObject NameMenu;
    public TMP_InputField Name;
    public GameObject Tutorial;
    // Start is called before the first frame update
    void Start()
    {
     if (B.Name != "")
        {
            NameMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void SelectName()
    {

        B.Name = Name.text;
        if (B.Name != "")
        {
            Tutorial.SetActive(true);
            NameMenu.SetActive(false);
        }
        
    }
}
