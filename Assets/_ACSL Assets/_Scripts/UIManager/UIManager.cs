using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public bool allowEmptyStack;
    public bool pushIfEmpty = true;
    public bool popIfDisabled;
    Stack<UIPanel> m_panelStack;

    public UIPanel initialPanel;
    private void Awake()
    {
        m_panelStack = new Stack<UIPanel>();
    }

    private void OnEnable()
    {
        if (pushIfEmpty || !allowEmptyStack)
        {
            if (m_panelStack.Count == 0)
                Push(initialPanel);
        }
    }

    private void OnDisable()
    {
        if (popIfDisabled)
        {
            int stackCountRemain = (allowEmptyStack) ? 0 : 1;
            while (m_panelStack.Count > stackCountRemain)
            {
                Pop();
            }
        }
    }

    public void Push(UIPanel panel)
    {
        if (panel == null)
        {
            Debug.LogError("Panel to push is null");
            return;
        }
        if (m_panelStack.Count > 0)
            Top().OnPushedOnTop();
        m_panelStack.Push(panel);
        Top().OnPushed();
    }

    public void Pop()
    {
        if (!allowEmptyStack)
        {
            if (m_panelStack.Count == 1)
                return;
        }
        else if (m_panelStack.Count == 0)
            return;

        Top().OnPopped();
        m_panelStack.Pop();
        if (m_panelStack.Count > 0)
            Top().OnPoppedTo();
    }


    public UIPanel Top()
    {
        return m_panelStack.Peek();
    }

}
