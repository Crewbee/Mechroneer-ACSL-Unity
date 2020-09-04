using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DoubleBarSubPanel : MonoBehaviour
{
    private ResourceComponent m_resourceComponent;

    public Image mainBar;
    public Image subBar;
    public TextMeshProUGUI textDisplay;
    public float catchUpDuration;

    private LerpFloat m_time;

    private delegate void UpdateFunc();
    UpdateFunc m_fixedUpdate;

    private float m_oldResourcePercent;
    private void Awake()
    {
        if (m_time == null)
            m_time = new LerpFloat();
        m_fixedUpdate = EmptyUpdate;
    }
    public void Init(ResourceComponent component)
    {
        m_resourceComponent = component;
        mainBar.fillAmount = subBar.fillAmount = m_resourceComponent.percent;
        if (textDisplay)
            textDisplay.text = ((int)m_resourceComponent.currentValue).ToString();
        m_fixedUpdate = UpdateBars;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_fixedUpdate();
    }

    void EmptyUpdate()
    {
    }

    void UpdateBars()
    {

        m_oldResourcePercent = mainBar.fillAmount;

        if (m_resourceComponent)
            mainBar.fillAmount = m_resourceComponent.percent;

        if (mainBar.fillAmount != m_oldResourcePercent)
        {
            m_time.Reset(0, 1, catchUpDuration);
        }
        if (m_time.Active())
            subBar.fillAmount = Mathf.SmoothStep(subBar.fillAmount, mainBar.fillAmount, m_time.GetAccumulated());
        if (mainBar.fillAmount > subBar.fillAmount)
        {
            subBar.fillAmount = mainBar.fillAmount;
            m_time.Stop();
        }

        if (textDisplay)
            textDisplay.text = ((int)m_resourceComponent.currentValue).ToString();
        m_time.Update();
    }
}
