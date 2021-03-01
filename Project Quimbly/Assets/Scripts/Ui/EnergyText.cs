using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyText : MonoBehaviour
{
    public PlayerStats Player;
    private void Update()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + PlayerStats.Instance.GetEnergy();
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "/" + PlayerStats.Instance.GetEnergy(true);
    }
    public void Sleep()
    {
        Player.AdjustEnergy(20);
    }
}
