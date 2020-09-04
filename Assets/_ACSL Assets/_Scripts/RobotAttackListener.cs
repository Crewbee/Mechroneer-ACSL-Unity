using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotAttackListener : RobotListener
{
    public override event UnityAction requirementsMet;

    private void Start()
    {
        robot.TargetSelectedEvent += Robot_TargetSelectedEvent;
    }

    private void Robot_TargetSelectedEvent()
    {
        requirementsMet();
        robot.TargetSelectedEvent -= Robot_TargetSelectedEvent;
    }
}
