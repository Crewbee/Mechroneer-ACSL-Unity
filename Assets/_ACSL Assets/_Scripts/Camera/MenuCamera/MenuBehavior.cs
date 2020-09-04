using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    public enum MenuState
    {
        CenterState = 0,
        LeftState,
        RightState
    }

    public enum MouseState
    {
        MouseNull = 0,
        MouseLeft,
        MouseRIght
    }

    public Camera m_MenuCamera;

    public MenuState m_MenuState;

    public MouseState m_MouseState;

    Vector3[] m_CameraPositions;
    Vector3[] m_CameraRotations;

    // Start is called before the first frame update
    void Start()
    {
        m_MenuState = MenuState.CenterState;
        m_MouseState = MouseState.MouseNull;

        if(m_MenuCamera = null)
        {
            m_MenuCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(m_MouseState != MouseState.MouseNull)
        {
            CheckForMouseInput();
        }

        switch (m_MenuState)
        {
            case MenuState.CenterState:
                if(m_MouseState == MouseState.MouseLeft)
                {

                }
                if(m_MouseState == MouseState.MouseRIght)
                {

                }

                break;
            case MenuState.LeftState:
                if (m_MouseState == MouseState.MouseLeft)
                {

                }
                if (m_MouseState == MouseState.MouseRIght)
                {

                }
                break;

            case MenuState.RightState:
                if (m_MouseState == MouseState.MouseLeft)
                {

                }
                if (m_MouseState == MouseState.MouseRIght)
                {

                }
                break;
                
        }


    }

    void CheckForMouseInput()
    {
        Vector2 ScreenSize = new Vector2(Screen.width, Screen.height);

        if(Input.GetMouseButtonDown(0))
        {
            if(Input.mousePosition.x > (ScreenSize.x / 2.0f))
            {
                m_MouseState = MouseState.MouseRIght;
            }
            if(Input.mousePosition.x < (ScreenSize.x / 2.0f))
            {
                m_MouseState = MouseState.MouseLeft;
            }
        }
    }
}
