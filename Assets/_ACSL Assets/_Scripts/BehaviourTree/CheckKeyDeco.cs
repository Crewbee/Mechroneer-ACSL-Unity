using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKeyDeco : Decorator
{
    int numOfKey = 0;

    public CheckKeyDeco(Node node, int key) : base(node)
    {
        numOfKey = key;
    }

    //did I break this
    public override NodeResult DecoratorExecute()
    {
        if (numOfKey == (int)tree.GetValue("Key"))
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
        else
        {
            return NodeResult.FAILURE;
        }
    }
}