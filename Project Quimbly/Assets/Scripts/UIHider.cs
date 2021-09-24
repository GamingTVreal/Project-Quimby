using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHider : MonoBehaviour
{
    [SerializeField] GameObject UI,textbox;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            HideUI();
        }
    }

    void HideUI()
    {
        if (UI.activeInHierarchy == true)
        {
            UI.SetActive(false);
        }
        else
        {
            UI.SetActive(true);
        }

    }
}
