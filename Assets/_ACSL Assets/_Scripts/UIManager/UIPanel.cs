using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class UIPanel : MonoBehaviour
{
    public RectTransform rectTransform { get => transform as RectTransform;}
    private bool awakeCalled;

    virtual protected void Awake()
    {
        if (!awakeCalled)
        {
            awakeCalled = true;
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(true);
    }
    //Called when this panel gets pushed to stack
    public virtual void OnPushed()
    {
        if (!awakeCalled)
            awakeCalled = true;
        gameObject.SetActive(true);
    }

    //Called when this panel gets popped off the stack
    public virtual void OnPopped()
    {
        gameObject.SetActive(false);
    }

    //Called when a panel was pushed on top of this panel on the stack
    public virtual void OnPushedOnTop()
    {
        gameObject.SetActive(false);
    }

    //Called when the panel above it gets popped off
    public virtual void OnPoppedTo()
    {
        gameObject.SetActive(true);
    }
}
