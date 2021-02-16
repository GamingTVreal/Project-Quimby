using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TextBoxManager Peter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void StartBrowsing()
    {
        Peter.currentline = 1;
        Peter.EnableTextBox();
        Peter.endatline = 4;
    }
}
