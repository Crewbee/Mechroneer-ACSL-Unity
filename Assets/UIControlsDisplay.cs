using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControlsDisplay : MonoBehaviour
{
    [Header("Control Displays")]
    public GameObject windowsControls;
    public GameObject gamepadControls;
    public GameObject mobileControls;

    private GameObject[] m_controlSchemes = new GameObject[3];

    void Start()
    {
        m_controlSchemes[0] = windowsControls;
        m_controlSchemes[1] = gamepadControls;
        m_controlSchemes[2] = mobileControls;

        InvokeRepeating("CheckInputMethod", 0f, 5f);
    }

    private void CheckInputMethod()
    {
        // If mobile platform
        if (Application.isMobilePlatform)
        {
            foreach (var scheme in m_controlSchemes)
            {
                if (scheme != mobileControls)
                    scheme.SetActive(false);
                else
                    scheme.SetActive(true);
            }
        }

        // If windows platform
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            foreach (var scheme in m_controlSchemes)
            {
                if (scheme != windowsControls)
                    scheme.SetActive(false);
                else
                    scheme.SetActive(true);
            }
        }
        
        // If joystick detected
        if (InputManager.instance.JoystickCount > 0)
        {
            foreach (var scheme in m_controlSchemes)
            {
                if (scheme != gamepadControls)
                    scheme.SetActive(false);
                else
                    scheme.SetActive(true);
            }
        }
    }
}
