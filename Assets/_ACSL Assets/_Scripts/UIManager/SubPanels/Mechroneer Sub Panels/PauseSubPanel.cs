using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSubPanel : MechroneerSubPanel
{

    public void QuitGame()
    {
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        m_localController.SetControllerEnabled(false);
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
        m_localController.SetControllerEnabled(true);
    }
}
