using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBody : RobotPart
{
    protected override void Awake()
    {
        base.Awake();
        partType = RobotPartType.Body;
    }
}
