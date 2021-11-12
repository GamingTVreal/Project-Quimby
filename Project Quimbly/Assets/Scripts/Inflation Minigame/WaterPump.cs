using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Inflation
{
    public class WaterPump : MonoBehaviour
    {
        public Inflate InflationCode;
        private bool isBeingHeld = false;
        public AudioSource Main;
        [SerializeField] AudioClip[] Enemasounds;
        
        private void OnMouseDown()
        {
            if (Main.isPlaying != true)
            {
                Main.PlayOneShot(Enemasounds[0]);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 MousePos;
                MousePos = Input.mousePosition;
                // MousePos = this.transform.localPosition - MousePos;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);
                isBeingHeld = true;
            }
        }

        private void OnMouseUp()
        {
            Main.Stop();
        }
        
        // Update is called once per frame
        void Update()
        {
            if (isBeingHeld == true)
            {
                InflationCode.WaterInflate();
            }
        }
    }
}
