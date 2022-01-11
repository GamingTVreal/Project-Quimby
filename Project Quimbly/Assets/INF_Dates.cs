using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectQuimbly.Dialogue;

public class INF_Dates : MonoBehaviour
{
    AIConversant conversant = null;
    GirlController girlController = null;

    private void Awake()
    {
        conversant = GetComponent<AIConversant>();
        girlController = GetComponent<GirlController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
    


