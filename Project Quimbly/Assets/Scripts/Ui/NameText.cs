using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameText : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerStats.Instance.Name;
    }
}
