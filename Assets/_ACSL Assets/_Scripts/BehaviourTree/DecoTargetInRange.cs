using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoTargetInRange : Decorator
{
    private float m_Range;
    public DecoTargetInRange(Node node, float range) : base(node)
    {
        m_Range = range;
    }

    public override NodeResult DecoratorExecute()
    {
        GameObject go = tree.gameObject;
        GameObject Target = (GameObject)tree.GetValue("Target");
        float range = m_Range;

        if (Target)
        {
            Vector3 target = Target.transform.position;

            Debug.DrawLine(target, go.transform.position, Color.cyan);

            float distance = Vector3.Distance(go.transform.position, target);

            if (distance < range)
            {

                if (currentChild == -1)
                {
                    currentChild++;
                    tree.PushNode(wrappedNode);
                    return NodeResult.STACKED;
                }
                // if we've previously pushed a child onto the stack and we're
                // executing, then that child has completed (with either a success or a failure)
                if (childResult == NodeResult.FAILURE)
                {
                    // we're done - report failure up the tree
                    Reset();
                    return NodeResult.FAILURE;
                }
                else
                {
                    // we got a Success.  
                    Reset();
                    return NodeResult.SUCCESS;
                }
            }
            else
            {
                return NodeResult.FAILURE;
            }
        }
        else
        {
            return NodeResult.SUCCESS;
        }
    }
}