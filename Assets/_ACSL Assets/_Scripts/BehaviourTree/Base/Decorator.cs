using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : Node
{
    /* Child node to evaluate */
    //childrenNodes will not be evaluated
    public Node wrappedNode;
    protected NodeResult childResult;

    public Node GetNode
    {
        get { return wrappedNode; }
    }

    /* The constructor requires the child node that this inverter decorator 
     * wraps*/
    public Decorator(Node node)
    {
        wrappedNode = node;
    }

    /* Reports a success if the child fails and 
     * a failure if the child succeeds. Running will report 
     * as running */

    //Modify Execute and input your checks in here
    public override NodeResult Execute()
    {
        //push the node
        return DecoratorExecute();
    }

    public virtual NodeResult DecoratorExecute()
    {
        if (currentChild == -1)
        {
            currentChild++;
            tree.PushNode(wrappedNode);
            return NodeResult.STACKED;
        }
        // if we've previously pushed a child onto the stack and we're
        // executing, then that child has completed (with either a sucess or a failure)
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

    public override void Reset()
    {
        childResult = NodeResult.UNKNOWN;
        base.Reset();
    }
}