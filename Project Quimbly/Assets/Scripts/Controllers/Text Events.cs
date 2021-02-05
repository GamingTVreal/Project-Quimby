using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEvents : MonoBehaviour
{
    public TextAsset NextText;
    public int StartLine;


    public TextBoxManager TextBox;
    // Start is called before the first frame update
    void Start()
    {
        TextBox = FindObjectOfType<TextBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
