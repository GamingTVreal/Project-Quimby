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

        if (this.transform.localPosition.y > 51)
        {
            isBeingHeld = false;
            this.transform.localPosition = new Vector3(807, 48, 0);
        }
        if (this.transform.localPosition.y < -201)
        {
            isBeingHeld = false;
            this.transform.localPosition = new Vector3(807, -198, 0);
        }
        if (isBeingHeld == true && this.transform.position.y > -200 && this.transform.position.y < 55)
        {

            Vector3 MousePos;
            MousePos = Input.mousePosition;
            // MousePos = this.transform.localPosition - MousePos;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            this.transform.localPosition = new Vector3(807, MousePos.y * 100, 0);
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