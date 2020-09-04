using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotMovementListener : RobotListener
{
    public override event UnityAction requirementsMet;

    void Start()
    {
        robot.MovePlayerEvent += Robot_MovePlayerEvent;
    }

    private void Robot_MovePlayerEvent()
    {
        requirementsMet();
        robot.MovePlayerEvent -= Robot_MovePlayerEvent;
    }
}
