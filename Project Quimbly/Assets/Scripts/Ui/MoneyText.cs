﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "" + PlayerStats.Instance.GetMoney();
    }
}
