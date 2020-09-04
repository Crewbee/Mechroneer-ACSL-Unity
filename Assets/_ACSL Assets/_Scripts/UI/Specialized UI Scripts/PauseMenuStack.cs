using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuStack : MonoBehaviour
{
    public RectTransform parent;
    //UIPanelStack m_stack;
    // Start is called before the first frame update
    //List<UIPanelOld> m_childPanels;
    //private void Awake()
    //{
    //    m_childPanels = new List<UIPanelOld>();
    //    foreach (Transform child in parent)
    //    {
    //        UIPanelOld panel = child.GetComponent<UIPanelOld>();
    //        if (!panel)
    //            panel = child.gameObject.AddComponent<UIPanelOld>();

    //        m_childPanels.Add(panel);
    //        child.gameObject.SetActive(false);
    //        panel.active = false;
    //    }
    //    m_stack = new UIPanelStack(m_childPanels[0], false);
    //}

    //public void Push(int index)
    //{
    //    m_stack.Push(m_childPanels[index]);
    //}

    //public void Pop()
    //{
    //    m_stack.Pop();
    //}
}
