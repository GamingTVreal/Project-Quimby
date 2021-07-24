using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour
{
    private float startPosY;
    private bool isBeingHeld = false;

    // Update is called once per frame
    private void Update()
    {
        if (isBeingHeld == true)
        {
            
            Vector3 MousePos;
            MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            this.transform.localPosition = new Vector3(MousePos.x * 100, MousePos.y * 100, 0);
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            isBeingHeld = true;
        }


            

        

    }
    private void OnMouseUp()
    {
        isBeingHeld = false;
    }
}
