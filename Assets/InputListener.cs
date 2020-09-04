using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputListener : MonoBehaviour
{
    public List<string> buttonActions = new List<string>();

    private readonly List<bool> m_ButtonStatus = new List<bool>();

    public event UnityAction<bool> allButtonsPressed;


    private void Start()
    {
        // Populate status list
        foreach (string button in buttonActions)
        {
            m_ButtonStatus.Add(new bool());
        }

    }

    private void Update()
    {
        // Poll for input
        for (int i = 0; i < buttonActions.Count; i++)
        {
            // If button input detected set status to true
            if (Input.GetButtonDown(buttonActions[i]) && !EventSystem.current.IsPointerOverGameObject())
            {
                m_ButtonStatus[i] = true;
            }
        }

        // If all input completed
        for (int i = 0; i < m_ButtonStatus.Count; i++)
        {
            int trueCount = m_ButtonStatus.Count(b => b);

            if (trueCount == m_ButtonStatus.Count)
            {
                allButtonsPressed(true);
            }
        }
    }
}
