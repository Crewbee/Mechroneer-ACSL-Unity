using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    //Reference to tree the node resides in
    public BehaviorTree tree;

    //Used as an index for an array of children
    public int currentChild;

    public int m_Index;

    public Node()
    {
    }
    public virtual NodeResult Execute()
    {
        return NodeResult.FAILURE;
    }

    public virtual bool SetChildResult(NodeResult result)
    {
        return true;
    }

    public virtual void Reset()
    {
        //Resets the current child to consider the node unexecuted and set the index back to the beginning
        currentChild = -1;
    }
}
