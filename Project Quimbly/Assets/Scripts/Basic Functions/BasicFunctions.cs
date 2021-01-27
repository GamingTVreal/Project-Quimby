using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BasicFunctions : MonoBehaviour
{
    public TextMeshProUGUI MoneyText,EnergyText,PlayerName;
    public int Money;
    public int Energy;
    public string Name;
    // Start is called before the first frame update
    void Start() 
    {
        if(Name == "")
        {
            FirstTimeSetup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerName.text = Name;
        MoneyText.text = Money.ToString();
        EnergyText.text = Energy.ToString();
    }
    void FirstTimeSetup()
    {
        Energy = 20;
        Money = 2500;
        
    }

}
