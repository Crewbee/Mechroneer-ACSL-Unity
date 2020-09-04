using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    RobotPart[] m_Collection;

    public void AddPart(RobotPart aNewPart)
    {
        int index;
        m_Collection = new RobotPart[1];

        index = m_Collection.Length;

        m_Collection[index] = aNewPart;

    }

    public RobotPart RetrievePart(int index)
    {
        return m_Collection[index];
    }
}
