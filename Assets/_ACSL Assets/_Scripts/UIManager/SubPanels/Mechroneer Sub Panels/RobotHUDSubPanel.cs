using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotHUDSubPanel : MechroneerSubPanel
{
    Robot m_robot;

    public RobotAbilitySubPanel leftArmPanel;
    public RobotAbilitySubPanel rightArmPanel;
    public RobotAbilitySubPanel bodyPanel;
    public RobotAbilitySubPanel legPanel;

    public GameObject aimPanel;

    public DoubleBarSubPanel healthBar;
    public DoubleBarSubPanel energyBar;
    public void PossessPlayer(Robot robot)
    {
        m_robot = robot;
        robot.onRobotDeath += Robot_onRobotDeath;
        robot.pauseFunction = mechroneerUI.TogglePauseHUD;
        InstantiateHUDElements();
    }

    private void InstantiateHUDElements()
    {
        leftArmPanel.Init(m_robot.robotParts[RobotPartType.LeftArm], m_robot.energyComponent);
        rightArmPanel.Init(m_robot.robotParts[RobotPartType.RightArm], m_robot.energyComponent);
        bodyPanel.Init(m_robot.robotParts[RobotPartType.Body], m_robot.energyComponent);
        legPanel.Init(m_robot.robotParts[RobotPartType.Leg], m_robot.energyComponent);

        healthBar.Init(m_robot.healthComponent);
        energyBar.Init(m_robot.energyComponent);
    }

    private void Robot_onRobotDeath(Robot caller)
    {
        mechroneerUI.SwitchToSpectatorHUD();
        m_robot = null;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        if (m_localController)
            m_localController.SetControllerEnabled(true);
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
        if (m_localController)
            m_localController.SetControllerEnabled(false);
    }

    private void Update()
    {
        if (!m_robot)
            return;

        if (m_robot.abilityToFire)
            aimPanel.SetActive(true);
        else
            aimPanel.SetActive(false);
    }

    public void PlayerLeftArmAbility()
    {
        m_robot.SelectAbility1();
    }

    public void PlayerRightArmAbility()
    {
        m_robot.SelectAbility2();
    }
    public void PlayerBodyAbility()
    {
        m_robot.SelectAbility3();
    }

    public void PlayerLegAbility()
    {
        m_robot.SelectAbility4();
    }

}
