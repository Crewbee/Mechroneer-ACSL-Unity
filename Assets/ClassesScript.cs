using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassesScript : MonoBehaviour
{
    public MenuRobot previewRobot;

    public TextMeshProUGUI RobotName;
    public TextMeshProUGUI RobotDescription;

    public TextMeshProUGUI HealthValue;
    public TextMeshProUGUI EnergyValue;
    public TextMeshProUGUI Range;
    public TextMeshProUGUI Speed;

    public Image LeftArmSkill;
    public Image RightArmSkill;
    public Image BodySkill;
    public Image LegSkill;

    public TextMeshProUGUI LeftArmSkillName;
    public TextMeshProUGUI RightArmSkillName;
    public TextMeshProUGUI BodySkillName;
    public TextMeshProUGUI LegSkillName;

    public TextMeshProUGUI LeftArmSkillCost;
    public TextMeshProUGUI RightArmSkillCost;
    public TextMeshProUGUI BodySkillCost;
    public TextMeshProUGUI LegSkillCost;

    public TextMeshProUGUI LeftArmSkillDescription;
    public TextMeshProUGUI RightArmSkillDescription;
    public TextMeshProUGUI BodySkillDescription;
    public TextMeshProUGUI LegSkillDescription;

    public int m_robotIndex;
    private List<RobotData> m_robots;


    // Start is called before the first frame update
    void Start()
    {
        m_robotIndex = 0;
        m_robots = UserData._instance.robots;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayClassInformation();
    }

    void DisplayClassInformation()
    {
        RobotName.text = m_robots[m_robotIndex].RobotName;
        RobotDescription.text = m_robots[m_robotIndex].RobotDescription;

        HealthValue.text = m_robots[m_robotIndex].health.ToString();
        EnergyValue.text = m_robots[m_robotIndex].energy.ToString();
        Range.text = m_robots[m_robotIndex].range.ToString();
        Speed.text = m_robots[m_robotIndex].speed.ToString();

        LeftArmSkill.color = m_robots[m_robotIndex].lArm.abilityData.abilityColor;
        LeftArmSkill.sprite = m_robots[m_robotIndex].lArm.abilityData.abilityIcon;
        LeftArmSkillName.text = m_robots[m_robotIndex].lArm.abilityData.abilityName;
        LeftArmSkillCost.text = m_robots[m_robotIndex].lArm.abilityData.energyCost.ToString();
        LeftArmSkillDescription.text = m_robots[m_robotIndex].lArm.abilityData.abilityDescription;

        RightArmSkill.color = m_robots[m_robotIndex].rArm.abilityData.abilityColor;
        RightArmSkill.sprite = m_robots[m_robotIndex].rArm.abilityData.abilityIcon;
        RightArmSkillName.text = m_robots[m_robotIndex].rArm.abilityData.abilityName;
        RightArmSkillCost.text = m_robots[m_robotIndex].rArm.abilityData.energyCost.ToString();
        RightArmSkillDescription.text = m_robots[m_robotIndex].rArm.abilityData.abilityDescription;

        BodySkill.color = m_robots[m_robotIndex].body.abilityData.abilityColor;
        BodySkill.sprite = m_robots[m_robotIndex].body.abilityData.abilityIcon;
        BodySkillName.text = m_robots[m_robotIndex].body.abilityData.abilityName;
        BodySkillCost.text = m_robots[m_robotIndex].body.abilityData.energyCost.ToString();
        BodySkillDescription.text = m_robots[m_robotIndex].body.abilityData.abilityDescription;

        LegSkill.color = m_robots[m_robotIndex].legs.abilityData.abilityColor;
        LegSkill.sprite = m_robots[m_robotIndex].legs.abilityData.abilityIcon;
        LegSkillName.text = m_robots[m_robotIndex].legs.abilityData.abilityName;
        LegSkillCost.text = m_robots[m_robotIndex].legs.abilityData.energyCost.ToString();
        LegSkillDescription.text = m_robots[m_robotIndex].legs.abilityData.abilityDescription;
    }

    public void NextRobot()
    {
        m_robotIndex = (m_robotIndex + 1) % m_robots.Count;
        previewRobot.Build(m_robots[m_robotIndex]);
    }

    public void PreviousRobot()
    {
        m_robotIndex -= 1;
        if (m_robotIndex < 0)
        {
            m_robotIndex = m_robots.Count - 1;
        }

        previewRobot.Build(m_robots[m_robotIndex]);
    }

}
