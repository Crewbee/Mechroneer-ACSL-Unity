using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotButton : MonoBehaviour
{
    public string nameValue;
    public string healthValue;
    public string energyValue;
    public string rangeValue;
    public string speedValue;
    
    [Header("Text Fields")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI speedText;

    public void UpdateText()
    {
        nameText.text = nameValue;
        healthText.text = healthValue;
        energyText.text = energyValue;
        rangeText.text = rangeValue;
        speedText.text = speedValue;
    }
}
