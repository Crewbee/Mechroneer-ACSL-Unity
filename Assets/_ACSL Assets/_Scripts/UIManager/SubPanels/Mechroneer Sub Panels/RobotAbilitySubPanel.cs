using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotAbilitySubPanel : MonoBehaviour
{
    public Image skillIcon;
    public Image cooldownRingVisual;
    public TextMeshProUGUI energyCostText;
    public TextMeshProUGUI cooldownText;

    private RobotPart m_robotPart;
    private EnergyComponent m_energyComponent;
    public void Init(RobotPart robotPart, EnergyComponent energyComponent)
    {
        m_robotPart = robotPart;
        m_energyComponent = energyComponent;

        skillIcon.sprite = m_robotPart.abilityData.abilityIcon;
        skillIcon.color = m_robotPart.abilityData.abilityColor;

        energyCostText.text = m_robotPart.abilityData.energyCost.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_robotPart)
            UpdateDisplay();
    }

    void UpdateDisplay()
    {
        UpdateCooldownVisuals();
        UpdateTextColor();
        UpdateSkillIconAlpha();
    }

    void UpdateCooldownVisuals()
    {
        MyTimer abilityTimer = m_robotPart.specialAbilityTimer;
        if (abilityTimer.active)
        {
            cooldownRingVisual.fillAmount = abilityTimer.timePassed;
            cooldownText.text = abilityTimer.timeLeftSeconds.ToString("N1");
        }
        else
        {
            cooldownRingVisual.fillAmount = 1;
            cooldownText.text = "";
        }

    }

    void UpdateTextColor()
    {
        if (m_energyComponent.currentValue < m_robotPart.abilityData.energyCost)
        {
            energyCostText.color = new Color(0.9f, 0.2549f, 0.2549f);
            cooldownText.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            cooldownText.color = energyCostText.color = Color.white;
        }
    }

    void UpdateSkillIconAlpha()
    {
        MyTimer abilityTimer = m_robotPart.specialAbilityTimer;
        if (m_energyComponent.currentValue < m_robotPart.abilityData.energyCost)
        {
            Color alpha = skillIcon.color;
            alpha.a = 0.2f;
            skillIcon.color = alpha;

            Color alpha2 = cooldownRingVisual.color;
            alpha2.a = alpha.a;
            cooldownRingVisual.color = alpha2;
        }
        else if (abilityTimer.active)
        {
            Color alpha = skillIcon.color;
            alpha.a = abilityTimer.timePassed;
            skillIcon.color = alpha;
        }
        else
        {
            Color alpha = skillIcon.color;
            alpha.a = 1;
            skillIcon.color = alpha;

            Color alpha2 = cooldownRingVisual.color;
            alpha2.a = alpha.a;
            cooldownRingVisual.color = alpha2;
        }
    }
}
