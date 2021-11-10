using ProjectQuimbly.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SophiaFinale : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        int date;
        GirlController controller = GetComponent<GirlController>();

        date = controller.GetDateLevel();
        if ( date > 2)
        {
            AIConversant Conversant = GetComponent<AIConversant>();
            Conversant.SetNewDialog(dialogue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
