using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BasicFunctions : MonoBehaviour
{

    public Animator PhoneAnimation;
    public GameObject Phone;
    public TextMeshProUGUI MoneyText,EnergyText,PlayerName;
    public static int Money;
    public static int Energy;
    public static string Name;
    // Start is called before the first frame update
    void Start() 
    {
        if(Name == null)
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
   public void OpenthePhone()
    {
        if (PhoneAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime>1)
        {
            Debug.Log("NotPlaying");
            Phone.SetActive(true);
        }
        else
        {
            Debug.Log("Playing");
        }
    }
}
