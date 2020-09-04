using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechroneerSubPanel : UISubPanel
{
    public MechroneerUI mechroneerUI;

    protected MechroneerController m_localController;

    public virtual void Init(MechroneerController controller)
    {
        m_localController = controller;
    }
}
