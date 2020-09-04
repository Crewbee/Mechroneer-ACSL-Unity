using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : Node {
    public float executionTime = 2.0f;
    public float elapsedTime = 0.0f;

    public override NodeResult Execute()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= executionTime)
        {
            Reset();
            return NodeResult.SUCCESS;
        }
        else
        {
            return NodeResult.RUNNING;
        }
    }

    public override void Reset()
    {
        elapsedTime = 0.0f;
        base.Reset();
    }

}
