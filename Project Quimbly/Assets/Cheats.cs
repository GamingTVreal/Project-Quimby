using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Cheats : MonoBehaviour
{
    public TMP_InputField CheatBox;
    public TMP_Text Console;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            switch(CheatBox.text){
                case "MakinBank":
                CheatBox.text = "";
                Console.text = "Gave $100000 don't blow it all in one place!";
                PlayerStats.Instance.AdjustMoney(100000);
                break;
                case "EnergyDrink":
                CheatBox.text = "";
                Console.text = "You will never sleep again...";
                PlayerStats.Instance.AdjustEnergy(100000);
                break;
            }

        }
    }
}
