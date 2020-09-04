using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : Task
{
    public override NodeResult Execute()
    {
        tree.gameObject.GetComponent<AudioSource>().Play();


        return NodeResult.SUCCESS;
    }
}

