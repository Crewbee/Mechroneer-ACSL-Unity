using UnityEngine;

public enum InputMode
{
    Keyboard,
    MouseKeyboard,
    Joystick,
    Touch
}
public class InputManager : MonoBehaviour
{
    // Singleton
    public static InputManager instance;

    // Events
    public delegate void JoystickConnected(string name, int index);
    public event JoystickConnected JoystickConnectedEvent;

    public delegate void JoystickDisconnected(string name, int index);
    public event JoystickConnected JoystickDisconnectedEvent;

    public delegate void InputModeChanged(InputMode newInputMode);
    public event InputModeChanged InputModeChangedEvent;

    // Variables
    public InputMode InputMode { get; private set; }
    public string[] JoystickNames { get; private set; }
    public int JoystickCount { get; private set; }

    private void Awake()
    {
        // Singleton check
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        InvokeRepeating("DetectJoysticks", 0f, 5f);
    }

    private void Update()
    {
        DetectInput();
    }

    public bool DetectInput()
    {
        // Keyboard or mouse input detected
        if (Input.inputString.Length > 0 && Input.mousePresent)
        {
            InputMode = InputMode.Keyboard;

            if (Input.mousePresent)
            {
                InputMode = InputMode.MouseKeyboard;
            }

            InputModeChangedEvent?.Invoke(InputMode);
            return true;
        }

        // Joystick
        if (JoystickCount > 0)
        {
            for (int i = 0; i < 20; i++)
            {
                if (Input.GetKeyDown("joystick 1 button " + i)) // Assuming single local user
                {
                    InputMode = InputMode.Joystick;
                    InputModeChangedEvent?.Invoke(InputMode);
                    return true;
                }
            }
        }

        // Mobile (touch)
        if (Input.touchCount > 0)
        {
            InputMode = InputMode.Touch;
            InputModeChangedEvent?.Invoke(InputMode);
            return true;
        }

        // No input
        return false;
    }

    public bool DetectJoysticks()
    {
        //Get Joystick Names
        JoystickNames = Input.GetJoystickNames();

        if (JoystickNames.Length > 0)
        {
            for (int i = 0; i < JoystickNames.Length; ++i)
            {
                if (!string.IsNullOrEmpty(JoystickNames[i]))
                {
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + i + " is connected using: " + JoystickNames[i]);
                    JoystickCount++;
                    JoystickConnectedEvent?.Invoke(JoystickNames[i], i);
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + i + " is disconnected.");
                    JoystickCount--;
                    JoystickDisconnectedEvent?.Invoke(JoystickNames[i], i);
                }
            }

            // If at least 1 joystick is enabled
            if (JoystickCount > 0)
            {
                return true;
            }
        }

        return false;
    }
}
