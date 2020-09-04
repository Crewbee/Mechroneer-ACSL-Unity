using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISubPanelManager : MonoBehaviour
{
    private List<UISubPanel> m_panels;
    private UISubPanel m_currentActivePanel;

    public bool allowOnlyOneActive;
    public bool allowNoActivePanel;
    public bool pushOnEnabled;
    public int initialPanelIndex = 0;
    private void Awake()
    {
        m_panels = new List<UISubPanel>();
        for (int i = 0; i < transform.childCount; i++)
        {
            UISubPanel findPanel = transform.GetChild(i).GetComponent<UISubPanel>();
            if (findPanel)
                m_panels.Add(findPanel);
        }
        m_currentActivePanel = null;
    }

    private void OnEnable()
    {
        if (pushOnEnabled)
        {
            if (allowOnlyOneActive && !m_currentActivePanel)
                SelectSubPanel(initialPanelIndex);
            else
                m_panels[initialPanelIndex].SetSubPanelActive(true);
        }
    }

    private void OnDisable()
    {
        if (allowNoActivePanel && allowOnlyOneActive)
        {
            if (m_currentActivePanel)
            {
                m_currentActivePanel.SetSubPanelActive(false);
                m_currentActivePanel = null;
            }
        }
    }
    public void SelectSubPanel(int index)
    {
        if (index > m_panels.Count)
            return;

        if (allowOnlyOneActive)
        {
            if (!m_currentActivePanel)
            {
                m_currentActivePanel = m_panels[index];
                m_currentActivePanel.SetSubPanelActive(true);
            }
            else if (m_currentActivePanel == m_panels[index] && allowNoActivePanel)
            {
                m_currentActivePanel.SetSubPanelActive(false);
                m_currentActivePanel = null;
            }
            else
            {
                m_currentActivePanel.SetSubPanelActive(false);
                m_currentActivePanel = m_panels[index];
                m_currentActivePanel.SetSubPanelActive(true);
            }
        }
        else
        {
            m_panels[index].ToggleSubPanel();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
