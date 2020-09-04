//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;


//TODO: DELETE THIS
//[System.Serializable]
//public class MouseKeyPlayerController : PlayerController
//{
//    public CameraDriver ControlCamera { get; set; }
//    [Header("Cursor Settings")]
//    public CursorLockMode CursorLockType = CursorLockMode.Confined;
//    public bool CursorVisible = true;
//    ARRaycastManager m_arRaycastManager;
//    private bool m_EnableMouseControl;
//    private bool m_InputEnabled = true;

//    public void Init(OldPlayer player)
//    {
//        m_EnableMouseControl = true;
//        Cursor.lockState = CursorLockType;
//        ControlCamera = Camera.main.GetComponent<CameraDriver>();
//        if (ControlCamera)
//        {
//            if ((player as MechroneerPlayer).currentRobot != null)
//            {
//                (ControlCamera as MechroneerDriver).AddTarget((player as MechroneerPlayer).currentRobot.transform);
//                // Add all robots to centroid
//                //foreach (var Robot in (player as MechroneerPlayer).activeRobots)
//                //    (ControlCamera as MechroneerDriver).AddTarget(Robot.transform);
//            }
//            else
//            {
//                //ControlCamera.SetTarget(new GameObject().transform);
//            }
//        }
//        else
//        {
//            GameObject arSessionOrigin = GameObject.Find("AR Session Origin");
//            if (arSessionOrigin)
//            {
//                m_arRaycastManager = arSessionOrigin.GetComponent<ARRaycastManager>();
//            }
//        }
//    }

//    public void SetControlsActive(bool active)
//    {
//        m_InputEnabled = active;
//    }

//    public MouseKeyPlayerController()
//    {

//    }

//    public void UpdateControls()
//    {

//    }

//    public void FixedUpdate()
//    {
//        if (!ControlCamera)
//        {
//            return;
//        }

//        if (!m_InputEnabled)
//            return;

//        UpdateMouseControlToggle();
//        Vector2 lookInput = GetLookInput();
//        ControlCamera.UpdateRotation(lookInput.x, lookInput.y);
//    }

//    public void Update()
//    {
//        if (!ControlCamera)
//        {
//            return;
//        }

//        if (!m_InputEnabled)
//            return;

//        float zoomInput = GetZoomInput();
//        ControlCamera.UpdateZoom(zoomInput);
//    }

//    public void SetFacingDirection(Vector3 direction)
//    {

//    }

//    public void AddLedgeDir(Vector3 ledgeDir)
//    {

//    }

//    public Vector3 GetControlRotation()
//    {
//        return Vector3.zero;
//    }

//    public float GetZoomInput()
//    {
//        if (!m_InputEnabled)
//            return 0;

//        if (!m_EnableMouseControl)
//        {
//            return 0f;
//        }

//        return Input.GetAxisRaw("Mouse ScrollWheel");
//    }

//    public Vector3 GetMoveInput()
//    {
//        if (!m_InputEnabled)
//            return Vector3.zero;

//        if (EventSystem.current.IsPointerOverGameObject())
//        {
//            //Debug.Log("EventSystem.current.IsPointerOverGameObject");
//            return Vector3.one * Mathf.Infinity;
//        }
//#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hitInfo = new RaycastHit();

//        int layerMask = 1 << 10;
//        layerMask = ~layerMask;

//        RaycastHit[] allHits = Physics.RaycastAll(ray);
//        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
//        {
//            return hitInfo.point;
//        }
//#endif
//#if UNITY_IOS || UNITY_ANDROID
//        DebugText.AddMessage("Checking for touch", 1.0f);
//        if (Input.touchCount > 0)
//        {
//            DebugText.AddMessage("Touch found", 10.0f);
//            Vector2 touchPosition = Input.GetTouch(0).position;
//            //m_arRaycastManager.Raycast(touchPosition, s_)
//            GameObject arcamera = GameObject.Find("AR Camera");
//            if (arcamera)
//            {
//                DebugText.AddMessage("Camera Found", 10.0f);
//                Camera camera = arcamera.GetComponent<Camera>();
//                Ray ARray = camera.ScreenPointToRay(touchPosition);
//                DebugText.AddMessage("Trying to move - raycast", 10.0f);
//                RaycastHit ARhitInfo = new RaycastHit();
//                if (Physics.Raycast(ARray, out ARhitInfo))
//                {
//                    DebugText.AddMessage("Found - raycast", 10.0f);
//                    return ARhitInfo.point;
//                }
//            }
//        }
//#endif
//        return Vector3.one * Mathf.Infinity;
//    }

//    public Vector3 GetLookInput()
//    {
//        if (!m_InputEnabled)
//            return Vector3.zero;

//        //Don't allow looking around if mouse isn't enabled
//        if (!m_EnableMouseControl)
//        {
//            return Vector3.zero;
//        }
//        // Keyboard
//        return new Vector3(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"), 0.0f);
//    }

//    public bool ChangePerspective()
//    {
//        if (!m_InputEnabled)
//            return false;

//        return Input.GetButtonDown("Change Perspective");
//    }

//    public GameObject GetAimTarget()
//    {
//        if (!m_InputEnabled)
//            return null;

//        if (EventSystem.current.IsPointerOverGameObject())
//        {
//            return null;
//        }

//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hitInfo = new RaycastHit();
//        if (Physics.Raycast(ray, out hitInfo))
//        {
//            return hitInfo.transform.gameObject;
//        }
//        return null;
//    }

//    public bool IsFiring()
//    {
//        if (!m_EnableMouseControl)
//        {
//            return false;
//        }

//        if (!m_InputEnabled)
//            return false;


//        return Input.GetButton("Fire1");
//    }

//    public bool SwitchRobotTarget()
//    {
//        if (!m_InputEnabled)
//            return false;

//        return Input.GetKeyDown(KeyCode.LeftShift);
//    }

//    public bool LeftArmAbility()
//    {
//        if (!m_InputEnabled)
//            return false;

//        return Input.GetButtonDown("Ability0");
//    }

//    public bool RightArmAbility()
//    {
//        if (!m_InputEnabled)
//            return false;

//        return Input.GetButtonDown("Ability1");
//    }

//    public bool BodyAbility()
//    {
//        if (!m_InputEnabled)
//            return false;

//        return Input.GetButtonDown("Ability2");
//    }

//    public bool LegAbility()
//    {
//        if (!m_InputEnabled)
//            return false;

//        return Input.GetButtonDown("Ability3");
//    }

//    private void UpdateMouseControlToggle()
//    {
//        if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
//        {
//            Cursor.lockState = CursorLockType;
//        }
//        m_EnableMouseControl = Cursor.lockState == CursorLockType;

//        Cursor.visible = CursorVisible; // !m_EnableMouseControl
//    }
//}
