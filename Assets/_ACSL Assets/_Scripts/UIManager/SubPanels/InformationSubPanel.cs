using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationSubPanel : UISubPanel
{
    List<UISubPanel> m_subPanels;
    UISubPanel m_activeSubPanel;
    

    public void ActivateSubPanel(int index)
    {
        if (m_activeSubPanel == m_subPanels[index])
            return;

        m_activeSubPanel.ToggleSubPanel();
        m_activeSubPanel = m_subPanels[index];
        m_activeSubPanel.ToggleSubPanel();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_subPanels = new List<UISubPanel>();
        foreach (Transform child in transform)
        {
            UISubPanel findPanel = child.GetComponent<UISubPanel>();
            if (findPanel)
                m_subPanels.Add(findPanel);
        }
        m_activeSubPanel = m_subPanels[0];
        m_activeSubPanel.SetSubPanelActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
