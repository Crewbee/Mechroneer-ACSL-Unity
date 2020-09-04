using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseSubPanel : MechroneerSubPanel
{
    public GameObject winPanel;
    public GameObject losePanel;

    private GameObject m_activePanel;
    public void DidWin(bool win)
    {
        if (win)
            m_activePanel = winPanel;
        else
            m_activePanel = losePanel;
        m_activePanel.SetActive(true);
    }

    protected override void OnDeactivated()
    {
        base.OnDeactivated();
        m_activePanel.SetActive(false);
    }
}
