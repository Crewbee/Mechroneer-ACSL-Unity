using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHead : RobotPart
{
    protected float m_RangeModifier { get; set; }

    protected float m_AccuracyModifier { get; set; }
    protected override void Awake()
    {
        base.Awake();
        partType = RobotPartType.Head;
    }

}
