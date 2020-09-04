using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Task
{
    public Wait()
    {
        m_Index = 1;
    }

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
