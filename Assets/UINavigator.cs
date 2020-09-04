using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINavigator : MonoBehaviour
{
    public EventSystem eventSystem;
    private RectTransform m_Rect;
    private Image m_Image;

    private readonly MyTimer m_DisplayTimer = new MyTimer();
    private bool m_Displaying = false;

    private void Start()
    {
        m_Rect = GetComponent<RectTransform>();
        m_Image = GetComponent<Image>();
        InputManager.instance.JoystickConnectedEvent += Display;
        InputManager.instance.JoystickDisconnectedEvent += Hide;
    }

    private void Display(string name, int index)
    {
        m_Displaying = true;
    }

    private void Hide(string name, int index)
    {
        m_Displaying = false;
    }

    private void Update()
    {
        // If keyboard input is detected
        if (Input.inputString.Length > 0 && !eventSystem.currentSelectedGameObject.GetComponent<InputField>())
        {
            m_Displaying = true;
            m_DisplayTimer.StartTimer(2);
        }

        if (!m_DisplayTimer.active)
        {
            m_Displaying = false;
        }

        if (m_Displaying)
        {
            m_Image.enabled = true;
        }
        else
        {
            m_Image.enabled = false;
        }

        GameObject currentSelected = eventSystem.currentSelectedGameObject;

        // Ensure we never get lost in the UI.
        if (!currentSelected || currentSelected == null || !currentSelected.activeInHierarchy)
        {
            Selectable foundSelectable = FindObjectOfType<Selectable>();
            if (!foundSelectable)
            {
                return;
            }

            GameObject foundGameObject = foundSelectable.gameObject;

            currentSelected = foundGameObject;
            eventSystem.SetSelectedGameObject(currentSelected);
        }

        //// Close the current panel if 'Cancel' is hit.
        //if (Input.GetButtonDown("Cancel"))
        //{
        //    UIManager uiManager = currentSelected.GetComponentInParent<UIManager>();
        //    if (uiManager)
        //    {
        //        if (uiManager.Top() == uiManager.GetPanelFromIndex(0) || uiManager.Top() == uiManager.GetPanelFromIndex(5))
        //        {
        //            return;
        //        }

        //        uiManager.Pop();
        //    }
        //}

        // Size the selector to the currently selected GO's rect
        SizeToRect(m_Rect, currentSelected.transform.GetComponent<RectTransform>());

        // Update display timer
        m_DisplayTimer.Update();
    }

    public void SizeToRect(RectTransform rt, RectTransform other)
    {
        Vector2 lastPivot = rt.pivot;
        lastPivot = other.pivot;

        rt.position = Vector3.Slerp(rt.position, other.position, Time.deltaTime * 25);

        rt.localScale = Vector3.Slerp(rt.localScale, other.localScale, Time.deltaTime * 25);

        Vector2 size = new Vector2();
        size = Vector3.Slerp(rt.sizeDelta, other.rect.size, Time.deltaTime * 10);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);

        rt.pivot = lastPivot;
    }
}

