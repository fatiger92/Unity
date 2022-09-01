using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatePanel : BasePanel
{
    [SerializeField]
    TextMeshProUGUI scoreValue;

    [SerializeField]
    Gage HPGage;

    public void SetScore(int value)
    {
        Debug.Log("SetScore value = " + value);

        scoreValue.text = value.ToString();
    }

    public void SetHP(float currentValue, float maxValue)
    {
        HPGage.SetHP(currentValue, maxValue);
    }
}