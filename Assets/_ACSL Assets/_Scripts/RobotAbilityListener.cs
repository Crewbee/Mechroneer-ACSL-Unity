using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotAbilityListener : RobotListener
{
    public override event UnityAction requirementsMet;
    private readonly bool[] m_StepStatus = new bool[2];

    private void Start()
    {
        robot.AbilitySelectedEvent += Robot_AbilitySelectedEvent;
        robot.AbilityFiredEvent += Robot_AbilityFiredEvent;
    }

    private void Robot_AbilityFiredEvent()
    {
        m_StepStatus[1] = true;
    }

    private void Robot_AbilitySelectedEvent()
    {
        m_StepStatus[0] = true;
    }

    protected override void CheckConditions()
    {
        int trueCount = 0;

        // If all input completed
        for (int i = 0; i < m_StepStatus.Length; i++)
        {
            if (m_StepStatus[i])
                trueCount++;

            if (trueCount == m_StepStatus.Length)
            {
                requirementsMet();
                enabled = false;
                robot.AbilitySelectedEvent -= Robot_AbilitySelectedEvent;
                robot.AbilityFiredEvent -= Robot_AbilityFiredEvent;
            }
        }
    }
}
