using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : Task
{
    // Use this for initialization
    public float Speed = 5.0f;
    public float TurnSpeed = 2.0f;
    public float Accuracy = 1.5f;
    public Vector3 Target = Vector3.zero;

    public override NodeResult Execute()
    {
        // Receive task information
        GameObject go = tree.gameObject;

        // Override default values
        Speed = (float)tree.GetValue("Speed");
        TurnSpeed = (float)tree.GetValue("TurnSpeed");
        Accuracy = (float)tree.GetValue("Accuracy");
        this.Target = (Vector3)tree.GetValue("Target");

        if (Vector3.Distance(go.transform.position, Target) < Accuracy)
        {
            return NodeResult.SUCCESS;
        }

        Vector3 position = new Vector3(go.transform.position.x, -Target.y, go.transform.position.z);
        Vector3 direction = Target - position;
        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);

        if (Vector3.Distance(go.transform.position, Target) < Speed * Time.deltaTime)
        {
            go.transform.position = Target;
        }
        else
        {
            go.transform.position = go.transform.position + go.transform.forward * Speed * Time.deltaTime;
            //go.transform.Translate(0, 0, Speed * Time.deltaTime);
        }
        return NodeResult.RUNNING;
    }

    public override void Reset()
    {
        base.Reset();
    }
}
