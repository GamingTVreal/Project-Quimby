using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ProjectQuimbly.UI;
using ProjectQuimbly.Schedules;

namespace ProjectQuimbly.Inflation
{
    public class InflationMinigame : MonoBehaviour
    {
        [SerializeField] private const int GoodExitFullnessValue = 60;
        public GameObject gameOver, pumpObject;
        public TMP_Text pressureText, fullnessText;
        public float pressure, fullness;
        public AudioClip[] pumpSFX;            //0-4 Releasing Pump, 5-10 Charging Pump
        public AudioSource source;
        public int currentPump;
        static int InflatedDatelevel;

        bool infLine = false;
        bool hasDoneDialogue1 = false;
        GirlInflation girlInflation = null;
        GirlController girlController = null;

        // Used for cursor change
        [SerializeField] LayerMask grabbableLayers;

        private void Start()
        {
            // Get spawned girl obect
            GameObject girlContainer = GameObject.FindWithTag("GirlContainer");
            if (girlContainer.transform.childCount == 0) return;

            GameObject charPrefab = girlContainer.transform.GetChild(0).gameObject;
            girlInflation = charPrefab.GetComponent<GirlInflation>();
            girlController = charPrefab.GetComponent<GirlController>();
        }

        public void ChoosePump(int choice)
        {
            currentPump = choice;
            Debug.Log(currentPump + " is the current pump");
           
            if (choice == 0)
            {
                Debug.Log("airpump");
                girlInflation.AirPump.SetActive(true);
                girlInflation.WaterPump.SetActive(false);
                girlInflation.UpdateFullnessSprite(3, true);


            }
            if(choice == 1)
            {
                girlInflation.AirPump.SetActive(false);
                girlInflation.WaterPump.SetActive(true);
                girlInflation.UpdateFullnessSprite(3, false);
            }

        }
        public void Inflate()
        {
            if (currentPump == 0)
            {
                pressure = pressure + 25;
                fullness += Random.Range(1, 7);
                girlInflation.UpdateFullnessSprite(fullness, true);

                int x = Random.Range(0, 4);
                source.PlayOneShot(pumpSFX[x]);

                fullnessText.text = fullness.ToString();
                Debug.Log("Fullness: " + fullness);
            }
            else if (currentPump == 1)
            {
                infLine = false;
                pressure = pressure + (Time.deltaTime * 5);
                fullness = fullness + Time.deltaTime;
                girlInflation.UpdateFullnessSprite(fullness, false);

                fullnessText.text = fullness.ToString("0.00");
                Debug.Log("Fullness: " + fullness);
            }

            if (fullness >= 100)
            {
                fullness = 100;
                girlInflation.StartDialogue("DebMax");
            }

            if (pressure >= 75)
            {
                if (source.isPlaying == false)
                {
                    source.PlayOneShot(girlInflation.GetPressureVoiceLine());
                }
            }

            if (pressure >= 100)
            {
                pumpObject.SetActive(false);
                gameOver.SetActive(true);
            }
        }

        public void Recharge()
        {
            infLine = false;
            int x = Random.Range(5, 9);
            source.PlayOneShot(pumpSFX[x]);
        }
        public void test()
        {
            fullness = 100;
        }
        private void Update()
        {
            CheckBellyRub();

            pressureText.text = pressure.ToString("0.00");
            if (pressure > 0 && pressure < 101)
            {
                pressure -= Time.deltaTime;
            }
        }

        private void CheckBellyRub()
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, grabbableLayers);
            if (hit.collider != null && hit.collider.name == "BellyRubArea")
            {
                if (Input.GetMouseButton(0))
                {
                    if (infLine == false)
                    {
                        if (source.isPlaying == false)
                        {
                            source.PlayOneShot(girlInflation.GetBellyRubVoiceLine());
                        }

                        infLine = true;
                    }
                    if (pressure >= 1)
                    {
                        pressure = pressure - Time.deltaTime * 3;
                    }
                    if (pressure < 1)
                    {
                        pressure = 0;
                    }
                }
            }
        }

        public void WaterInflate()
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (currentPump == 1 && Input.GetMouseButton(0) && hit.collider.name == "Enemabag")
            {
                Debug.Log(hit.collider.name);
                Inflate();

            }
        }

        public void LeaveInflation()
        {
            ResetGirlLocation();
            if (fullness <= 100 && girlController.GetInflatedDateLevel() <= 2)
            {
                girlInflation.StartDialogue("DatePossiblity");
            }
            else if (fullness < GoodExitFullnessValue)
            {
                girlInflation.StartDialogue("LeaveDeb");
            }
            else
            {
                girlInflation.StartDialogue("MaxDeb");
            }
        }

        public void ExitFromGameOver()
        {
            ResetGirlLocation();
            GetComponent<LoadingScreenScript>().Home();
        }

        private void ResetGirlLocation()
        {
            Scheduler schedule = girlInflation.GetComponent<Scheduler>();
            if (schedule != null)
            {
                schedule.ResetLocation();
            }
        }
    }
}
