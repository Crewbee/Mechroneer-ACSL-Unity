using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotCameraListener : RobotListener
{
    public override event UnityAction requirementsMet;
    private readonly bool[] m_StepStatus = new bool[3];
    private readonly bool[] m_RotateStatus = new bool[2];
    private readonly bool[] m_ZoomStatus = new bool[2];

    void Start()
    {
        robot.RotateCameraEvent += Robot_RotateCameraEvent;
        robot.ChangePerspectiveEvent += Robot_ChangePerspectiveEvent;
        robot.ZoomCameraEvent += Robot_ZoomCameraEvent;
    }

    private void Robot_ZoomCameraEvent(float input)
    {
        if (input > 0)
        {
            m_ZoomStatus[0] = true;
        }
        else if (input < 0)
        {
            m_ZoomStatus[1] = true;
        }

        if (m_ZoomStatus[0] == true && m_ZoomStatus[1] == true)
            m_StepStatus[2] = true;

        CheckConditions();
    }

    private void Robot_ChangePerspectiveEvent()
    {
        m_StepStatus[1] = true;
        CheckConditions();
    }

    private void Robot_RotateCameraEvent(Vector2 input)
    {
        if (input.x != 0)
        {
            m_RotateStatus[0] = true;
        }
        else if (input.y != 0)
        {
            m_RotateStatus[1] = true;
        }

        if (m_RotateStatus[0] == true && m_RotateStatus[1] == true)
            m_StepStatus[0] = true;

        CheckConditions();
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
                robot.RotateCameraEvent -= Robot_RotateCameraEvent;
                robot.ChangePerspectiveEvent -= Robot_ChangePerspectiveEvent;
                robot.ZoomCameraEvent -= Robot_ZoomCameraEvent;
            }
        }
    }
}
