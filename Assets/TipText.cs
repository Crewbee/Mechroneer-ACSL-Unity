using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class TipText : MonoBehaviour
{
    public List<string> tipText;
    public TextMeshProUGUI tipTextDisplay;
    public float tipDisplayTime = 10f;

    private float m_ElpasedTime = 0f;

    void ChangeText(string text)
    {
        tipTextDisplay.text = text;
    }

    private void Awake()
    {
        ChangeText(tipText[UnityEngine.Random.Range(0, tipText.Count)]);
    }

    void Update()
    {
        if (m_ElpasedTime >= tipDisplayTime)
        {
            ChangeText(tipText[UnityEngine.Random.Range(0, tipText.Count)]);
            m_ElpasedTime = 0f;
        }

        m_ElpasedTime += Time.deltaTime;
    }
}
