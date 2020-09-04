using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomNum : Task {
    // Use this for initialization
    public override NodeResult Execute()
    {
        tree.SetValue("Key", (Random.Range(0, (int)tree.GetValue("NumOfActions"))));
        return NodeResult.SUCCESS;
    }
}
