using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : UIPanel
{
    private List<UISubPanel> m_panels;
    private UISubPanel m_currentActivePanel;

    protected override void Awake()
    {
        base.Awake();

        m_panels = new List<UISubPanel>();
        for (int i = 0; i < transform.childCount; i++)
        {
            UISubPanel findPanel = transform.GetChild(i).GetComponent<UISubPanel>();
            if (findPanel)
                m_panels.Add(findPanel);
        }
        m_currentActivePanel = null;
    }

    public void SelectSubPanel(int index)
    {
        if (!m_currentActivePanel)
        {
            m_currentActivePanel = m_panels[index];
            m_currentActivePanel.ToggleSubPanel();
        }
        else if (m_currentActivePanel == m_panels[index])
        {
            m_currentActivePanel.ToggleSubPanel();
            m_currentActivePanel = null;
        }
        else
        {
            m_currentActivePanel.ToggleSubPanel();
            m_currentActivePanel = m_panels[index];
            m_currentActivePanel.ToggleSubPanel();
        }
    }

    public override void OnPopped()
    {
        base.OnPopped();
        if (!m_currentActivePanel)
        {
            m_currentActivePanel.ToggleSubPanel();
            m_currentActivePanel = null;
        }
    }
}
