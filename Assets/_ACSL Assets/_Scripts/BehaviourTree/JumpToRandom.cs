using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToRandom : Task
{
    // Use this for initialization
    public float Speed = 5.0f;
    public float TurnSpeed = 2.0f;
    public float Accuracy = 1.5f;
    public Vector3 Target = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
    public float JumpStrength = 5.0f;
    bool HasJumped = false;

    public override NodeResult Execute()
    {
        // Receive task information
        GameObject go = tree.gameObject;

        // Override default values
        Speed = (float)tree.GetValue("Speed");
        TurnSpeed = (float)tree.GetValue("TurnSpeed");
        Accuracy = (float)tree.GetValue("Accuracy");
        JumpStrength = (float)tree.GetValue("JumpStrength");
        //this.Target = (Vector3)tree.GetValue("Target");

        if (HasJumped == false)
        {
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(0, JumpStrength, 0, ForceMode.Impulse);
            HasJumped = true;
        }

        if (Vector3.Distance(go.transform.position, Target) < Accuracy)
        {
            Target = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            HasJumped = false;
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
